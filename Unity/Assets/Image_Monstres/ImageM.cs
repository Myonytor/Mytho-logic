using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ImageM : MonoBehaviour {
 
    public Sprite Nordique;
    public Sprite Japonaise;
    public Sprite Egyptienne;
    public Sprite Greque;
 
    public int imgNumberCount;

    private void Start()
    {
        imgNumberCount = (PlayerPrefs.GetInt("choix",0));
    }
    

    private void Update()
    {
        switch (imgNumberCount)
        {
 
            case 0:
                GetComponent<Image>().sprite = Nordique;
                break;
            case 1:
                GetComponent<Image>().sprite = Japonaise;
                break;
            case 2:
                GetComponent<Image>().sprite = Egyptienne;
                break;
            case 3:
                GetComponent<Image>().sprite = Greque;
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}