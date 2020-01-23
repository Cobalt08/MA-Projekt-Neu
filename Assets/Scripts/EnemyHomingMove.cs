using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMove : MonoBehaviour
{
    private Transform target1;
    private Transform target2;
    private Transform theTarget;
    public float speed = 350f;
    public int lives = 6;
    //public float rotationSpeed = 2000f;
    public bool targetArc;
    Rigidbody2D rb;

    void Start()
    {
        target1 = GameObject.Find("Arc").transform;
        target2 = GameObject.Find("Tic").transform;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (targetArc)
        {
            theTarget = target1;
        }
        else
        {
            theTarget = target2;
        }
    }

    void Update()
    {

        rb.velocity = transform.up * speed * Time.deltaTime;

        Vector3 targetVector = theTarget.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ProjectileMoveScript shot = otherCollider.gameObject.GetComponent<ProjectileMoveScript>();
        if (shot != null)
        {
            Destroy(otherCollider.gameObject);
            if (lives <= 0)
            {
                this.rb.constraints = RigidbodyConstraints2D.None;
                PolygonCollider2D p = GetComponent<PolygonCollider2D>();
                p.isTrigger = false;
                Destroy(this);
                Destroy(gameObject, 3);
            }
            else
            {
                StartCoroutine(hasBeenHit());
            }
        }
    }

    IEnumerator hasBeenHit()
    {
        this.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2);
        this.rb.constraints = RigidbodyConstraints2D.None;
        this.lives--;
    }
}
