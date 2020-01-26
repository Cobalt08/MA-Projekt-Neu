using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosTic : MonoBehaviour
{

    public GameObject PortalTic;
    // Start is called before the first frame update
    void Start()
    {
        PortalTic = FindObjectOfType<GameSession>().PortalTic;
        transform.position = new Vector2(PortalTic.transform.position.x, PortalTic.transform.position.y);
    }

    
}
