using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    private bool moving;

    private void OnCollisonEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisonExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            moving = false;
        }
    }

    
    private void FixedUpdate() {
        if (moving) {
            transform.position += (velocity * Time.deltaTime);
        }
    }
    
}
