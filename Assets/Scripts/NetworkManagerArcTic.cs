using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerArcTic : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    GameObject chooseMenu;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
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

}
