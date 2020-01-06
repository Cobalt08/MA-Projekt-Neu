using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    
    public float lifeTime;
    public GameObject explosion;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("BigProjectile"))
        {
            //Destroy(this.gameObject);
            Invoke("DestroyProjectile", lifeTime);
        }
    }

    void DestroyProjectile(){
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
