using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;



public partial class xmlReader : MonoBehaviour
{

    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;
    
    string Play;
    string Reseau;
    string Quit;
    string Volume;
    string Languages;
    string Graphism;
    string Back;
    

    public Text textPlay;
    public Text textReseau;
    public Text textQuit;
    public Text textVolume;
    public Text textLanguages;
    public Text textGraphism;
    public Text textBack;
    
    public Dropdown selectDropdown;

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    void Awake()
    {
        Reader();
    }

    void Update()
    {

        languages[currentLanguage].TryGetValue("Name", out languageName);
        languages[currentLanguage].TryGetValue("Play", out Play);
        languages[currentLanguage].TryGetValue("Reseau", out Reseau);
        languages[currentLanguage].TryGetValue("Quit", out Quit);
        languages[currentLanguage].TryGetValue("Volume", out Volume);
        languages[currentLanguage].TryGetValue("Languages", out Languages);
        languages[currentLanguage].TryGetValue("Graphism", out Graphism);
        languages[currentLanguage].TryGetValue("Back", out Back);


        textPlay.text = Play;
        textReseau.text = Reseau;
        textQuit.text = Quit;
        textVolume.text = Volume;
        textLanguages.text = Languages;
        textGraphism.text = Graphism;
        textBack.text = Back;




    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {

                if (value.Name == "Play")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Reseau")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Quit")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Volume")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Languages")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Graphism")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Back")
                    obj.Add(value.Name, value.InnerText);
            }

            languages.Add(obj);
        }
    }


    public void ValueChangeCheck()
    {
        currentLanguage = selectDropdown.value;

        PlayerPrefs.SetInt("lang", currentLanguage);

    }

}