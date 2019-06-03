using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NomPerso4 : MonoBehaviour
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

                    nom = "Berserk";
                    break;
                case 1:
                    nom = "Yuki-Onna";
                    break;
                case 2:
                    nom = "Atoum";
                    break;
                case 3:
                    nom = "Hydre";
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

                    nom = "Berserk";
                    break;
                case 1:
                    nom = "Yuki-Onna";
                    break;
                case 2:
                    nom = "Atoum";
                    break;
                case 3:
                    nom = "Hydra";
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
        
        NomP.text = nom;
    }
}
