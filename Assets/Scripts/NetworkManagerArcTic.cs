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
    GameObject sceneChanger;

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
            sceneChanger = Instantiate(playerPrefab);
            DontDestroyOnLoad(sceneChanger);
            NetworkServer.Spawn(sceneChanger);

            sceneChanger.GetComponent<ChangeScene>().RpcCloseWaitMenu();

            chooseMenu = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ChooseMenu"));
            NetworkServer.Spawn(chooseMenu, hostConn);
        }
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        
        Transform spawnArc = GameObject.Find("Arc").transform;
        Destroy(GameObject.Find("Arc"));

        print("Scene changed " + conn.connectionId);

        GameObject.Find("SwitchPlayerButton").transform.position = new Vector3(-1000, -1000, -1000);

        if(ClientScene.localPlayer.isServer)
        {
            if (!ClientScene.ready) ClientScene.Ready(conn);
            SetupArc(spawnArc.position);
        }
        else
        {
            //if (!ClientScene.ready) ClientScene.Ready(conn);
            //SetupTic(spawnTic.position);
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        Transform spawnTic = GameObject.Find("Tic").transform;
        Destroy(GameObject.Find("Tic"));
        print(GameObject.Find("SceneChanger(Clone)"));
        sceneChanger.GetComponent<ChangeScene>().TargetSetupTic(ClientScene.localPlayer.connectionToClient, spawnTic.position);
    }

    public override void OnStartClient()
    {
        print("Start client");
        base.OnStartClient();
    }
    public override void OnStartHost()
    {
        print("Start host");
        base.OnStartHost();
    }

    private void SetupTic(Vector3 position)
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


    private void SetupArc(Vector3 position)
    {
        GameObject ArcM = GameObject.Find("MultiplayerArc(Clone)");
        ArcM.GetComponent<Player>().activePlayer = true;
        ArcM.transform.position = position;
        ArcM.GetComponent<Player>().camera = GameObject.Find("CM vcam1");
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = ArcM.transform;
        ArcM.GetComponent<Player>().setWhenSwitchSceneInMultiplayer();
        ArcM.GetComponent<GrabberScript>().setWhenSwitchSceneInMultiplayer();
        Joystick weaponjoystick = GameObject.Find("Fixed Joystick 2").GetComponent<Joystick>();
        weaponjoystick.gameObject.SetActive(false);
    }
}
