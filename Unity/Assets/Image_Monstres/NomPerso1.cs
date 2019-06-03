﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NomPerso1 : MonoBehaviour
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
                    nom = "Kirin";
                    break;
                case 2:
                    nom = "Nephtys";
                    break;
                case 3:
                    nom = "Sirène";
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
                    nom = "Kirin";
                    break;
                case 2:
                    nom = "Nephtys";
                    break;
                case 3:
                    nom = "Sirene";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        
        NomP.text = nom;
    }
}
