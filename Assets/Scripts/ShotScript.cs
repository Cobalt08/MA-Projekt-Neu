using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{

    public GameObject explosion;

    void Start()
    {
        //Destroy BigProjectile after cooldown is refreshed
        StartCoroutine(ProjectileCoroutine());
        Destroy(gameObject, GameObject.Find("Arc").GetComponent<WeaponScript>().shootingRate);
    }

    IEnumerator ProjectileCoroutine()
    {
        yield return new WaitForSeconds(GameObject.Find("Arc").GetComponent<WeaponScript>().shootingRate -0.1f);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag.Equals("Ground"))
        {
            if (!this.gameObject.tag.Equals("BigProjectile") || (this.gameObject.tag.Equals("BigProjectile") && !collision.gameObject.tag.Equals("Ground")))
            {
                Destroy(this.gameObject);
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
        }
    }
}

