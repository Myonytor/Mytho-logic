using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRegistrationMenu : MonoBehaviour
{
    public GameObject usernameGameobject;
    public GameObject UsernameChoice;
    public GameObject MythoChoice;

    private string username;
    public int indexPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    public void ButtonChoice(int choice)
    {
        string playpref = "mythology" + indexPlayer;
        PlayerPrefs.SetInt(playpref, choice);
        
        Debug.Log(choice);
    }
    
    public void Online(int choice)
    {
        PlayerPrefs.SetInt("online", choice);
    }
}
