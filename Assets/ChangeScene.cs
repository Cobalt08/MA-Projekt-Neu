using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : NetworkBehaviour
{
    [ClientRpc]
    public void RpcSetupScene()
    {

        CmdSetupTic();
        CmdSetupArc();
        GameObject.Find("SwitchPlayerButton").transform.position = new Vector3(-1000, -1000, -1000);

    }

    [Command]
    private void CmdSetupTic()
    {
        Transform spawnTic = GameObject.Find("Tic").transform;
        GameObject TicM = GameObject.Find("MultiplayerTic(Clone)");
        TicM.transform.position = spawnTic.position;
        Destroy(GameObject.Find("Tic"));
        TicM.GetComponent<Player>().camera = GameObject.Find("CM vcam2");
        GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>().Follow = TicM.transform;
        TicM.GetComponent<Player>().setWhenSwitchSceneInMultiplayer();
        TicM.GetComponent<GrabberScript>().setWhenSwitchSceneInMultiplayer();
        Joystick weaponjoystick = GameObject.Find("Fixed Joystick 2").GetComponent<Joystick>();
        weaponjoystick.gameObject.SetActive(true);

    }


    [Command]
    private void CmdSetupArc()
    {
        Transform spawnArc = GameObject.Find("Arc").transform;
        GameObject ArcM = GameObject.Find("MultiplayerArc(Clone)");
        ArcM.transform.position = spawnArc.position;
        Destroy(GameObject.Find("Arc"));
        ArcM.GetComponent<Player>().camera = GameObject.Find("CM vcam1");
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = ArcM.transform;
        ArcM.GetComponent<Player>().setWhenSwitchSceneInMultiplayer();
        ArcM.GetComponent<GrabberScript>().setWhenSwitchSceneInMultiplayer();

    }

    [ClientRpc]
    public void RpcCloseWaitMenu()
    {
        if (GameObject.Find("WaitMenu") != null)
        {
            GameObject.Find("WaitMenu").SetActive(false);
        }
    }
}
