using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using UnityEditor;
using UnityEngine.UI;

public class XmlLanguagesConv : MonoBehaviour
{

    public TextAsset dictionary;

    public string LanguageName;
    public int CurrentLanguage;  //FR 0 & EN 1

    string Play;
    string Options;
    string Save;
    string Quit;
    string Volume;
    string Graphics;
    string Key_inputs;
    string Langue;

    private List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    private Dictionary<string, string> obj;

    public Text textPlay;
    public Text textOptions;
    public Text textSave;
    public Text textQuit;
    public Text textVolume;
    public Text textGraphics;
    public Text textKey_Inputs;
    public Text textLangue;
    
    public Dropdown SelectDropdown;
    
    
    private void Awake()
    {
        Reader();
    }
    
    // Update is called once per frame
    void Update()
    {
        languages[CurrentLanguage].TryGetValue("Name", out LanguageName);
        languages[CurrentLanguage].TryGetValue("Play", out Play);
        languages[CurrentLanguage].TryGetValue("Options", out Options);
        languages[CurrentLanguage].TryGetValue("Save", out Save);
        languages[CurrentLanguage].TryGetValue("Quit", out Quit);
        languages[CurrentLanguage].TryGetValue("Volume", out Volume);
        languages[CurrentLanguage].TryGetValue("Graphics", out Graphics);
        languages[CurrentLanguage].TryGetValue("Key_inputs", out Key_inputs);
        languages[CurrentLanguage].TryGetValue("Langue", out Langue);

        /*textPlay.text = Play;
        textOptions.text = Options;
        textSave.text = Save;
        textQuit.text = Quit;
        textVolume.text = Volume;
        textGraphics.text = Graphics;
        textKey_Inputs.text = Key_inputs;
        textLangue.text = Langue;*/




    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language"); // cherche les rubriques language

        foreach (XmlNode languageValue in languageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes; // recupère toutes les sous baliste (mots de language)
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {
                if (value.Name == "Name")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Play")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Options")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Save")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Quit")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Volume")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Graphics")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Key_inputs")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                if (value.Name == "Langue")
                {
                    obj.Add(value.Name, value.InnerText);
                }
                
                
                
            }

            languages.Add(obj);
        }
        
    }

    public void ValueChangeCheck()
    {
        CurrentLanguage = SelectDropdown.value ;
    }
}
