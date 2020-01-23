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
        if(numPlayers == 1)
        {
            hostConn = conn;
        }
        else
        {
            clientConn = conn;
        }
        // add player at correct spawn position
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
        // spawn ball if two players
        if (numPlayers == 2)
        {
            //GameObject.Find("ChooseMenu").GetComponent<MainMenu>().RpcChangeScene();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if(GameObject.Find("WaitMenu") != null)
            {
                GameObject.Find("WaitMenu").SetActive(false);
            }
            chooseMenu = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "ChooseMenu"));
            NetworkServer.Spawn(chooseMenu);

        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        //if (ball != null)
        //    NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        GameObject.Find("Arc").AddComponent<NetworkTransform>();
        GameObject.Find("Tic").AddComponent<NetworkTransform>();
        NetworkServer.ReplacePlayerForConnection(hostConn, GameObject.Find("Arc"));
        NetworkServer.ReplacePlayerForConnection(clientConn, GameObject.Find("Tic"));
    }

}
