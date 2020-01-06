using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableJump : MonoBehaviour
{

    public GameObject explosion;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.Find("Arc"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
