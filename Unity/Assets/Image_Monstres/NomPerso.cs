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

    private string name;

    public int indexPlayer;
    public int button;

    void Start()
    {
        currentLanguage = PlayerPrefs.GetInt("lang",0);

        string playerpref = "mythology" + indexPlayer;
        currentMythologie = PlayerPrefs.GetInt(playerpref, 0);
        name = "test";
        NomP.text = name;
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
                            name = "Draugr";
                            break;
                        case 1:
                            name = "Tatsu";
                            break;
                        case 2:
                            name = "Nout";
                            break;
                        case 3:
                            name = "Minotaure";
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
                            name = "Draugr";
                            break;
                        case 1:
                            name = "Tatsu";
                            break;
                        case 2:
                            name = "Nout";
                            break;
                        case 3:
                            name = "Minotaur";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            case 1:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            name = "Berserker";
                            break;
                        case 1:
                            name = "Kirin";
                            break;
                        case 2:
                            name = "Atoum";
                            break;
                        case 3:
                            name = "Sirène";
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
                            name = "Berserker";
                            break;
                        case 1:
                            name = "Kirin";
                            break;
                        case 2:
                            name = "Atoum";
                            break;
                        case 3:
                            name = "Siren";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            case 2:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            name = "Fenrir";
                            break;
                        case 1:
                            name = "Kitsune";
                            break;
                        case 2:
                            name = "Osiris";
                            break;
                        case 3:
                            name = "Harpie";
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
                            name = "Fenrir";
                            break;
                        case 1:
                            name = "Kitsune";
                            break;
                        case 2:
                            name = "Osiris";
                            break;
                        case 3:
                            name = "Harpy";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            case 3:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            name = "Troll";
                            break;
                        case 1:
                            name = "Yuki-Onna";
                            break;
                        case 2:
                            name = "Nephtys";
                            break;
                        case 3:
                            name = "Cerbère";
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
                            name = "Troll";
                            break;
                        case 1:
                            name = "Yuki-Onna";
                            break;
                        case 2:
                            name = "Nephtys";
                            break;
                        case 3:
                            name = "Cerberus";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            case 4:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            name = "Valkyrie";
                            break;
                        case 1:
                            name = "Jorogumo";
                            break;
                        case 2:
                            name = "Anubis";
                            break;
                        case 3:
                            name = "Hydre";
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
                            name = "Valkyrie";
                            break;
                        case 1:
                            name = "Jorogumo";
                            break;
                        case 2:
                            name = "Anubis";
                            break;
                        case 3:
                            name = "Hydra";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            case 5:
                if (currentLanguage == 0)
                {
                    switch (currentMythologie)
                    {
                        case 0:
                            name = "Sorcier";
                            break;
                        case 1:
                            name = "Furi";
                            break;
                        case 2:
                            name = "Isis";
                            break;
                        case 3:
                            name = "Méduse";
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
                            name = "Wizard";
                            break;
                        case 1:
                            name = "Furi";
                            break;
                        case 2:
                            name = "Isis";
                            break;
                        case 3:
                            name = "Medusa";
                            break;
                        default:
                            Debug.Log("Error");
                            break;
                    }
                }
        
                NomP.text = name;
                break;
            
            default:
                Debug.Log("Error at the button " + button);
                break;
        }
    }
}
