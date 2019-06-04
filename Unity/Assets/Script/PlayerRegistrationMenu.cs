﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRegistrationMenu : MonoBehaviour
{
    private string SceneToLoad = "BoardScene";
    
    public GameObject usernameGameobject;
    public GameObject UsernameChoice;
    public GameObject MythoChoice;

    private string username;
    public int indexPlayer;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterButton();
        }
    }

    public void RegisterButton()
    {
        username = usernameGameobject.GetComponent<InputField>().text;
        if (username != "")
        {
            string playprefs = "player" + indexPlayer;
            PlayerPrefs.SetString(playprefs, username);
            
            Debug.Log(username);
            UsernameChoice.SetActive(false);
            MythoChoice.SetActive(true);
        }
    }

    public void ButtonChoice(int mythology)
    {
        string playpref = "mythology" + indexPlayer;
        PlayerPrefs.SetInt(playpref, mythology);
        
        Debug.Log(mythology);
    }
    
    public void Online(int choice)
    {
        PlayerPrefs.SetInt("online", choice);
    }
    
    public void ChangeScene()
    {
        if (indexPlayer == 0 && PlayerPrefs.GetInt("online") == 0)
        {
            indexPlayer = 1;
            MythoChoice.SetActive(false);
            UsernameChoice.SetActive(true);
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }

    public void ChangeIndexPlayer()
    {
        indexPlayer = 1;
    }
}
