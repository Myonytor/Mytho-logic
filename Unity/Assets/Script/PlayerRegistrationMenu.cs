using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.Return) && UsernameChoice.activeSelf)
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
            usernameGameobject.GetComponent<InputField>().text = "";
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
            StartCoroutine(LoadAsync());
    }

    public void ChangeIndexPlayer()
    {
        indexPlayer = 1;
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log("Chargement : " + progress);
            yield return null;
        }
    }
}
