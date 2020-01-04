using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("BigProjectile"))
        {
            Destroy(this.gameObject);
        }
    }

}
