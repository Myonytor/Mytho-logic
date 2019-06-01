using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Network_Manager : NetworkManager
{
    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public void JoinGame()
    {
        SetIPAdress();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAdress()
    {
        string ipAdress = GameObject.Find("inputFieldIPAdress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAdress;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level==0)
        {
            MenuSceneButton();
        }
        else
        {
            SceneButton();
        }

        void MenuSceneButton()
        {
            GameObject.Find("ButtonHost").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("ButtonHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        }

        void SceneButton()
        {
            
            GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

        }
    }
}
