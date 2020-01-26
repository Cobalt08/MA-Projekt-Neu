using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

	public GameObject PortalTic;
	public GameObject PortalArc;
	
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			FindObjectOfType<GameSession>().PortalTic = this.PortalTic;
			FindObjectOfType<GameSession>().PortalArc = this.PortalArc;
		}
	}

}
