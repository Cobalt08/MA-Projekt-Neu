using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, GameObject.Find("Arc").GetComponent<WeaponScript>().shootingRate);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag.Equals("Ground"))
        {
            if (!this.gameObject.tag.Equals("BigProjectile") || (this.gameObject.tag.Equals("BigProjectile") && !collision.gameObject.tag.Equals("Ground")))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

