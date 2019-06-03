using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixMytho : MonoBehaviour
{
    
    public int choix;
    
    public void Nord()
    {
        choix = 0;
        PlayerPrefs.SetInt("choix", choix);
    }

    public void Jap()
    {
        choix = 1;
        PlayerPrefs.SetInt("choix", choix);
    }
    
    public void Egy()
    {
        choix = 2;
        PlayerPrefs.SetInt("choix", choix);
    }
    
    public void Gre()
    {
        choix = 3;
        PlayerPrefs.SetInt("choix", choix);
    }
    
}
