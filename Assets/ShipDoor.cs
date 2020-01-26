﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoor : MonoBehaviour
{
	
	
	public bool isActive = true;
	
	public GameObject PortalTic;
	public GameObject PlayerTic;
	public GameObject PortalArc;
	public GameObject PlayerArc;


	// Use this for initialization
	void Start()
	{
		

	}

	// Update is called once per frame
	void Update()
	{
		
	}


	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (isActive)
		{
			StartCoroutine(Teleport());
		}
	}

	IEnumerator Teleport()
	{
		yield return new WaitForSeconds(0.5f);
		PlayerTic.transform.position = new Vector2(PortalTic.transform.position.x, PortalTic.transform.position.y);
		PlayerArc.transform.position = new Vector2(PortalArc.transform.position.x, PortalArc.transform.position.y);
	}

}
