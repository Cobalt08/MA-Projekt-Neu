using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerArcTic : NetworkManager
{
    public GameObject arc;
    public GameObject tic;
    GameObject chooseMenu;
    NetworkConnection hostConn;
    NetworkConnection clientConn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {



        if (numPlayers == 0)
        {
            GameObject playerArc = Instantiate(arc);
            DontDestroyOnLoad(playerArc);
            NetworkServer.AddPlayerForConnection(conn, playerArc);
            hostConn = conn;

        }
        else
        {
            GameObject playerTic = Instantiate(tic);
            DontDestroyOnLoad(playerTic);
            NetworkServer.AddPlayerForConnection(conn, playerTic);
            clientConn = conn;

        }

        GameObject.Find("WaitMenu").GetComponent<NetworkIdentity>().AssignClientAuthority(hostConn);
        
        // spawn choose menu if two players
        if (numPlayers == 2)
        {
            //This is the SceneChanger
            GameObject sceneChanger = Instantiate(playerPrefab);
            DontDestroyOnLoad(sceneChanger);
            NetworkServer.Spawn(sceneChanger);

            sceneChanger.GetComponent<ChangeScene>().RpcCloseWaitMenu();

            chooseMenu = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ChooseMenu"));
            NetworkServer.Spawn(chooseMenu, hostConn);
        }
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        print("Scene changed " + conn.connectionId);
        GameObject.Find("SwitchPlayerButton").transform.position = new Vector3(-1000, -1000, -1000);

    }

    public override void OnServerSceneChanged(string sceneName)
    {
        GameObject switchPlayer = GameObject.Find("SwitchPlayerButton");
        //SetupArc();
        //SetupTic();
        //switchPlayer.AddComponent<NetworkTransform>();
        //NetworkServer.Spawn(switchPlayer);
        //switchPlayer.transform.position = new Vector3(-1000, -1000, -1000);
        print(sceneName);
    }

    public void RpcSetupScene()
    {


        GameObject.Find("SwitchPlayerButton").transform.position = new Vector3(-1000, -1000, -1000);

    }

    private void SetupTic()
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


    private void SetupArc()
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
}
