using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
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
        
        Players[1].Add("Nout0", 1, 3);
        Players[1].Add("Nout1", 1, 4);
        Players[1].Add("Nout2", 1, 2);
        Players[1].Add("Nout3", 1, 1);
        Players[1].Add("Nout4", 1, 3);
        Players[1].Add("Nout5", 1, 2);
        Players[1].Add("Nout6", 1, 1);
        
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
        Dictionary<Vector2, Unit> stays = new Dictionary<Vector2, Unit>();

        // Répertorisation des mouvements et attaques des monstres des 2 joueurs
        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                if (!Equals(monster._movement, Vector2.negativeInfinity))
                {
                    // Monstres qui vont bouger ainsi que leur attaque si il y a
                    if (moves.ContainsKey(monster._movement)) moves[monster._movement].Add(monster);
                    else moves.Add(monster._movement, new List<Unit>(){monster});
                    
                    if (!Equals(monster._attack, Vector2.negativeInfinity))
                    {
                        if (attacks.ContainsKey(monster._attack)) attacks[monster._attack].Add(monster);
                        else attacks.Add(monster._attack, new List<Unit>(){monster});
                    }
                }
                else if (Equals(monster._movement, Vector2.negativeInfinity))
                {
                    // Monstres qui ne vont pas bouger ainsi que leur attaque si il y a
                    stays[monster._movement] = monster;
                    
                    if (!Equals(monster._attack, Vector2.negativeInfinity))
                    {
                        if (attacks.ContainsKey(monster._attack)) attacks[monster._attack].Add(monster);
                        else attacks.Add(monster._attack, new List<Unit>(){monster});
                    }
                }
            }
        }

        // Gestion des mouvements et attaques si 2 monstres se retrouvent sur la même case
        foreach (var monsters in moves)
        {
            if (monsters.Value.Count == 1 && !stays.ContainsKey(monsters.Value[0]._position))
            {
                // Mouvement sur une case vide
                Unit monster = monsters.Value[0];
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
                Unit monsterMotionless = stays[monsters.Key]; 
                
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

    // Déplace le monstre donné en paramètre
    private void Move(Unit monster)
    {
        board.hexGrid[(int) monster._position.x, (int) monster._position.y].GetComponent<Tile>().isEmpty = true;
                
        // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
        monster._position = monster._movement;
        monster.MovePrefab(board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position);
                
        monster._movement = Vector2.negativeInfinity;
        board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
                
        Debug.Log(monster.Name + " c'est déplacé");
    }

    // Gestion des attaques sans déplacements des monstres
    private void Attack()
    {
        
    }

    // Vérification si il y a d'autres attaquants
    private void Assistance(Unit monster0, Unit monster1, Dictionary<Vector2, List<Unit>> attacks, out int power0, out int power1)
    {
        power0 = monster0.Power;
        power1 = monster1.Power;
        Vector2 position = monster0._movement;
        
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

    // Gestion de l'attaque de deux monstres qui se sont déplacés sur une même case
    private void AttackSame(Unit monster0, Unit monster1, Dictionary<Vector2, List<Unit>> attacks, Vector2 position)
    {
        // Suppression de leur attaque et prise en compte de l'aide d'autres monstres de chaque joueur
        monster0._attack = Vector2.negativeInfinity;
        monster1._attack = Vector2.negativeInfinity;
        int power0;
        int power1;
        
        Assistance(monster0, monster1, attacks, out power0, out power1);
        
        // Gestion des gagnant et mise à jour des états de chacun
        if (power0 == power1)
        {
            monster0._movement = Vector2.negativeInfinity;
            monster1._movement = Vector2.negativeInfinity;

            if (!monster0.state) monster0.state = true;
            else Players[monster0.Player].Delete(monster0);

            if (!monster1.state) monster1.state = true;
            else Players[monster1.Player].Delete(monster1);
            
            Debug.Log(monster0.Name + " et " + monster1.Name + " sont à égalités");
        }
        else if (power0 > power1)
        {
            Move(monster0);
            monster1._movement = Vector2.negativeInfinity;
            
            if (!monster1.state) monster1.state = true;
            else Players[monster1.Player].Delete(monster1);
            
            Debug.Log(monster0.Name + " gagne");
        }
        else
        {
            monster0._movement = Vector2.negativeInfinity;
            Move(monster1);
            
            if (!monster0.state) monster0.state = true;
            else Players[monster0.Player].Delete(monster0);
            
            Debug.Log(monster1.Name + " gagne");
        }
        
        monster0._attack = Vector2.negativeInfinity;
        monster1._attack = Vector2.negativeInfinity;
    }

    // Gestion de l'attaque d'un monstre qui se déplace sur une case déjà pleine
    private void AttackAlong(Unit monsterInMotion, Unit monsterMotionless, Dictionary<Vector2, List<Unit>> attacks, Vector2 position)
    {
        // Suppression de leur attaque et prise en compte de l'aide d'autres monstres pour chaque joueurs
        monsterInMotion._attack = Vector2.negativeInfinity;
        monsterMotionless._attack = Vector2.negativeInfinity;
        int powerInMotion;
        int powerMotionless;
        
        Assistance(monsterInMotion, monsterMotionless, attacks, out powerInMotion, out powerMotionless);
        
        // Gestion des gagnant et mise à jour des états de chacun
        if (powerInMotion >= powerMotionless)
        {
            // Calcul de la nouvelle position du monstre immobile
            Vector2 posAttacker = monsterInMotion._position;
            Vector2 posVictim = monsterMotionless._position;
            
            Debug.Log("posVictim = (" + posVictim.x + ", " + posVictim.y + ")");
            Debug.Log("posAttacker = (" + posAttacker.x + ", " + posAttacker.y + ")");
            Debug.Log("pos = (" + (2 * posVictim.x - posAttacker.x) + ", " + (2 * posVictim.y - posAttacker.y) + ")");
            
            if (Equals(posAttacker.x, posVictim.x)) monsterMotionless._movement = new Vector2(posVictim.x, 2 * posVictim.y - posAttacker.y);
            else if (Equals(posAttacker.y, posVictim.y)) monsterMotionless._movement = new Vector2(2 * posVictim.x - posAttacker.x, posVictim.y);
            else monsterMotionless._movement = new Vector2(2 * posVictim.x - posAttacker.x, 2 * posVictim.y - posAttacker.y);
            
            Move(monsterMotionless);

            // Changement d'état du monstre qui était immobile
            if (!monsterMotionless.state) monsterMotionless.state = true;
            else Players[monsterMotionless.Player].Delete(monsterMotionless);
            
            if (powerInMotion == powerMotionless)
            {
                // Changement du monstre qui voulait bouger
                if (!monsterInMotion.state) monsterInMotion.state = true;
                else Players[monsterInMotion.Player].Delete(monsterInMotion);

                monsterInMotion._movement = Vector2.negativeInfinity;
                
                Debug.Log(monsterInMotion.Name + " et " + monsterMotionless.Name + " sont à égalités");
            }
            else
            {
                Move(monsterInMotion);
            
                Debug.Log(monsterInMotion.Name + " gagne");
            }
        }
        else
        {
            monsterInMotion._movement = Vector2.negativeInfinity;
            Move(monsterMotionless);
            
            if (!monsterInMotion.state) monsterInMotion.state = true;
            else Players[monsterInMotion.Player].Delete(monsterInMotion);
            
            Debug.Log(monsterMotionless.Name + " gagne");
        }
        
        monsterInMotion._attack = Vector2.negativeInfinity;
        monsterMotionless._attack = Vector2.negativeInfinity;
    }
}
