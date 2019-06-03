using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NomPerso2 : MonoBehaviour
{
    private int currentLanguage;
    private int currentMythologie;

    public Text NomP; 
    

    private string nom;

    
    void Start()
    {
        currentLanguage = PlayerPrefs.GetInt("lang",0);
        currentMythologie = PlayerPrefs.GetInt("choix", 0);
        nom = "";
    }

    void Update()
    {

        if (currentLanguage == 0)
        {
            switch (currentMythologie)
            {

                case 0:

                    nom = "Valkyrie";
                    break;
                case 1:
                    nom = "Jorugumo";
                    break;
                case 2:
                    nom = "Anubis";
                    break;
                case 3:
                    nom = "Harpie";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }

        else
        {
            switch (currentMythologie)
            {

                case 0:

                    nom = "Valkyrie";
                    break;
                case 1:
                    nom = "Jorugumo";
                    break;
                case 2:
                    nom = "Anubis";
                    break;
                case 3:
                    nom = "Harpy";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        
        NomP.text = nom;
    }
}
