using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NomPerso : MonoBehaviour
{
    private int currentLanguage;
    private int currentMythology;

    public int CurrentMythology => currentMythology;

    public Text NomP; 

    private string name;

    public int player;
    public int button;

    void Start()
    {
        currentLanguage = PlayerPrefs.GetInt("lang",0);

        string playerpref = "mythology" + player;
        currentMythology = PlayerPrefs.GetInt(playerpref, 0);
        name = "test";
        NomP.text = name;
    }

    void Update()
    {
        switch (button)
        {
            case 0:
                switch (currentMythology)
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
                        if (currentLanguage == 0) name = "Minotaure";
                        else name = "Minotaur";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }

                NomP.text = name;
                break;
            
            case 1:
                switch (currentMythology)
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
                        if (currentLanguage == 0) name = "Sirène";
                        else name = "Siren";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }

                NomP.text = name;
                break;
            
            case 2:
                switch (currentMythology)
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
                        if (currentLanguage == 0) name = "Harpie";
                        else name = "Harpy";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }
        
                NomP.text = name;
                break;
            
            case 3:
                switch (currentMythology)
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
                        if (currentLanguage == 0) name = "Cerbère";
                        else name = "Cerberus";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }

                NomP.text = name;
                break;
            
            case 4:
                switch (currentMythology)
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
                        if (currentLanguage == 0) name = "Hydre";
                        else name = "Hydra";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }

                NomP.text = name;
                break;
            
            case 5:
                switch (currentMythology)
                {
                    case 3:
                        if (currentLanguage == 0) name = "Sorcier";
                        else name = "Wizard";
                        break;
                    case 2:
                        name = "Furi";
                        break;
                    case 0:
                        name = "Isis";
                        break;
                    case 1:
                        if (currentLanguage == 0) name = "Méduse";
                        else name = "Medusa";
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }

                NomP.text = name;
                break;
            
            default:
                Debug.Log("Error at the button " + button);
                break;
        }
    }

    public void ChangePlayer(int index, int mythology)
    {
        player = index;
        currentMythology = mythology;
    }
}
