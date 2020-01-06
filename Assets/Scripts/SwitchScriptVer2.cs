using UnityEngine;
using System.Collections;

public class SwitchScriptVer2 : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
    public GameObject[] destroyObjects;
    public GameObject[] connected;

    private bool destroyed = false;
    private bool pressed = false;
    private bool allPressed = true;


    // Use this for initialization
    void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}


	void OnTriggerStay2D()
	{
		anim.SetBool("goDown", true);
        pressed = true;
        foreach (GameObject sw in connected)
        {
            if(sw.GetComponent<SwitchScriptVer2>() != null)
            {
                if (!sw.GetComponent<SwitchScriptVer2>().pressed)
                {
                    allPressed = false;
                }
            }
            else
            {
                allPressed = false;
            }
        }

        if (allPressed)
        {
            allPressed = false;
            if (!destroyed)
            {
                foreach (GameObject obj in destroyObjects)
                {
                    Destroy(obj);
                }
                destroyed = true;
            }

            /*foreach (DoorTrigger trigger in doorTrig)
            {

                trigger.Toggle(true);

            }
            */
        }
        else
        {
            allPressed = true;
        }
    }

	void OnTriggerExit2D()
	{
        
        if (sticks)
			return;
        pressed = false;
        anim.SetBool("goDown", false);

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