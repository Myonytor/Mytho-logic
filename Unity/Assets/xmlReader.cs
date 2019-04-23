using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;

// Ce script a été rédigé par la chaîne Youtube TUTOUNITYFR, Pour savoir comment l'utiliser
// rendez-vous sur cette vidéo : https://www.youtube.com/watch?v=jVgukYyEURM

public class xmlReader : MonoBehaviour
{
	// Glissez ici votre dictionnaire XML
    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

	// Ces variables s'adaptent en fonction du langage utilisé
    string Play;
    string Load;
    string Options;
    string Quit;
    string Volume;
    string Languages;
    string KeyIn;
    string Graphism;
    string Back;
    
    

	// Ces 3 variables UI ne sont pas nécessaires et peuvent être supprimées (Voir vidéo tuto)
	// MAIS il vous faudra aussi supprimer les différentes lignes relatives à ces variables
    public Text textPlay;
    public Text textLoad;
    public Text textOptions;
    public Text textQuit;
    public Text textVolume;
    public Text textLanguages;
    public Text textKeyIn;
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
		// Pour chaque phrase / mot se trouvant dans votre dictionnaire, il vous faut
		// ajouter une ligne semblable aux suivantes en modifiant la valeur se trouvant 
		// entre les " " ainsi que le "out NomDeLaVariable"
        languages[currentLanguage].TryGetValue("Name", out languageName);
        languages[currentLanguage].TryGetValue("Play", out Play);
        languages[currentLanguage].TryGetValue("Load", out Load);
        languages[currentLanguage].TryGetValue("Options", out Options);
        languages[currentLanguage].TryGetValue("Quit", out Quit);
        languages[currentLanguage].TryGetValue("Volume", out Volume);
        languages[currentLanguage].TryGetValue("Languages", out Languages);
        languages[currentLanguage].TryGetValue("KeyIn", out KeyIn);
        languages[currentLanguage].TryGetValue("Graphism", out Graphism);
        languages[currentLanguage].TryGetValue("Back", out Back);


		// Ces lignes ne sont pas nécessaires vous pouvez les supprimer à condition 
		// d'avoir supprimer les variables UI plus haut (Voir vidéo)
        textPlay.text = Play;
        textLoad.text = Load;
        textOptions.text = Options;
        textQuit.text = Quit;
        textVolume.text = Volume;
        textLanguages.text = Languages;
        textKeyIn.text = KeyIn;
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
				// Ajouter ici une condition pour chaque expression / mot de votre dictionnaire
				// n'oubliez pas de remplacer la valeur entre les " " (Attention à la casse !)
                if (value.Name == "Play")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Load")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Options")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "Quit")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Volume")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Languages")
                    obj.Add(value.Name, value.InnerText);
                
                if (value.Name == "KeyIn")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Graphism")
                    obj.Add(value.Name, value.InnerText);

                if (value.Name == "Back")
                    obj.Add(value.Name, value.InnerText);
            }

            languages.Add(obj);
        }
    }

	// Fonction non nécessaire, vous pouvez la supprimer à condition d'avoir 
	// supprimé les variables UI.
    public void ValueChangeCheck()
    {
        currentLanguage = selectDropdown.value;
    }
}