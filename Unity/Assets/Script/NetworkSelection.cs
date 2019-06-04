using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSelection : MonoBehaviour
{
    private string username;
    public int indexPlayer;

    void Start()
    {
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
    
    public void ButtonChoice(int mythology)
    {
        string playpref = "mythology" + indexPlayer;
        PlayerPrefs.SetInt(playpref, mythology);
    }
}
