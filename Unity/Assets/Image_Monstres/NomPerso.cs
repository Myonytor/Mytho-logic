using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class NomPerso : MonoBehaviour
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

                    nom = "Troll";
                    break;
                case 1:
                    nom = "Tatsu";
                    break;
                case 2:
                    nom = "Isis";
                    break;
                case 3:
                    nom = "Minotaure";
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
                    nom = "Troll";
                    break;
                case 1:
                    nom = "Tatsu";
                    break;
                case 2:
                    nom = "Isis";
                    break;
                case 3:
                    nom = "Minotaur";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        NomP.text = nom;

        
    }
    
}
