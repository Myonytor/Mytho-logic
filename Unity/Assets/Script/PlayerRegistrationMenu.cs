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
            Debug.Log("Success Username");
            UsernameChoice.SetActive(false);
            MythoChoice.SetActive(true);
        }
    }
}
