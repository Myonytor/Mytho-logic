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
    string Choix;
    string Nord;
    string Jap;
    string Egy;
    string Gre;
    string CapaN;
    string CapaJ;
    string CapaG;
    string CapaE;
    string DescriN;
    string DescriJ;
    string DescriG;
    string DescriE;
    string Passer;
    string Temps;
    
    
    
    


    public Text textQuit;
    public Text textBack;
    public Text textChoix;
    public Text textNord;
    public Text textJap;
    public Text textEgy;
    public Text textGre;
    public Text textCapaN;
    public Text textCapaJ;
    public Text textCapaG;
    public Text textCapaE;
    public Text textDescriN;
    public Text textDescriJ;
    public Text textDescriG;
    public Text textDescriE;
    public Text textPasser;
    public Text textTemps;

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
        languages[currentLanguage].TryGetValue("Back", out Back);
        languages[currentLanguage].TryGetValue("Choix", out Choix);
        languages[currentLanguage].TryGetValue("Nord", out Nord);
        languages[currentLanguage].TryGetValue("Jap", out Jap);
        languages[currentLanguage].TryGetValue("Egy", out Egy);
        languages[currentLanguage].TryGetValue("Gre", out Gre);
        languages[currentLanguage].TryGetValue("CapaN", out CapaN);
        languages[currentLanguage].TryGetValue("CapaE", out CapaE);
        languages[currentLanguage].TryGetValue("CapaJ", out CapaJ);
        languages[currentLanguage].TryGetValue("CapaG", out CapaG);
        languages[currentLanguage].TryGetValue("DescriN", out DescriN);
        languages[currentLanguage].TryGetValue("DescriE", out DescriE);
        languages[currentLanguage].TryGetValue("DescriJ", out DescriJ);
        languages[currentLanguage].TryGetValue("DescriG", out DescriG);
        languages[currentLanguage].TryGetValue("Passer", out Passer);
        languages[currentLanguage].TryGetValue("Temps", out Temps);

        textQuit.text = Quit;
        textBack.text = Back;
        textChoix.text = Choix;
        textNord.text = Nord;
        textJap.text = Jap;
        textEgy.text = Egy;
        textGre.text = Gre;
        
        textPasser.text = Passer;
        textTemps.text = Temps;


        textDescriN.text = DescriN;
        textDescriE.text = DescriE;
        textDescriJ.text = DescriJ;
        textDescriG.text = DescriG;
        textCapaN.text = CapaN;
        textCapaE.text = CapaE;
        textCapaJ.text = CapaJ;
        textCapaG.text = CapaG;




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
                
                if (value.Name == "Choix")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Nord")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Jap")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Egy")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Gre")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "DescriN")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "DescriJ")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "DescriE")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "DescriG")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "CapaN")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "CapaJ")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "CapaE")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "CapaG")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Temps")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Passer")
                    obj.Add(value.Name, value.InnerText);
            }

            languages.Add(obj);
        }
    }

}