using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() // on change de scène 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        // File build settings index + 1 index menu 0 index Game 1 et 0+1 = 1
    }

    public void QuitGame()
    {
        Debug.Log(("Quit")); // ecrit dans la console que l'on quit le jeu
        Application.Quit();
    }
    
}
