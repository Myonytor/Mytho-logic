using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Network_Manager : NetworkManager
{
    // NetworkManager.ServerChangeScene()

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
        //NetworkManager.ServerChangeScene();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIP").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }
}
