using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the lives of the boss
/// </summary>
public class BossScript : MonoBehaviour
{

    public int lives = 3;

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
