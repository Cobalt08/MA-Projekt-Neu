using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
	public bool isActive = false;

    // Use this for initialization
    void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnTriggerStay2D()
	{
		anim.SetBool("goDown", true);
		isActive = true;

        /*foreach (DoorTrigger trigger in doorTrig)
        {

            trigger.Toggle(true);

        }
        */
    }

	void OnTriggerExit2D()
	{
		if (sticks)
			return;

		anim.SetBool("goDown", false);
		isActive = false;

		/*foreach (DoorTrigger trigger in doorTrig)
		{

			trigger.Toggle(false);

		}*/





	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;

		/*foreach (DoorTrigger trigger in doorTrig)
		{

			Gizmos.DrawLine(transform.position, trigger.transform.position);

		}*/


	}


}