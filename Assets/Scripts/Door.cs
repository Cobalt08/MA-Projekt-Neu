using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{


	//public DoorTrigger[] doorTrig;



	Animator anim;
	public bool sticks;
	public bool isActive = false;
	public Switch Switch;
	public GameObject PortalTic;
	public GameObject PlayerTic;
	public GameObject PortalArc;
	public GameObject PlayerArc;


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

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (isActive) {
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