using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ChangeScene : NetworkBehaviour
{


    [ClientRpc]
    public void RpcCloseWaitMenu()
    {
        if (GameObject.Find("WaitMenu") != null)
        {
            GameObject.Find("WaitMenu").SetActive(false);
        }
    }

    [TargetRpc]
    public void TargetSetupTic(NetworkConnection conn, Vector3 position)
    {
        GameObject TicM = GameObject.Find("MultiplayerTic(Clone)");
        TicM.GetComponent<Player>().activePlayer = true;
        TicM.transform.position = position;
        TicM.GetComponent<Player>().camera = GameObject.Find("CM vcam2");
        GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>().Follow = TicM.transform;
        TicM.GetComponent<Player>().setWhenSwitchSceneInMultiplayer();
        TicM.GetComponent<GrabberScript>().setWhenSwitchSceneInMultiplayer();
        Joystick weaponjoystick = GameObject.Find("Fixed Joystick 2").GetComponent<Joystick>();
        weaponjoystick.gameObject.SetActive(true);
    }
}
