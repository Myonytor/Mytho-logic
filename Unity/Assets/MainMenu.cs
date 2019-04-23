using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  
    public void QuitGame()
    {
        Debug.Log(("Quit")); // ecrit dans la console que l'on quit le jeu
        Application.Quit();
    }
    
}
