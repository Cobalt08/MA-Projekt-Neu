using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
	public bool isActive = false;

    public GameObject[] destroyObjects;
    public GameObject[] connected;

    private bool destroyed = false;
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
		isActive = true;

        foreach (GameObject sw in connected)
        {
            if (sw.GetComponent<Switch>() != null)
            {
                if (!sw.GetComponent<Switch>().isActive)
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