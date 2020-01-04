using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    private bool activated = false;

    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRenderer;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Check if the lever was hit by a projectile
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            Destroy(shot.gameObject, 0.5f);
            Destroy(otherCollider.gameObject.GetComponent<MoveScript>());

            otherCollider.isTrigger = false;
            //otherCollider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

            this.activated = !this.activated;

            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            if (spriteRenderer.sprite == sprite1)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }
        }
    }
}
