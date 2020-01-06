using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableJump : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.Find("Arc"))
        {
            Destroy(this.gameObject);
        }
    }
}
