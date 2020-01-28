using UnityEngine;
using System.Collections;

public class Door2 : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
	public bool isActive = false;
	public Switch Switch0; 
	public Switch Switch1;


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{
		Open();
		Close();
	}


	void Open()
	{
		if (Switch1.isActive && Switch0.isActive)
		{
			anim.SetBool("goUp", true);
			isActive = true;
		}
		/*foreach (DoorTrigger trigger in doorTrig)
		{

			trigger.Toggle(true);

		}
		*/

	}

	void Close()
	{
		if (sticks)
			return;

		if (!Switch1.isActive || !Switch0.isActive)
		{
			anim.SetBool("goUp", false);
			isActive = false;
		}
		/*foreach (DoorTrigger trigger in doorTrig)
		{

			trigger.Toggle(false);

		}*/





	}



}