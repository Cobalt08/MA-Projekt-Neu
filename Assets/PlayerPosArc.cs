using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosArc : MonoBehaviour
{
    public GameObject PortalArc;
    // Start is called before the first frame update
    void Start()
    {
        PortalArc= FindObjectOfType<GameSession>().PortalTic;
        transform.position = new Vector2(PortalArc.transform.position.x, PortalArc.transform.position.y);
    }
}
