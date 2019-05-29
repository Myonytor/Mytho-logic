using System;
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

        // Répertorisation des mouvements et attaques des monstres des 2 joueurs
        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                Vector2 movement = monster._position + monster._movement;
                
                // Monstres qui vont bouger ainsi que leur attaque si il y a
                if (moves.ContainsKey(movement))
                {
                    moves[movement].Add(monster);
                    foreach (var m in moves[movement])
                        m._attack = Vector2.zero;
                }
                else moves.Add(movement, new List<Unit>(){monster});
            }
        }
        
        foreach (var player in Players)//Repertorie la position des differentes attaques
        {
            foreach (var monster in player._monsters)
            {
                if (monster._attack != Vector2.zero)
                {
                    Vector2 attack = monster._position + monster._movement + monster._attack;
                    for (int i = 0; i < 3; i++)//un monstre peut attaquer jusqu'a 3 cases de distance
                    {
                        bool isAttackable = false;
                        if (moves.ContainsKey(attack)) //teste si un monstre adverse est present sur la case
                        {
                            foreach (var m in moves[attack])
                                isAttackable = isAttackable || m.Player != monster.Player;
                        }

                        if(isAttackable)
                        {
                            if (attacks.ContainsKey(attack)) attacks[attack].Add(monster);
                            else attacks.Add(attack, new List<Unit>() {monster});
                            i = 3;
                        }
                        else
                            attack += monster._attack;
                    }
                }
            }
        }

        // Gestion des mouvements et attaques si 2 monstres se retrouvent sur la même case
        foreach (var monsters in moves)
        {
            if (monsters.Value.Count == 2)
            {
                var m = monsters.Value;
                int attack = m[0].Power - m[1].Power;

                if (attacks.ContainsKey(monsters.Key))//s'il y a des monstres qui attaquent cette case
                {
                    foreach (var monster in attacks[monsters.Key])
                    {
                        attack += (monster.Player == m[0].Player ? monster.Power : -monster.Power);
                    }
                }

                var movement = m[1]._movement;
                if (attack >= 0)
                {
                    m[1]._movement = (m[1]._movement == Vector2.zero ? -m[0]._movement : Vector2.zero);
                    State(m[1]);
                }
                if (attack <= 0)
                {
                    m[0]._movement = (m[0]._movement == Vector2.zero ? -movement : Vector2.zero);
                    State(m[0]);
                }
            }
            else
            {
                if (attacks.ContainsKey(monsters.Key))
                {
                    var m = monsters.Value[0];
                    int attack = m.Power;
                    
                    foreach (var monster in attacks[monsters.Key])
                    {
                        attack += (monster.Player == m.Player ? monster.Power : -monster.Power);
                    }

                    if (attack <= 0)
                    {
                        State(m);
                    }
                }
            }
        }

        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                if (monster._movement != Vector2.zero)
                {
                    monster._position += monster._movement;
                }
            }
        }
        
        foreach (var player in Players)
        {
            int i = 0;
            while (i < player._monsters.Count)
            {
                var monster = player._monsters[i];
                if (monster._movement != Vector2.zero && 
                   (monster._position.x < 0 || monster._position.y < 0 || 
                    monster._position.x > 9 || monster._position.y > 12))
                {
                    player.Delete(monster);
                }
                else
                    i++;
            }
        }
    }
    
    private void Move(Unit monster, Dictionary<Vector2, List<Unit>> attacks)
    {
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
        
        Debug.Log(board.hexGrid[(int) monster._position.x, (int) monster._position.y].tag);
        
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        if (!Equals(monster._movement, Vector2.zero)) monster._position += monster._movement;
        
        monster.MovePrefab(board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position);        
        monster._movement = Vector2.zero;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;

        if (attacks.ContainsKey(monster._position)) attacks.Remove(monster._position);
                
        Debug.Log(monster.Name + " c'est déplacé");
    }

    private void State(Unit monster)
    {
        if (!monster.wounded) monster.wounded = true;
        else Players[monster.Player].Delete(monster);
    }
}
