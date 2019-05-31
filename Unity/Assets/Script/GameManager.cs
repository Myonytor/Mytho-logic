﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int timer = 25;
    public Board board;
    public int indexPlayer;
    public MouseManager mouse;
    
    private float decompte;

    public List<GameObject> PrefabsMonsters;
    public List<Player> Players;

    public GameObject prefabParticle;

    public Text timeText;
    public bool skipTurn;
    
    // Start is called before the first frame update
    void Start()
    {
        decompte = timer;
        indexPlayer = 0;
        board.Setup();

        // Entré des noms des joueurs à la place de Zeus et Poseidon"
        GameObject player = new GameObject("Player");
        GameObject player0 = new GameObject("Zeus");
        player0.transform.parent = player.transform;
        GameObject player1 = new GameObject("Poseidon");
        player1.transform.parent = player.transform;
        
        Players = new List<Player>()
            {
                new Player("Zeus", "Spawn1", PrefabsMonsters.GetRange(0, 2), player0, prefabParticle, "Nordique", 0),
                new Player("Poseidon", "Spawn2", PrefabsMonsters.GetRange(2, 2), player1, prefabParticle, "Egyptienne", 1)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        
        Players[0].Add("Meduse0", 2);
        Players[0].Add("Meduse1", 1);
        Players[0].Add("Meduse2", 3);
        Players[0].Add("Meduse3", 1);
        Players[0].Add("Meduse4", 2);
        Players[0].Add("Meduse5", 4);
        Players[0].Add("Meduse6", 2);
        Players[0].AddTest("MeduseTest4", 4, 1, 1);
        Players[0].AddTest("MeduseTest5", 4, 1, 2);
        Players[0].AddTest("MeduseTest6", 4, 2, 1);
        
        Players[1].Add("Nout0", 3);
        Players[1].Add("Nout1", 4);
        Players[1].Add("Nout2", 2);
        Players[1].Add("Nout3", 1);
        Players[1].Add("Nout4", 3);
        Players[1].Add("Nout5", 2);
        Players[1].Add("Nout6", 1);
        Players[1].AddTest("NoutTest2", 4, 0, 2);
        Players[1].AddTest("NoutTest3", 4, 0, 3);
        Players[1].AddTest("NoutTest4", 4, 2, 2);

        Players[0].Mythologie.activated = true;
        Players[1].Mythologie.activated = true;
        
        // selon la sélection de la mythologie dans l'interface on renvoie un int qui va être l'index * 6
        foreach (var p in Players)
        {
            foreach (var m in p._monsters)
            {
                Debug.Log(m);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if((int)(decompte - Time.deltaTime) != (int)(decompte))
        {
            Debug.Log((int)(decompte - Time.deltaTime));
            timeText.text = "Temps restant : " + (int)(decompte - Time.deltaTime);
        }
        decompte -= Time.deltaTime;

        if (decompte <= 0 || skipTurn)// Fin du timer
        {
            mouse.Clear();
            decompte = timer;
            if (indexPlayer == 1) NextBoard();
            indexPlayer = indexPlayer == 0 ? 1 : 0;
            mouse.player = Players[indexPlayer];
            skipTurn = false;
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
                UsePowerSpecial(monster, "Egyptienne", ref power);
                Vector2 movement = monster._position + monster._movement;
                
                // Monstres qui vont bouger ainsi que leur attaque si il y en a
                if (moves.ContainsKey(movement))
                {
                    moves[movement].Add(monster);
                    foreach (var m in moves[movement])
                        m._attack = Vector2.zero;
                }
                else moves.Add(movement, new List<Unit>(){monster});
            }
        }
        
        foreach (var player in Players)// Répertorie la position des différentes attaques
        {
            foreach (var monster in player._monsters)
            {
                int power = 0;
                UsePowerSpecial(monster, "Japonaise", ref power);
                
                if (monster._attack != Vector2.zero)
                {
                    Vector2 attack = monster._position + monster._movement + monster._attack;
                    for (int i = 0; i < 3; i++)// Un monstre peut attaquer jusqu'à 3 cases de distance
                    {
                        bool isAttackable = false;
                        if (moves.ContainsKey(attack))// Teste si un monstre adverse est présent sur la case
                        {
                            foreach (var m in moves[attack])
                                isAttackable = isAttackable || m.Player != monster.Player;
                        }

                        if(isAttackable)// S'il y a un monstre qui peut être attaqué, on l'ajoute au dictionnaire
                        {
                            if (attacks.ContainsKey(attack)) attacks[attack].Add(monster);
                            else attacks.Add(attack, new List<Unit>() {monster});
                            i = 3;
                        }
                        else// Sinon on regarde sur la case suivante
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
            if (monsters.Value.Count == 2)// S'il y a deux monstres qui veulent aller sur cette case
            {
                Debug.Log("Deux monstres se retrouvent sur la même case");

                int power = 0;
                var m = monsters.Value;
                int attack = m[0].Power - m[1].Power;
                
                UsePowerSpecial(m[0], "Nordique", ref attack);
                UsePowerSpecial(m[1], "Nordique", ref power);
                attack -= power;

                if (attacks.ContainsKey(monsters.Key))// S'il y a des monstres qui attaquent cette case
                {
                    foreach (var monster in attacks[monsters.Key])
                    {
                        attack += (monster.Player == m[0].Player ? monster.Power : -monster.Power);
                    }
                    
                    Debug.Log("Il y a des attaques extérieur");
                }

                var movement = m[1]._movement;
                if (attack >= 0)// Si le premier monstre gagne
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
                if (attack <= 0)// Si le deuxième monstre gagne
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

                if (m.Count == 1)// Si un monstre se déplace sur la case ciblée
                {
                    m[0]._position = monsters.Key;
                    Move(m[0]);
                    Debug.Log("Il ne reste plus que le monstre " + m[0].Name);
                }
                else Debug.Log("Il y a toujours deux monstres");
            }
            else// Sinon, il n'y a qu'un monstre qui veut atteindre cette case
            {
                var m = monsters.Value[0];
                m._position = monsters.Key;
                Move(m);
                
                if (attacks.ContainsKey(monsters.Key))// Si la case est attaquée
                {
                    int attack = m.Power;
                    UsePowerSpecial(m, "Nordique", ref attack);
                    
                    foreach (var monster in attacks[monsters.Key])
                    {
                        attack += (monster.Player == m.Player ? monster.Power : -monster.Power);
                    }

                    if (attack <= 0)// Si le monstre perd
                    {
                        if (m.wounded) Debug.Log(m.Name + " est mort d'attaque extérieur");
                        State(m);
                    }
                }
            }
        }

        foreach (var kvp in repelledMonster)// Pour tout les monstres qui ont été repoussés
        {
            if ((moves.ContainsKey(kvp.Key) && moves[kvp.Key].Count > 0)
                || kvp.Value.Count > 1
                || kvp.Key.x < 0 || kvp.Key.x > 9 || kvp.Key.y < 0 || kvp.Key.y > 12)// Les cas ou le monstres meurt
            {
                foreach (var monster in kvp.Value)
                {
                    State(monster);
                }
            }
            else// Les cas ou le monstre est juste repoussé
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
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
        
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        monster.MovePrefab(board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position);        
        monster._movement = Vector2.zero;
        monster._attack = Vector2.zero;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
                
        Debug.Log(monster.Name + " c'est déplacé");
    }

    // Fait perdre une vie au monstre
    private void State(Unit monster)
    {
        if (!monster.wounded) monster.wounded = true;
        else Players[monster.Player].Delete(monster);
    }

    // Utilise le pouvoir de la mythologie de chaque joueurs si elle est activée
    private void UsePowerSpecial(Unit monster, string mythologie, ref int power)
    {
        if (Players[0].Mythologie.Name == mythologie) Players[0].Mythologie.PowerSpecial(monster, ref power);
        if (Players[0].Mythologie.Name == mythologie) Players[1].Mythologie.PowerSpecial(monster, ref power);
    }

    public void skipTurnFunc()
    {
        skipTurn = true;
    }
}
