using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    Animator anim;
    public bool sticks;
    public bool isActive = false;
    public Switch Switch;


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
            anim.SetBool("isOn", true);
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
            anim.SetBool("isOn", false);
            isActive = false;
        }
        /*foreach (DoorTrigger trigger in doorTrig)
		{

			trigger.Toggle(false);

		}*/





    }
}
