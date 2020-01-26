using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class NetworkManagerNew : NetworkManager
{

    public GameObject arc;
    public GameObject tic;
    GameObject chooseMenu;
    NetworkConnection hostConn;
    NetworkConnection clientConn;
    private int counter = 0;


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        print("connection " + conn);
        if (counter %2 == 0)
        {
            GameObject playerArc = Instantiate(arc);
            NetworkServer.AddPlayerForConnection(conn, playerArc);
            hostConn = conn;
            position(conn);

        }
        else
        {
            GameObject playerTic = Instantiate(tic);
            NetworkServer.AddPlayerForConnection(conn, playerTic);
            clientConn = conn;
            position(conn);

        }
        counter++;

        // spawn choose menu if two players
        if (numPlayers == 2 && networkSceneName == "Menu")
        {

            GameObject.Find("WaitMenu").GetComponent<NetworkIdentity>().AssignClientAuthority(hostConn);
            //This is the SceneChanger

            chooseMenu = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ChooseMenu"));
            NetworkServer.Spawn(chooseMenu, hostConn);
            chooseMenu.GetComponent<ChangeScene>().RpcCloseWaitMenu();

        }
    }

    private void position(NetworkConnection conn)
    {
        if (GameObject.Find("StartPos1") != null)
        {
            GameObject.Find("Arc").GetComponent<Player>().activePlayer = false;
            //need to be done activate Joystick 2;
            GameObject.Find("Tic").GetComponent<Player>().activePlayer = true;
            GameObject.Find("Tic").GetComponent<Player>().AttackButtonHandling();
            GameObject.Find("Tic").GetComponent<Player>().CameraHandling();
            GameObject.Find("Arc").GetComponent<Player>().CameraHandling();

            Destroy(GameObject.Find("Arc"));
            Destroy(GameObject.Find("Tic"));

            conn.identity.gameObject.transform.position = GameObject.Find("StartPos1").transform.position;
            print(GameObject.Find("Fixed Joystick 2"));
            print(conn.identity.gameObject.GetComponent<Player>());
            if (conn.identity.gameObject.name.Equals("MultiplayerArc(Clone)"))
            {
                SetupArc(conn.identity.gameObject);
            }
        }


    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {

        GameObject.Find("SwitchPlayerButton").transform.position = new Vector3(-1000, -1000, -1000);

        if (!ClientScene.ready)
        {
            ClientScene.Ready(NetworkClient.connection);
            if (ClientScene.localPlayer == null)
            {
                ClientScene.AddPlayer();
            }
        }
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


    private void SetupArc(GameObject ArcM)
    {
        ArcM.GetComponent<Player>().activePlayer = true;
        ArcM.GetComponent<Player>().camera = GameObject.Find("CM vcam1");
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = ArcM.transform;
        //GameObject.Find("CM vcam2").SetActive(false);
        ArcM.GetComponent<Player>().setWhenSwitchSceneInMultiplayer();
        ArcM.GetComponent<GrabberScript>().setWhenSwitchSceneInMultiplayer();
    }
}
