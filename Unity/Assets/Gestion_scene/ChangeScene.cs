using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public string SceneToLoad;

    void Start()
    {
        
    }

    public void changeScene()

    {
        SceneManager.LoadScene(SceneToLoad);
    }
    
    
}