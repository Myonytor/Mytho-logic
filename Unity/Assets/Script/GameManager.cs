﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Script;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.Experimental.UIElements.Image;

public class GameManager : MonoBehaviour
{
    private const int timer = 60;
    public Board board;
    public int indexPlayer;
    public MouseManager mouse;
    
    private float decompte;

    public List<GameObject> prefabMythology;
    public List<GameObject> PrefabsMonsters;
    public List<Player> Players;
    
    public List<GameObject> Buttons;
    public GameObject ButtonMythology;
        
    public GameObject prefabParticle;
    public GameObject pauseMenu;
    public GameObject newTurnPanel;
    public GameObject endGamePanel;
    public GameObject BMenu;
    public GameObject BRun;

    public Text timeText;
    public Text newTurnText;
    public Text endGame;
    public Text B1;
    public Text B2;
    public bool skipTurn;

    public GameObject AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.GetComponent<AudioManager>().Play("MainMusic");
        //PrefabsMonsters[0].GetComponent<SpriteRenderer>().sprite;
        decompte = timer;
        indexPlayer = (PlayerPrefs.GetInt("online") == 2 ? 1 : 0);
        board.Setup();
        mouse.goal = board.goal;
        mouse.onMenu = pauseMenu.activeSelf;

        // Entré des noms des joueurs et de leurs mythologies
        int mytho0 = PlayerPrefs.GetInt("mythology0", 0);
        int mytho1 = PlayerPrefs.GetInt("mythology1", 1);

        string name0 = PlayerPrefs.GetString("player0", "Player 1");
        string name1 = PlayerPrefs.GetString("player1", "Player 2");
        
        GameObject player = new GameObject("Player");
        GameObject player0 = new GameObject(name0);
        player0.transform.parent = player.transform;
        GameObject player1 = new GameObject(name1);
        player1.transform.parent = player.transform;
        
        /*
         * Correspondance pour les mythologies :
         * 0 = Egyptienne
         * 1 = Grecque
         * 2 = Japonaise
         * 3 = Nordique
         */
        Players = new List<Player>()
            {
                new Player(name0, "Spawn1", CreateList(mytho0), player0, prefabParticle, mytho0, 0),
                new Player(name1, "Spawn2", CreateList(mytho1), player1, prefabParticle, mytho1, 1)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        ChangeSpriteButton();
        NewTurn();
        PlayerPrefs.DeleteAll();
    }

    // Crée la liste de monstres correspondant à la mythologie donnée
    private List<GameObject> CreateList(int mythology)
    {
        int start = mythology * 12 + 6;
        List<GameObject> prefabsMythology = PrefabsMonsters.GetRange(start, 12);
        prefabsMythology.AddRange(PrefabsMonsters.GetRange(0, 6));

        return prefabsMythology;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Détecter le bouton échap correspondant au bouton pause
        {
            if (pauseMenu.activeSelf)
                Resume();
            else
                Pause();
        }

        if (!pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Space)) // Détecter le bouton espace correspondant au bouton passer
        {
            if(newTurnPanel.activeSelf)
                StartNewTurn();
            else
                skipTurnFunc();
        }

        if((int)(decompte - Time.deltaTime) != (int)(decompte)) // Affiche le temps qui s'écoule en seconde
        {
            timeText.text = "" + (int)(decompte - Time.deltaTime);
        }
        decompte -= Time.deltaTime;

        if (decompte <= 0 || skipTurn) // Fin du timer
        {
            mouse.Clear();
            decompte = timer;
            if (indexPlayer == 1 || PlayerPrefs.GetInt("online") != 0)
                // Si le tour du deuxième joueur vient de se finir où le jeu se déroule sur le même ordinateur
            {
                NextBoard();
                int w = 0;
                
                for(int i = 0; i < Players.Count; i++) // Vérifie si il y a un gagnant
                {
                    foreach (var m in Players[i]._monsters)
                    {
                        for (int j = 0; j < board.goal.Length; j++)
                        {
                            if (board.goal[j] == m._position)
                                w += (i == 0 ? 1 : -1);
                        }
                    }

                    if (w != 0)
                        i = Players.Count;
                }

                if (Math.Abs(w) == 3) // Affichage du gagnant et du menu qui va avec
                {
                    AudioManager.GetComponent<AudioManager>().Play("Fireworks");
                    endGame.text = (w > 0 ? Players[0].Name : Players[1].Name) + " WIN !";
                    endGamePanel.SetActive(true);
                    Time.timeScale = 0;
                }
            }

            if (PlayerPrefs.GetInt("online") == 0) // Si le jeu se déroule sur un même ordinateur il y a un changement de l'index du joueur
            {
                indexPlayer = indexPlayer == 0 ? 1 : 0;
                mouse.player = Players[indexPlayer];
            }

            // Changement de l'affichage après changement du joueur
            skipTurn = false;
            ChangeSpriteButton();
            if(!endGamePanel.activeSelf)
                NewTurn();
        }
    }

    void NextBoard()
    {
        Dictionary<Vector2, List<Unit>> moves = new Dictionary<Vector2, List<Unit>>();
        Dictionary<Vector2, List<Unit>> attacks = new Dictionary<Vector2, List<Unit>>();

        // Répertorisation des mouvements et attaques des monstres des 2 joueurs
        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                int power = 0;
                UsePowerSpecial(monster, Mythologie.Mytho.Egyptienne, ref power);
                Vector2 movement = monster._position + monster._movement;

                // Monstres qui vont bouger ainsi que leur attaque si il y en a
                if (moves.ContainsKey(movement))
                {
                    moves[movement].Add(monster);
                    foreach (var m in moves[movement])
                        m._attack = Vector2.zero;
                }
                else moves.Add(movement, new List<Unit>() {monster});
            }
        }

        foreach (var player in Players) // Répertorie la position des différentes attaques
        {
            foreach (var monster in player._monsters)
            {
                int power = 0;
                UsePowerSpecial(monster, Mythologie.Mytho.Japonaise, ref power);

                if (monster._attack != Vector2.zero)
                {
                    Vector2 attack = monster._position + monster._movement + monster._attack;
                    for (int i = 0; i < 3; i++) // Un monstre peut attaquer jusqu'à 3 cases de distance
                    {
                        bool isAttackable = false;
                        if (moves.ContainsKey(attack)) // Teste si un monstre adverse est présent sur la case
                        {
                            foreach (var m in moves[attack])
                                isAttackable = isAttackable || m.Player != monster.Player;
                        }

                        if (isAttackable) // S'il y a un monstre qui peut être attaqué, on l'ajoute au dictionnaire
                        {
                            if (attacks.ContainsKey(attack)) attacks[attack].Add(monster);
                            else attacks.Add(attack, new List<Unit>() {monster});
                            i = 3;
                        }
                        else // Sinon on regarde sur la case suivante
                            attack += monster._attack;
                    }
                }
            }
        }
        

        MoveMonsters(moves, attacks);
    }

    // Gestion des mouvements et attaques si 2 monstres se retrouvent sur la même case
    private void MoveMonsters(Dictionary<Vector2, List<Unit>> moves, Dictionary<Vector2, List<Unit>> attacks)
    {
        Dictionary<Vector2, List<Unit>> repelledMonster = new Dictionary<Vector2, List<Unit>>();
        foreach (var monsters in moves)
        {
            if (monsters.Value.Count == 2) // S'il y a deux monstres qui veulent aller sur cette case
            {
                int power = 0;
                var m = monsters.Value;
                int attack = m[0].Power - m[1].Power;

                UsePowerSpecial(m[0], Mythologie.Mytho.Nordique, ref attack);
                UsePowerSpecial(m[1], Mythologie.Mytho.Nordique, ref power);
                attack -= power;

                if (attacks.ContainsKey(monsters.Key)) // S'il y a des monstres qui attaquent cette case
                {
                    foreach (var monster in attacks[monsters.Key])
                        attack += (monster.Player == m[0].Player ? monster.Power : -monster.Power);
                }

                var movement = m[1]._movement;
                if (attack >= 0) // Si le deuxieme monstre perd
                {
                    m[1]._movement = (m[1]._movement == Vector2.zero ? m[0]._movement : Vector2.zero);
                    if (!m[1].wounded)
                    {
                        var v = m[1]._position + m[1]._movement;
                        if (repelledMonster.ContainsKey(v)) repelledMonster[v].Add(m[1]);
                        else repelledMonster.Add(v, new List<Unit>() {m[1]});
                    }

                    int win = 0;
                    UsePowerSpecial(m[1], Mythologie.Mytho.Grecque, ref win);
                    
                    if (win == 1) Players[m[1].Player].Delete(m[1]);
                    else State(m[1]);
                    m.RemoveAt(1);
                }

                if (attack <= 0) // Si le premier monstre perd
                {
                    m[0]._movement = (m[0]._movement == Vector2.zero ? movement : Vector2.zero);
                    if (!m[0].wounded)
                    {
                        var v = m[0]._position + m[0]._movement;
                        if (repelledMonster.ContainsKey(v)) repelledMonster[v].Add(m[0]);
                        else repelledMonster.Add(v, new List<Unit>() {m[0]});
                    }

                    int win = 0;
                    UsePowerSpecial(m[0], Mythologie.Mytho.Grecque, ref win);
                    
                    if (win == 1) Players[m[0].Player].Delete(m[0]);
                    else State(m[0]);
                    m.RemoveAt(0);
                }

                if (m.Count == 1) // Si un monstre se déplace sur la case ciblée
                {
                    m[0]._position = monsters.Key;
                    Move(m[0]);
                }
            }
            else // Sinon, il n'y a qu'un monstre qui veut atteindre cette case
            {
                var m = monsters.Value[0];
                m._position = monsters.Key;
                Move(m);

                if (attacks.ContainsKey(monsters.Key)) // Si la case est attaquée
                {
                    int attack = m.Power;
                    UsePowerSpecial(m, Mythologie.Mytho.Nordique, ref attack);

                    foreach (var monster in attacks[monsters.Key])
                        attack += (monster.Player == m.Player ? monster.Power : -monster.Power);

                    if (attack <= 0) // Si le monstre perd
                        State(m);
                }
            }
        }

        foreach (var kvp in repelledMonster) // Pour tout les monstres qui ont été repoussés
        {
            if ((moves.ContainsKey(kvp.Key) && moves[kvp.Key].Count > 0)
                || kvp.Value.Count > 1
                || kvp.Key.x < 0 || kvp.Key.x > 10 || kvp.Key.y < 0 || kvp.Key.y > 10) // Les cas ou le monstres meurt
            {
                foreach (var monster in kvp.Value)
                {
                    State(monster);
                }
            }
            else // Les cas ou le monstre est juste repoussé
            {
                var m = kvp.Value[0];
                m._position = kvp.Key;
                Move(m);
            }
        }
    }

    // Déplace la prefab du monstre en fonction de sa position
    private void Move(Unit monster)
    {
        if (monster._movement != Vector2.zero) Debug.Log(monster.Name + " c'est déplacé");
        
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
        
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        monster.MovePrefab(board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position);        
        monster._movement = Vector2.zero;
        monster._attack = Vector2.zero;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
    }

    // Fait perdre une vie au monstre
    private void State(Unit monster)
    {
        if (!monster.wounded) monster.wounded = true;
        else Players[monster.Player].Delete(monster);
    }

    // Utilise le pouvoir de la mythologie de chaque joueurs si elle est activée
    private void UsePowerSpecial(Unit monster, Mythologie.Mytho mythologie, ref int power)
    {
        if (Players[0].Mythologie.activated && Players[0].Mythologie.Name == mythologie) Players[0].Mythologie.PowerSpecial(monster, ref power);
        if (Players[1].Mythologie.activated && Players[1].Mythologie.Name == mythologie) Players[1].Mythologie.PowerSpecial(monster, ref power);
    }

    // Met en place les images des monstres ainsi que les noms qui leur sont liés sur les boutons d'ajout des monstres
    public void ChangeSpriteButton()
    {
        for(int i = 0; i < Buttons.Count; i ++)
        {
            Buttons[i].GetComponent<UnityEngine.UI.Image>().sprite = Players[indexPlayer].Mythologie
                .Monsters[i * 2 % Players[indexPlayer].Mythologie.Monsters.Count].GetComponent<SpriteRenderer>().sprite;
            Buttons[i].GetComponent<NomPerso>().ChangePlayer(indexPlayer, (int) Players[indexPlayer].Mythologie.Name);
        }

        ButtonMythology.GetComponent<UnityEngine.UI.Image>().sprite =
            prefabMythology[(int) Players[indexPlayer].Mythologie.Name].GetComponent<SpriteRenderer>().sprite;
    }

    // Passe directement au joueur suivant
    public void skipTurnFunc()
    {
        skipTurn = true;
    }

    // Met en pause le jeu et affiche un menu
    public void Pause()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
        BMenu.SetActive(false);
        BRun.SetActive(true);
        B2.text = ">";
        mouse.onMenu = true;       
    }

    // Reprend le jeu
    public void Resume()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false); 
        BMenu.SetActive(true);
        BRun.SetActive(false);
        B2.text = "||";
        mouse.onMenu = newTurnPanel.activeSelf;       
    }

    // Indique quel joueur va jouer
    public void NewTurn()
    {
        mouse.onMenu = true;
        Time.timeScale = 0;
        newTurnText.text = "C'est à " + Players[indexPlayer].Name + " de jouer";
        newTurnPanel.SetActive(true);
    }
    
    // Indique que l'on commence un nouveau tour
    public void StartNewTurn()
    {
        mouse.onMenu = pauseMenu.activeSelf;
        Time.timeScale = 1f; 
        newTurnPanel.SetActive(false); 
    }

    // Quitte la partie en cours et retourne au menu principale
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Mytho-Lobby", LoadSceneMode.Single);
    }

    // Quitte l'application
    public void Quit()
    {
        Application.Quit();
    }
}
