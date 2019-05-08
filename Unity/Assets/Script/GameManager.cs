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
        
        
        Players[0].Add("Meduse");
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
            NextBoard();
        }
    }

    void NextBoard()
    {
        HashSet<Vector2> movAlone = new HashSet<Vector2>(), movMult = new HashSet<Vector2>();

        foreach (var p in Players)
        {
            foreach (var m in p._monsters)
            {
                if (movAlone.Contains(m._mouvement))
                {
                    movAlone.Remove(m._mouvement);
                    movMult.Add(m._mouvement);
                }
                else
                {
                    movAlone.Add(m._mouvement);
                }
            }
        }

        foreach (var p in Players)
        {
            foreach (var m in p._monsters)
            {
                if (movAlone.Contains(m._mouvement))
                {
                    m._position = m._mouvement;
                    m.PrefabMonster.transform.position =
                        board.hexGrid[(int) (m._position.x), (int) m._position.y].transform.position;
                }
                else
                {
                    
                }
            }
        }
    }
}
