using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour

{

    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    public bool activePlayer = true;
    [SerializeField] bool multiplayer = false;
    public GameObject camera;
    private float positiveDeadZone = 0.5f;
    private float negativeDeadZone = -0.5f;
    private bool canJump = true;
    private bool wait = true;


    //private float timer = 0;
    //private float timerMax = 0;


    // State
    bool isAlive = true;
    float gravityScaleAtStart;
    public bool facingRight = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    CircleCollider2D myFeet;
    Joystick myjoystick;
    JumpButton myjumpbutton;
    SwitchPlayerButton switchPlayerButton;
    AttackButton myattackbutton;
    //GrabButton mygrabbutton;
    Joystick weaponjoystick;



    // Start is called before the first frame update
    void Start()
    {
        if (!multiplayer) { 
            facingRight = true;
            myRigidBody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myBodyCollider = GetComponent<CapsuleCollider2D>();
            myFeet = GetComponent<CircleCollider2D>();
            myjoystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
            myjumpbutton = GameObject.Find("JumpButton").GetComponent<JumpButton>();
            switchPlayerButton = GameObject.Find("SwitchPlayerButton").GetComponent<SwitchPlayerButton>();
            myattackbutton = FindObjectOfType<AttackButton>();
            //mygrabbutton = FindObjectOfType<GrabButton>();
            weaponjoystick = GameObject.Find("Fixed Joystick 2").GetComponent<Joystick>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!multiplayer)
        {
            //switch Player for testing only
            if (switchPlayerButton.Pressed)
            {
                if (wait)
                {
                    activePlayer = !activePlayer;
                    wait = false;
                    StartCoroutine(PlayerCoroutine());
                }
            }
            CameraHandling();

            if (!isAlive) { return; }

            Grab();
            //for offline testing
            if (activePlayer)
            {
                AttackButtonHandling();
                Run();
                Jump();
                Die();
                Attack();
            }
        }
    }

    public void setWhenSwitchSceneInMultiplayer()
    {
        multiplayer = false;
        Start();
    }

    IEnumerator PlayerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        wait = true;
    }

    private void CameraHandling(){
        if (activePlayer == true) {
            camera.SetActive(true);
        }
        if (activePlayer == false) {
            camera.SetActive(false);
        }

    }

    private void AttackButtonHandling() {
        if (GameObject.Find("MultiplayerArc(Clone)") == null)
        {
            if (this.gameObject.name == "Tic")
            {
                weaponjoystick.gameObject.SetActive(true);
                myattackbutton.gameObject.SetActive(false);
            }
            if (this.gameObject.name == "Arc")
            {
                weaponjoystick.gameObject.SetActive(false);
                myattackbutton.gameObject.SetActive(true);
            }
        }

    }
    

    private void Run()
    {


        float controlThrow = myjoystick.Horizontal; // value is betweeen -1 to +1

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;


        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));


        // If the input is moving the player right and the player is facing left...
        if (controlThrow > 0 && !facingRight)
        {
            // ... flip the player.
            //Flip();
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;
           
            transform.localScale = new Vector3(this.transform.localScale.x*-1, this.transform.localScale.y, this.transform.localScale.z);
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (controlThrow < 0 && facingRight)
        {
            // ... flip the player.
            //Flip();
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;
            transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }

    }

    private void Flip()
    {

        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);

    }

    private void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Grabbable")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Switch")))
        {
            myAnimator.SetBool("IsJumping", false);
        }
        else
        {
            return;
        }



        if (myjumpbutton.Pressed == true && canJump)
        {
            canJump = false;
            StartCoroutine(JumpCoroutine());
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            myAnimator.SetBool("IsJumping", true);
        }


        if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        canJump = true;
    }


    private void Die() {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))) {
            isAlive = false;
            //myAnimator.SetBool("IsDead", true);
            myAnimator.SetTrigger("IsDeadTrigger");
            myRigidBody.velocity = deathKick;
            //Invoke("DoSomething", 2);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }
    }

    private void Attack()
    {

        if (myattackbutton.Pressed == true)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack();
            }
        }

        //var horiz = Input.GetAxis("Horizontal");
        //var vert = Input.GetAxis("Vertical");

        float horiz = weaponjoystick.Horizontal;
        float vert = weaponjoystick.Vertical;

        if (horiz > this.positiveDeadZone | horiz < this.negativeDeadZone | vert > this.positiveDeadZone | vert < this.negativeDeadZone)
        {
            Weapon2 weapon2 = GetComponent<Weapon2>();
            if (weapon2 != null)
            {
                weapon2.Attack(horiz, vert);
            }
        }
    }

    private void Grab()
    {
        this.GetComponent<GrabberScript>().enabled = activePlayer;
    }

}
