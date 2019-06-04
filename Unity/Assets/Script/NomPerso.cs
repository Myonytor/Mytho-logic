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
                        case 3:
                            name = "Draugr";
                            break;
                        case 2:
                            name = "Tatsu";
                            break;
                        case 0:
                            name = "Nout";
                            break;
                        case 1:
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
                        case 3:
                            name = "Draugr";
                            break;
                        case 2:
                            name = "Tatsu";
                            break;
                        case 0:
                            name = "Nout";
                            break;
                        case 1:
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
                        case 3:
                            name = "Berserker";
                            break;
                        case 2:
                            name = "Kirin";
                            break;
                        case 0:
                            name = "Atoum";
                            break;
                        case 1:
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
                        case 3:
                            name = "Berserker";
                            break;
                        case 2:
                            name = "Kirin";
                            break;
                        case 0:
                            name = "Atoum";
                            break;
                        case 1:
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
                        case 3:
                            name = "Fenrir";
                            break;
                        case 2:
                            name = "Kitsune";
                            break;
                        case 0:
                            name = "Osiris";
                            break;
                        case 1:
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
                        case 3:
                            name = "Fenrir";
                            break;
                        case 2:
                            name = "Kitsune";
                            break;
                        case 0:
                            name = "Osiris";
                            break;
                        case 1:
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
                        case 3:
                            name = "Troll";
                            break;
                        case 2:
                            name = "Yuki-Onna";
                            break;
                        case 0:
                            name = "Nephtys";
                            break;
                        case 1:
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
                        case 3:
                            name = "Troll";
                            break;
                        case 2:
                            name = "Yuki-Onna";
                            break;
                        case 0:
                            name = "Nephtys";
                            break;
                        case 1:
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
                        case 3:
                            name = "Valkyrie";
                            break;
                        case 2:
                            name = "Jorogumo";
                            break;
                        case 0:
                            name = "Anubis";
                            break;
                        case 1:
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
                        case 3:
                            name = "Valkyrie";
                            break;
                        case 2:
                            name = "Jorogumo";
                            break;
                        case 0:
                            name = "Anubis";
                            break;
                        case 1:
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
                        case 3:
                            name = "Sorcier";
                            break;
                        case 2:
                            name = "Furi";
                            break;
                        case 0:
                            name = "Isis";
                            break;
                        case 1:
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
                        case 3:
                            name = "Wizard";
                            break;
                        case 2:
                            name = "Furi";
                            break;
                        case 0:
                            name = "Isis";
                            break;
                        case 1:
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
