using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the lives of the boss
/// </summary>
public class BossScript : MonoBehaviour
{

    public int lives = 3;
    public float roaringRate = 6f;
    public AudioClip roar;

    private float roarCooldown;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (roarCooldown > 0)
        {
            roarCooldown -= Time.deltaTime;
        }
        else
        {
            Roar();
        }
    }

    void Roar()
    {
        roarCooldown = roaringRate;
        source.PlayOneShot(roar);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ProjectileMoveScript shot = otherCollider.gameObject.GetComponent<ProjectileMoveScript>();
        if (shot != null)
        {
            if (this.lives < 1)
            {
                Rigidbody2D rigidbodyComponent = GetComponent<Rigidbody2D>();
                rigidbodyComponent.constraints = RigidbodyConstraints2D.None;
                Destroy(this.gameObject, 3);
            }
            else
            {
                this.lives -= 1;
            }
        }
    }
}
