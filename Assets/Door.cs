using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
	public bool isActive = false;
	public SwitchScriptVer3 Switch;


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
		if (Switch.isActive)
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

		if (!Switch.isActive)
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