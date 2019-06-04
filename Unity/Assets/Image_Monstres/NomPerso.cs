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

    public int indexPlayer;
    public int button;

    void Start()
    {
        currentLanguage = PlayerPrefs.GetInt("lang",0);

        string playerpref = "choice" + indexPlayer;
        currentMythologie = PlayerPrefs.GetInt(playerpref, 0);
        nom = "";

        button = 0;
    }

    void Update()
    {
        switch (button)
        {
            case 0:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            nom = "Draugr";
                            break;
                        case 1:
                            nom = "Tatsu";
                            break;
                        case 2:
                            nom = "Nout";
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
                            nom = "Draugr";
                            break;
                        case 1:
                            nom = "Tatsu";
                            break;
                        case 2:
                            nom = "Nout";
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
                break;
            
            case 1:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            nom = "Berserker";
                            break;
                        case 1:
                            nom = "Kirin";
                            break;
                        case 2:
                            nom = "Atoum";
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
                            nom = "Berserker";
                            break;
                        case 1:
                            nom = "Kirin";
                            break;
                        case 2:
                            nom = "Atoum";
                            break;
                        case 3:
                            nom = "Siren";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = nom;
                break;
            
            case 2:if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            nom = "Fenrir";
                            break;
                        case 1:
                            nom = "Kitsune";
                            break;
                        case 2:
                            nom = "Osiris";
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
                            nom = "Fenrir";
                            break;
                        case 1:
                            nom = "Kitsune";
                            break;
                        case 2:
                            nom = "Osiris";
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
                break;
            
            case 3:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            nom = "Troll";
                            break;
                        case 1:
                            nom = "Yuki-Onna";
                            break;
                        case 2:
                            nom = "Nephtys";
                            break;
                        case 3:
                            nom = "Cerbère";
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
                            nom = "Yuki-Onna";
                            break;
                        case 2:
                            nom = "Nephtys";
                            break;
                        case 3:
                            nom = "Cerberus";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = nom;
                break;
            
            case 4:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            nom = "Valkyrie";
                            break;
                        case 1:
                            nom = "Jorogumo";
                            break;
                        case 2:
                            nom = "Anubis";
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
                            nom = "Valkyrie";
                            break;
                        case 1:
                            nom = "Jorogumo";
                            break;
                        case 2:
                            nom = "Anubis";
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
                break;
            
            case 5:
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
                break;
            
            default:
                Debug.Log("Error at the button " + button);
                break;
        }
    }
}
