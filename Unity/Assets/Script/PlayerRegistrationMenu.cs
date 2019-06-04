using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRegistrationMenu : MonoBehaviour
{
    private string SceneToLoad = "BoardScene";
    
    public GameObject usernameGameobject;
    public GameObject UsernameChoice;
    public GameObject MythoChoice;
    public GameObject loadingScreen;
    public Slider slider;
    public Text LoadText;

    private string username;
    public int indexPlayer;
    
    public GameObject TDisplay;
    public Text Display1;
    public Text Display2;
    public int currentLangage;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && UsernameChoice.activeSelf)
        {
            RegisterButton();
        }
    }

    // Sauvegarde ce que le joueur tape pour son pseudo
    public void RegisterButton()
    {
        username = usernameGameobject.GetComponent<InputField>().text;
        if (username != "")
        {
            string playprefs = "player" + indexPlayer;
            PlayerPrefs.SetString(playprefs, username);
            
            UsernameChoice.SetActive(false);
            MythoChoice.SetActive(true);
            usernameGameobject.GetComponent<InputField>().text = "";
        }
    }

    // Enregistre le choix de la mythology du joueur
    public void ButtonChoice(int mythology)
    {
        string playpref = "mythology" + indexPlayer;
        PlayerPrefs.SetInt(playpref, mythology);
    }
    
    // Enregistre si le jeu se passe en ligne où sur le même ordinateur
    public void Online(int choice)
    {
        PlayerPrefs.SetInt("online", choice);
    }
    
    // Rejoue les scènes de sélection de la mythology et du surnom du joueur si besoin sinon passe à la scène suivante
    public void ChangeScene()
    {
        if (indexPlayer == 0 && PlayerPrefs.GetInt("online") == 0)
        {
            indexPlayer = 1;
            MythoChoice.SetActive(false);
            UsernameChoice.SetActive(true);
        }
        else
            StartCoroutine(LoadAsync());
    }

    // Change l'index du joueur
    public void ChangeIndexPlayer()
    {
        indexPlayer = 1;
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad);

        MythoChoice.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            LoadText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
    }
    
    private void SetScreenStartGame()
    {
        string a;
        string name0 = PlayerPrefs.GetString("player0", "Player 1"), name1 = PlayerPrefs.GetString("player1", "Player 2");
        currentLangage = PlayerPrefs.GetInt("lang",0);
        TDisplay.SetActive(true);

        if (currentLangage == 0)
        {
            Display1.text = name0 + " a choisi la mythologie " + Players[0].Mythologie.Name;
            Display2.text = Players[1].Name + " a choisi la mythologie " + Players[1].Mythologie.Name;
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                switch ((int) Players[i].Mythologie.Name)
                {
                    case 0:
                        a = "Egyptian";
                        break;

                    case 1:
                        a = "Greek";
                        break;

                    case 2:
                        a = "Japanese";
                        break;

                    case 3:
                        a = "Nordic";
                        break;

                    default:
                        Debug.Log("Error switch");
                        break;
                }

                if (i == 0)
                {
                    Display1.text = Players[0].Name + " choose the " + a + " mythology";
                    ;
                }
                else
                {
                    Display2.text = Players[1].Name + " choose the " + a + " mythology";
                }
            }
        }
    }
}
