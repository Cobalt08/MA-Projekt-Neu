﻿using UnityEngine;
using System.Collections;

public class GrabberScript : MonoBehaviour
{

	public bool grabbed;
	RaycastHit2D hit;
	public float distance = 2f;
	public Transform holdpoint;
	public float throwforce;
	public LayerMask notgrabbed;
	GrabButton mygrabbutton;
    [SerializeField] bool multiplayer = false;



    // Use this for initialization
    void Start()
	{
        if (!multiplayer)
        {
            mygrabbutton = FindObjectOfType<GrabButton>();
        }
	}

	// Update is called once per frame
	void Update()
	{
        if (!multiplayer)
        {

            //if (Input.GetKeyDown(KeyCode.B))
            if (mygrabbutton.Pressed)

            {

                if (!grabbed)
                {
                    Physics2D.queriesStartInColliders = false;

                    hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                    if (hit.collider != null && hit.collider.tag == "Grabbable")
                    {
                        grabbed = true;

                    }


                    //grab
                }
                else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
                {
                    grabbed = false;

                    if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    {

                        hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
                    }


                    //throw
                }

            }

            if (grabbed)
            {
                hit.collider.gameObject.transform.position = holdpoint.position;
            
            }
        }

	}

    public void setWhenSwitchSceneInMultiplayer()
    {
        multiplayer = false;
        Start();
    }

    void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}