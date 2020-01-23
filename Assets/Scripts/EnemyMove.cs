using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public Vector2 speed = new Vector2(3, 0);
    public Sprite sprite1;
    public Sprite sprite2;
    public float theCooldown = 5f;
    public int lives = 3;

    private Vector2 movement;
    private float cooldown;

    private Rigidbody2D rigidbodyComponent;
    private bool looksLeft = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        cooldown = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (looksLeft)
            {
                looksLeft = false;
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                looksLeft = true;
                spriteRenderer.sprite = sprite1;
            }
            cooldown += theCooldown;
        }

        if (looksLeft)
        {
            movement = new Vector2(
                      speed.x * -1,
                      speed.y * 0);
        }
        else
        {
            movement = new Vector2(
          speed.x * 1,
          speed.y * 0);
        }
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ProjectileMoveScript shot = otherCollider.gameObject.GetComponent<ProjectileMoveScript>();
        if (shot != null)
        {
            this.lives -= 1;
            if (lives == 0)
            {
                this.rigidbodyComponent.constraints = RigidbodyConstraints2D.None;
                PolygonCollider2D p = GetComponent<PolygonCollider2D>();
                p.isTrigger = false;
                Destroy(this);
                Destroy(gameObject, 3);
            }
        }
    }
}
