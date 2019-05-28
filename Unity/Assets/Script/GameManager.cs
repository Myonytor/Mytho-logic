﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject prefabParticle;
    
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
                new Player("Zeus", "Spawn1", PrefabsMonsters.GetRange(0, 2), player0, prefabParticle),
                new Player("Poseidon", "Spawn2", PrefabsMonsters.GetRange(2, 2), player1, prefabParticle)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        
        Players[0].Add("Meduse0", 0, 2);
        Players[0].Add("Meduse1", 0, 1);
        Players[0].Add("Meduse2", 0, 3);
        Players[0].Add("Meduse3", 0, 1);
        Players[0].Add("Meduse4", 0, 2);
        Players[0].Add("Meduse5", 0, 4);
        Players[0].Add("Meduse6", 0, 2);
        Players[0].AddTest("MeduseTest4", 0, 4, 1, 1);
        
        Players[1].Add("Nout0", 1, 3);
        Players[1].Add("Nout1", 1, 4);
        Players[1].Add("Nout2", 1, 2);
        Players[1].Add("Nout3", 1, 1);
        Players[1].Add("Nout4", 1, 3);
        Players[1].Add("Nout5", 1, 2);
        Players[1].Add("Nout6", 1, 1);
        Players[1].AddTest("NoutTest2", 1, 2, 0, 2);
        
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
            Debug.Log((int)(decompte - Time.deltaTime));
        decompte -= Time.deltaTime;
        
        if (decompte <= 0)
        {
            mouse.Clear();
            decompte = timer;
            if (indexPlayer == 1) NextBoard();
            indexPlayer = indexPlayer == 0 ? 1 : 0;
            mouse.player = Players[indexPlayer];
        }
    }

    void NextBoard()
    {
        Dictionary<Vector2, List<Unit>> moves = new Dictionary<Vector2, List<Unit>>();
        Dictionary<Vector2, List<Unit>> attacks = new Dictionary<Vector2, List<Unit>>();
        List<Unit> stays = new List<Unit>();

        // Répertorisation des mouvements et attaques des monstres des 2 joueurs
        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                if (!Equals(monster._movement, Vector2.negativeInfinity))
                {
                    Vector2 movement = new Vector2(monster._position.x + monster._movement.x, monster._position.y + monster._movement.y);
                    
                    // Monstres qui vont bouger ainsi que leur attaque si il y a
                    if (moves.ContainsKey(movement)) moves[movement].Add(monster);
                    else moves.Add(movement, new List<Unit>(){monster});
                    
                    if (!Equals(monster._attack, Vector2.negativeInfinity))
                    {
                        Vector2 attack = new Vector2(movement.x + monster._attack.x, movement.y + monster._attack.y);
                        
                        if (attacks.ContainsKey(attack)) attacks[attack].Add(monster);
                        else attacks.Add(attack, new List<Unit>(){monster});
                    }
                }
                else
                {
                    // Monstres qui ne vont pas bouger ainsi que leur attaque si il y a
                    stays.Add(monster);
                    
                    if (!Equals(monster._attack, Vector2.negativeInfinity))
                    {
                        Vector2 attack = new Vector2(monster._position.x + monster._attack.x, monster._position.y + monster._attack.y);
                        
                        if (attacks.ContainsKey(attack)) attacks[attack].Add(monster);
                        else attacks.Add(attack, new List<Unit>(){monster});
                    }
                }
            }
        }

        // Gestion des mouvements et attaques si 2 monstres se retrouvent sur la même case
        foreach (var monsters in moves)
        {
            Unit monster = monsters.Value[0];
            Vector2 movement = new Vector2(monster._position.x + monster._movement.x, monster._position.y + monster._movement.y);

            if (monsters.Value.Count == 1 && !stays.Exists(m => Equals(m._position, movement)))
            {
                Debug.Log(monsters.Value[0].Name + " bouge sur une case vide");
                
                // Mouvement sur une case vide
                Move(monster);
                
                // Gestion de la mise en place de l'attaque après le déplacement du monstre
                if (!Equals(monster._attack, Vector2.negativeInfinity))
                {
                    if (attacks.ContainsKey(monster._attack)) attacks[monster._attack].Add(monster);
                    else attacks.Add(monster._attack, new List<Unit>(){monster});
                }
            } 
            else if (monsters.Value.Count == 1)
            {
                // Mouvement sur une case pleine qui avait déjà un monstre
                Unit monsterInMotion = monsters.Value[0];
                Unit monsterMotionless = stays.Find(m => Equals(m._position, monsters.Key)); 
                
                AttackAlong(monsterInMotion, monsterMotionless, attacks, monsters.Key);
            }
            else
            {
                // Mouvement sur une case pleine en même temps qu'un autre monstre
                Unit monster0 = monsters.Value[0].Player == 0 ? monsters.Value[0] : monsters.Value[1];
                Unit monster1 = monsters.Value[0].Player == 1 ? monsters.Value[0] : monsters.Value[1];
                
                AttackSame(monster0, monster1, attacks, monsters.Key);
            }
        }
        
        if (moves.Count == 0) Debug.Log("Il n'y a pas de déplacement à faire");

        // Gestion des attaques seuls
        foreach (var attack in attacks)
        {
            
        }
    }

    private void Move(Unit monster)
    {
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
        
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        monster._position = new Vector2(monster._position.x + monster._movement.x, monster._position.y + monster._movement.y);
        monster.MovePrefab(board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position);
                
        monster._movement = Vector2.negativeInfinity;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
                
        Debug.Log(monster.Name + " c'est déplacé");
    }

    private void Attack()
    {
        
    }

    private void Assistance(Vector2 position, int player, ref int power0, ref int power1, Dictionary<Vector2, List<Unit>> attacks)
    {
        if (attacks.ContainsKey(position))
        {
            foreach (var attack in attacks[position])
            {
                if (attack.Player == 0) power0 += attack.Power;
                else power1 += attack.Power;
            }

            attacks.Remove(position);
        }
    }

    private void State(Unit monster)
    {
        if (!monster.state) monster.state = true;
        else Players[monster.Player].Delete(monster);
    }

    private void AttackSame(Unit monster0, Unit monster1, Dictionary<Vector2, List<Unit>> attacks, Vector2 position)
    {
        Debug.Log(monster0.Name + " fight with " + monster1.Name);
        
        // Suppression de leur attaque
        monster0._attack = Vector2.negativeInfinity;
        monster1._attack = Vector2.negativeInfinity;
        
        // Vérification si il y a d'autres attaquants
        int power0 = monster0.Power;
        int power1 = monster1.Power;
        
        Assistance(monster0._movement, 0, ref power0, ref power1, attacks);
        
        // Gestion des gagnant et mise à jour des états de chacun
        if (power0 == power1)
        {
            monster0._movement = Vector2.negativeInfinity;
            monster1._movement = Vector2.negativeInfinity;
            
            State(monster0);
            State(monster1);
            
            Debug.Log(monster0.Name + " et " + monster1.Name + " sont à égalités lorsqu'ils se déplacent sur la même case");
        }
        else if (power0 > power1)
        {
            monster1._movement = Vector2.negativeInfinity;
            
            Move(monster0);
            State(monster1);
            
            Debug.Log(monster0.Name + " gagne lorsqu'ils se déplacent sur la même case");
        }
        else
        {
            monster0._movement = Vector2.negativeInfinity;
            
            Move(monster1);
            State(monster0);
            
            Debug.Log(monster1.Name + " gagne lorsqu'ils se déplacent sur la même case");
        }
        
        monster0._attack = Vector2.negativeInfinity;
        monster1._attack = Vector2.negativeInfinity;
    }

    private void AttackAlong(Unit monsterInMotion, Unit monsterMotionless, Dictionary<Vector2, List<Unit>> attacks, Vector2 position)
    {
        Debug.Log(monsterInMotion.Name + "attack along " + monsterMotionless.Name);
        
        // Suppression de leur attaque
        monsterInMotion._attack = Vector2.negativeInfinity;
        monsterMotionless._attack = Vector2.negativeInfinity;
        
        // Vérification si il y a d'autres attaquants
        int powerInMotion = monsterInMotion.Power;
        int powerMotionless = monsterMotionless.Power;
        
        Assistance(monsterMotionless._position, monsterMotionless.Player, ref powerMotionless, ref powerInMotion, attacks);
        
        // Gestion des gagnant et mise à jour des états de chacun
        if (powerInMotion == powerMotionless)
        {
            monsterMotionless._movement = monsterInMotion._movement;
            monsterInMotion._movement = Vector2.negativeInfinity;
            
            Move(monsterMotionless);
            State(monsterMotionless);
            State(monsterInMotion);
            
            Debug.Log(monsterInMotion.Name + " et " + monsterMotionless.Name + " sont à égalités");
        }
        else if (powerInMotion > powerMotionless)
        {
            monsterMotionless._movement = monsterInMotion._movement;
            
            Move(monsterInMotion);
            Move(monsterMotionless);
            State(monsterMotionless);
            
            Debug.Log(monsterInMotion.Name + " gagne");
        }
        else
        {
            monsterInMotion._movement = Vector2.negativeInfinity;
            
            Move(monsterMotionless);
            State(monsterInMotion);
            
            Debug.Log(monsterMotionless.Name + " gagne");
        }
        
        monsterInMotion._attack = Vector2.negativeInfinity;
        monsterMotionless._attack = Vector2.negativeInfinity;
    }
}
