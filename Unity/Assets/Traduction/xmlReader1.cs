using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;


public class xmlReader1 : MonoBehaviour
{
    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

    string Quit;
    string Back;
    string Passer;
    string Temps;
    string Quit1;
    string ReturnM;
    
    public Text textPasser;
    public Text textTemps;
    public Text textQuit;
    public Text textBack;
    public Text textQuit1;
    public Text textReturnM;
    

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

	 void Start()
	{
		currentLanguage = (PlayerPrefs.GetInt("lang",0));
	}


    void Awake()
    {
        Reader();
    }

    void Update()
    {

        languages[currentLanguage].TryGetValue("Name", out languageName);

        languages[currentLanguage].TryGetValue("Quit", out Quit);
        languages[currentLanguage].TryGetValue("Quit1", out Quit1);
        languages[currentLanguage].TryGetValue("Back", out Back);
        
        languages[currentLanguage].TryGetValue("Passer", out Passer);
        languages[currentLanguage].TryGetValue("Temps", out Temps);
        
        languages[currentLanguage].TryGetValue("ReturnM", out ReturnM);

        textQuit.text = Quit;
        textQuit1.text = Quit1;
        textBack.text = Back;
        
        
        textPasser.text = Passer;
        textTemps.text = Temps;
        textReturnM.text = ReturnM;




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

                if (value.Name == "Quit")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Back")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Quit1")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Temps")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Passer")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "ReturnM")
                    obj.Add(value.Name, value.InnerText);
                
            }

            languages.Add(obj);
        }
    }

}