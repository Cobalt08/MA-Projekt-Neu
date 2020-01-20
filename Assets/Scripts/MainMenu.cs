using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : NetworkBehaviour
{

    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = GameObject.Find("Network").GetComponent<NetworkManager>();
        Debug.Log("awake");
        this.transform.SetParent(GameObject.Find("CanvasMenu").transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localEulerAngles = Vector3.zero;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void CreateServer()
    {
        networkManager.StartHost();
    }

    public void JoinServer()
    {
        networkManager.networkAddress = GameObject.Find("IpAdressInput").GetComponent<Text>().text;
        networkManager.StartClient();
        //switch to Choose Menu
    }

    public void StopHost()
    {
        networkManager.StopHost();
    }

    [ClientRpc]
    public void RpcSwitchPlayers()
    {
        string temp = GameObject.Find("GameOwnerChar").GetComponent<TextMeshProUGUI>().text;
        GameObject.Find("GameOwnerChar").GetComponent<TextMeshProUGUI>().text = GameObject.Find("JointPlayerChar").GetComponent<TextMeshProUGUI>().text;
        GameObject.Find("JointPlayerChar").GetComponent<TextMeshProUGUI>().text = temp;
    }

    public void ChangeName(string name)
    {
        Debug.Log("change variable" + name);

    }

    [ClientRpc]
    public void RpcChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
