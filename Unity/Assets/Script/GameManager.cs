using System;
using System.Collections;
using System.Collections.Generic;
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

    public List<GameObject> PrefabsMonsters;
    public List<Player> Players;
    
    public List<GameObject> Buttons;
        
    public GameObject prefabParticle;
    public GameObject pauseMenu;
    public GameObject newTurnPanel;
    public GameObject endGamePanel;

    public Text timeText;
    public Text newTurnText;
    public Text endGame;
    public bool skipTurn;

    public GameObject AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        //PrefabsMonsters[0].GetComponent<SpriteRenderer>().sprite;
        decompte = timer;
        indexPlayer = 0;
        board.Setup();
        mouse.goal = board.goal;
        mouse.onMenu = pauseMenu.activeSelf;

        // Entré des noms des joueurs à la place de Zeus et Poseidon"
        GameObject player = new GameObject("Player");
        GameObject player0 = new GameObject("Zeus");
        player0.transform.parent = player.transform;
        GameObject player1 = new GameObject("Poseidon");
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
                new Player("Zeus", "Spawn1", PrefabsMonsters.GetRange(8, 2), player0, prefabParticle, 2, 0),
                new Player("Poseidon", "Spawn2", PrefabsMonsters.GetRange(4, 2), player1, prefabParticle, 0, 1)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        Players[0].Add(0);
        Players[0].Add(0);
        Players[0].Add(0);
        Players[0].Add(0);
        Players[0].Add(0);
        Players[0].Add(0);
        Players[0].Add(0);
        
        
        Players[1].Add(0);
        Players[1].Add(0);
        Players[1].Add(0);
        Players[1].Add(0);
        Players[1].Add(0);
        Players[1].Add(0);
        Players[1].Add(0);
        
        changeSpriteButton();
        NewTurn();
        
        // selon la sélection de la mythologie dans l'interface on renvoie un int qui va être l'index * 6
        /*
        foreach (var p in Players)
        {
            foreach (var m in p._monsters)
            {
                //Debug.Log(m.name);
                Debug.Log(m._position.x + "; " + m._position.y);
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Détecter le bouton echap
        {
            if (pauseMenu.activeSelf)
                Resume();
            else
                Pause();
        }

        if (!pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if(newTurnPanel.activeSelf)
                StartNewTurn();
            else
                skipTurnFunc();
        }

        if((int)(decompte - Time.deltaTime) != (int)(decompte))
        {
            timeText.text = "Temps restant : " + (int)(decompte - Time.deltaTime);
        }
        decompte -= Time.deltaTime;

        if (decompte <= 0 || skipTurn) // Fin du timer
        {
            mouse.Clear();
            decompte = timer;
            if (indexPlayer == 1)
            {
                NextBoard();
                int w = 0;
                for(int i = 0; i < Players.Count; i++)
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

                if (Math.Abs(w) == 3)
                {
                    AudioManager.GetComponent<AudioManager>().Play("Fireworks");
                    endGame.text = (w > 0 ? Players[0].Name : Players[1].Name) + " WIN !";
                    endGamePanel.SetActive(true);
                    Time.timeScale = 0;
                }
            }
            indexPlayer = indexPlayer == 0 ? 1 : 0;
            mouse.player = Players[indexPlayer];
            skipTurn = false;
            changeSpriteButton();
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
                Debug.Log("Deux monstres se retrouvent sur la même case");

                int power = 0;
                var m = monsters.Value;
                int attack = m[0].Power - m[1].Power;

                UsePowerSpecial(m[0], Mythologie.Mytho.Nordique, ref attack);
                UsePowerSpecial(m[1], Mythologie.Mytho.Nordique, ref power);
                attack -= power;

                if (attacks.ContainsKey(monsters.Key)) // S'il y a des monstres qui attaquent cette case
                {
                    foreach (var monster in attacks[monsters.Key])
                    {
                        attack += (monster.Player == m[0].Player ? monster.Power : -monster.Power);
                    }

                    Debug.Log("Il y a des attaques extérieur");
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

                    State(m[1]);
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

                    State(m[0]);
                    m.RemoveAt(0);
                }

                if (m.Count == 1) // Si un monstre se déplace sur la case ciblée
                {
                    m[0]._position = monsters.Key;
                    Move(m[0]);
                    Debug.Log("Il ne reste plus que le monstre " + m[0].Name);
                }
                else Debug.Log("Il y a toujours deux monstres");
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
                    {
                        attack += (monster.Player == m.Player ? monster.Power : -monster.Power);
                    }

                    if (attack <= 0) // Si le monstre perd
                    {
                        if (m.wounded) Debug.Log(m.Name + " est mort d'attaque extérieur");
                        State(m);
                    }
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
        if (Players[0].Mythologie.Name == mythologie) Players[0].Mythologie.PowerSpecial(monster, ref power);
        if (Players[1].Mythologie.Name == mythologie) Players[1].Mythologie.PowerSpecial(monster, ref power);
    }

    public void changeSpriteButton()
    {
        Debug.Log(Buttons.Count);
        for(int i = 0; i < Buttons.Count; i ++)
        {
            Buttons[i].GetComponent<UnityEngine.UI.Image>().sprite = Players[indexPlayer].Mythologie
                .Monsters[i % Players[indexPlayer].Mythologie.Monsters.Count].GetComponent<SpriteRenderer>().sprite;
            Debug.Log(Buttons[i].name);
        }
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
        mouse.onMenu = true;       
    }

    // Reprend le jeu
    public void Resume()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false); 
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Mytho-Lobby", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
