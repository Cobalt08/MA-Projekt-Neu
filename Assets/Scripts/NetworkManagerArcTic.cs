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

        // spawn choose menu if two players
        if (numPlayers == 2)
        {
            //This is the SceneChanger
            GameObject sceneChanger = Instantiate(playerPrefab);
            DontDestroyOnLoad(sceneChanger);
            NetworkServer.Spawn(sceneChanger);

            sceneChanger.GetComponent<ChangeScene>().RpcCloseWaitMenu();

            chooseMenu = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ChooseMenu"));
            NetworkServer.Spawn(chooseMenu, clientConn);
        }
    }

}
