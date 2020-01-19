using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMove : MonoBehaviour
{
    private Transform target1;
    private Transform target2;
    private Transform theTarget;
    public float speed = 350f;
    public float rotationSpeed = 2000f;
    public bool targetArc;
    Rigidbody2D rb;
    
    void Start()
    {
        target1 = GameObject.Find("Arc").transform;
        target2 = GameObject.Find("Tic").transform;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (targetArc)
        {
            theTarget = target1;
        } else
        {
            theTarget = target2;
        }
    }

    void Update()
    {
        
        rb.velocity = transform.up * speed * Time.deltaTime;

        Vector3 targetVector = theTarget.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * speed * Time.deltaTime;
    }
}
