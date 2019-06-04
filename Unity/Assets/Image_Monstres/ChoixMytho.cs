using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixMytho : MonoBehaviour
{

    public int choix;
    public int choix1;

    public void Nord()
    {
        choix = 0;
    }

    public void Jap()
    {
        choix = 1;
    }

    public void Egy()
    {
        choix = 2;
    }

    public void Gre()
    {
        choix = 3;
    }

    public void Mytho0()
    {
        PlayerPrefs.SetInt("choix0", choix);
    }

    public void Mytho1()
    {
        choix1 = choix;
        PlayerPrefs.SetInt("choix1", choix1);
    }
}
