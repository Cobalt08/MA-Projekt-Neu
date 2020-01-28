using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossspawn : MonoBehaviour
{

    public GameObject boss;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("BigProjectile"))
        {
            boss.SetActive(true);
        }
    }
}
