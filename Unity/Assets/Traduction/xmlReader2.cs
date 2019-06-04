using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class xmlReader2 : MonoBehaviour
{
    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

    
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

    string Choix1;
    string Username;
    string Valider;
    
    
    
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
    
    public Text textChoix1;
    public Text textUsername;
    public Text textValider;
    

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

        
        languages[currentLanguage].TryGetValue("Choix1", out Choix1);
        languages[currentLanguage].TryGetValue("Username", out Username);
        languages[currentLanguage].TryGetValue("Valider", out Valider);
        
        

        textChoix.text = Choix;
        textNord.text = Nord;
        textJap.text = Jap;
        textEgy.text = Egy;
        textGre.text = Gre;


        textDescriN.text = DescriN;
        textDescriE.text = DescriE;
        textDescriJ.text = DescriJ;
        textDescriG.text = DescriG;
        textCapaN.text = CapaN;
        textCapaE.text = CapaE;
        textCapaJ.text = CapaJ;
        textCapaG.text = CapaG;

        textChoix1.text = Choix1;
        textUsername.text = Username;
        textValider.text = Valider;




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
                
                if (value.Name == "Choix1")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Valider")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Username")
                    obj.Add(value.Name, value.InnerText);
                
            }

            languages.Add(obj);
        }
    }
}

