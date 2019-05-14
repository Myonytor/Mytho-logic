﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int timer = 10;
    public Board board;
    public int indexPlayer;
    public MouseManager mouse;
    
    private float decompte;

    public List<GameObject> PrefabsMonsters;
    public List<Player> Players;
    
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
                new Player("Zeus", "Spawn1", PrefabsMonsters.GetRange(0, 2), player0),
                new Player("Poseidon", "Spawn2", PrefabsMonsters.GetRange(2, 2), player1)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        
        Players[0].Add("Meduse0", 0);
        Players[0].Add("Meduse1", 0);
        Players[0].Add("Meduse2", 0);
        Players[0].Add("Meduse3", 0);
        Players[0].Add("Meduse4", 0);
        Players[0].Add("Meduse5", 0);
        Players[0].Add("Meduse6", 0);
        
        Players[1].Add("Nout0", 1);
        Players[1].Add("Nout1", 1);
        Players[1].Add("Nout2", 1);
        Players[1].Add("Nout3", 1);
        Players[1].Add("Nout4", 1);
        Players[1].Add("Nout5", 1);
        Players[1].Add("Nout6", 1);
        
        // selon la sélection de la mythologie dans l'interface on renvoie un int qui va être l'index * 6
    }

    // Update is called once per frame
    void Update()
    {        
        if((int)(decompte - Time.deltaTime) != (int)(decompte))
            Debug.Log((int)(decompte - Time.deltaTime));
        decompte -= Time.deltaTime;
        
        if (decompte <= 0)
        {
            mouse.Clear();
            decompte = timer;
            if (indexPlayer == 1) NextBoardDico();
            indexPlayer = indexPlayer == 0 ? 1 : 0;
            mouse.player = Players[indexPlayer];
        }
    }

    void NextBoardDico()
    {
        Dictionary<Vector2, List<Unit>> moves = new Dictionary<Vector2, List<Unit>>();
        Dictionary<Vector2, List<Unit>> attacks = new Dictionary<Vector2, List<Unit>>();

        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                if (!Equals(monster._movement, Vector2.negativeInfinity))
                {
                    if (moves.ContainsKey(monster._movement)) moves[monster._movement].Add(monster);
                    else moves.Add(monster._movement, new List<Unit>(){monster});
                }
            }
        }

        foreach (var monsters in moves)
        {
            if (monsters.Value.Count == 1)
            {
                Unit monster = monsters.Value[0];
                Move(monster);
                
                // Gestion de la mise en place de l'attaque après le déplacement du monstre
                if (!Equals(monster._attack, Vector2.negativeInfinity))
                {
                    if (attacks.ContainsKey(monster._attack)) attacks[monster._attack].Add(monster);
                    else attacks.Add(monster._attack, new List<Unit>(){monster});
                }
            }
            else
            {
                // Suppression de leur attaque
                Unit monster0 = monsters.Value[0].Player == 0 ? monsters.Value[0]: monsters.Value[1];
                Unit monster1 = monsters.Value[0].Player == 1 ? monsters.Value[0]: monsters.Value[1];
                monster0._attack = Vector2.negativeInfinity;
                monster1._attack = Vector2.negativeInfinity;

                // Vérification si il y a d'autres attaquants
                int power0 = monster0.Power;
                int power1 = monster1.Power;
                if (attacks.ContainsKey(monsters.Key))
                {
                    foreach (var attack in attacks[monsters.Key])
                    {
                        if (attack.Player == 0) power0 += attack.Power;
                        else power1 += attack.Power;
                    }                    
                }
                
                // Gestion des gagnant et mise à jour des états de chacun
                if (power0 == power1)
                {
                    monster0._movement = Vector2.negativeInfinity;
                    monster1._movement = Vector2.negativeInfinity;

                    if (!monster0.state) monster0.state = true;
                    else ; // Suppression du monstre

                    if (!monster1.state) monster1.state = true;
                    else ; // Suppression du monstre
                    
                    Debug.Log(monster0.Name + " et " + monster1.Name + " sont à égalités");
                }
                else if (power0 > power1)
                {
                    Move(monster0);
                    monster1._movement = Vector2.negativeInfinity;
                    
                    if (!monster1.state) monster1.state = true;
                    else ; // Suppression du monstre
                    
                    Debug.Log(monster0.Name + " gagne");
                }
                else
                {
                    monster0._movement = Vector2.negativeInfinity;
                    Move(monster1);
                    
                    if (!monster0.state) monster0.state = true;
                    else ; // Suppression du monstre
                    
                    Debug.Log(monster1.Name + " gagne");
                }
                
                monster0._attack = Vector2.negativeInfinity;
                monster1._attack = Vector2.negativeInfinity;
            }
        }
        
        if (moves.Count == 0) Debug.Log("Il n'y a pas de déplacement à faire");
    }

    private void Move(Unit monster)
    {
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
                
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        monster._position = monster._movement;
        monster.PrefabMonster.transform.position = 
            board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position;
        monster.PrefabMonster.transform.position = new Vector3(monster.PrefabMonster.transform.position.x, monster.PrefabMonster.transform.position.y, -1);
                
        monster._movement = Vector2.negativeInfinity;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
                
        Debug.Log(monster.Name + " c'est déplacé");
    }
}
