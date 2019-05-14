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
        
        
        Players[0].Add("Meduse0");
        Players[0].Add("Meduse1");
        Players[0].Add("Meduse2");
        Players[0].Add("Meduse3");
        Players[0].Add("Meduse4");
        Players[0].Add("Meduse5");
        Players[0].Add("Meduse6");
        
        Players[1].Add("Nout");
        
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
            NextBoardDico();
            indexPlayer = indexPlayer == 0 ? 1 : 0;
            mouse.player = Players[indexPlayer];
        }
    }

    void NextBoardDico()
    {
        Dictionary<Vector2, List<Unit>> move = new Dictionary<Vector2, List<Unit>>();
        Dictionary<Vector2, List<Unit>> attack = new Dictionary<Vector2, List<Unit>>();

        foreach (var player in Players)
        {
            foreach (var monster in player._monsters)
            {
                if (!Equals(monster._movement, Vector2.negativeInfinity))
                {
                    if (move.ContainsKey(monster._movement)) move[monster._movement].Add(monster);
                    else move.Add(monster._movement, new List<Unit>() {monster});
                }
            }
        }

        foreach (var monsters in move)
        {
            if (monsters.Value.Count == 1)
            {
                // Gestion du mouvement lorsqu'il n'y a qu'un monstre sur la case d'arrivée
                board.hexGrid[(int) monsters.Value[0]._position.x, (int) monsters.Value[0]._position.y].GetComponent<Tile>().isEmpty = true;
                
                Unit monster = monsters.Value[0];
                monster._position = monster._movement;
                monster.PrefabMonster.transform.position = 
                    board.hexGrid[(int) (monster._position.x), (int) monster._position.y].transform.position;
                monster.PrefabMonster.transform.position = new Vector3(monster.PrefabMonster.transform.position.x, monster.PrefabMonster.transform.position.y, -1);
                
                monster._movement = Vector2.negativeInfinity;
                board.hexGrid[(int) (monster._position.x), (int) monster._position.y].GetComponent<Tile>().isEmpty = false;
                
                Debug.Log(monster.Name + " c'est déplacé");
                
                // Gestion de la mise en place de l'attaque après le déplacement du monstre
                if (!Equals(monster._attack, Vector2.negativeInfinity))
                {
                    
                }
            }
            else
            {
                
            }
        }
        
        if (move.Count == 0) Debug.Log("Il n'y a pas de déplacement à faire");
    }
}
