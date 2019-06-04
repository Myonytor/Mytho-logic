using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkSelection : NetworkBehaviour
{
    public GameObject usernameGameobject;

    public GameObject IsReadyText;

    private bool isReady;
    private string username;
    public int indexPlayer;

    void Start()
    {
        isReady = false;
        indexPlayer = (PlayerPrefs.GetInt("online") == 2 ? 1 : 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterButton();
        }
    }

    public void ChangeScene(string scene)
    {
        NetworkManager.singleton.ServerChangeScene(scene);
    }

    public void RegisterButton()
    {
        username = usernameGameobject.GetComponent<InputField>().text;
        if (username != "")
        {
            string playprefs = "player" + indexPlayer;
            PlayerPrefs.SetString(playprefs, username);
            isReady = !isReady;
            IsReadyText.SetActive(isReady);
            
            Debug.Log(username);
        }
    }
    
    public void ButtonChoice(int mythology)
    {
        string playpref = "mythology" + indexPlayer;
        PlayerPrefs.SetInt(playpref, mythology);
    }
}
