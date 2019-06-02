using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRegistrationMenu : MonoBehaviour
{
    public GameObject usernameGameobject;
    private string username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        username = usernameGameobject.GetComponent<InputField>().text;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(username != "")
            {
                RegisterButton();
            }
        }
    }

    public void RegisterButton()
    {
        Debug.Log("Success Username");
    }
}
