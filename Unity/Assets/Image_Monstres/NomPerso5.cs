using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NomPerso5 : MonoBehaviour
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

                    nom = "Sorcier";
                    break;
                case 1:
                    nom = "Furi";
                    break;
                case 2:
                    nom = "Isis";
                    break;
                case 3:
                    nom = "Méduse";
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

                    nom = "Wizard";
                    break;
                case 1:
                    nom = "Furi";
                    break;
                case 2:
                    nom = "Isis";
                    break;
                case 3:
                    nom = "Medusa";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        
        NomP.text = nom;
    }
}
