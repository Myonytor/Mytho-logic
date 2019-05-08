using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
    public List<GameObject> Prefabs = new List<GameObject>();
    private List<GameObject> monsters = new List<GameObject>();

    public GameObject ParentPanel;
    */
    
    public Board board;
    public int indexPlayer;
    public MouseManager mouse;
    
    private float decompte;

    public List<GameObject> PrefabsMonsters;
    public List<Player> Players;
    
    // Start is called before the first frame update
    void Start()
    {
        decompte = 90;
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
                new Player("Poseidon", "Spawn2", PrefabsMonsters.GetRange(1, 2), player1)
            };
        mouse.ChangePlayer(Players[indexPlayer]);
        
        
        Players[0].Add("Meduse");
        Players[1].Add("Nout");
        // selon la sélection de la mythologie dans l'interface on renvoie un int qui va être l'index * 6

        //board.hexGrid[1, 2].GetComponentInChildren<SpriteRenderer>().color = Color.red;

        /*
        int i = 0;
        foreach (var currentPrefab in Prefabs)
        {
            GameObject newGameObject = Instantiate(currentPrefab, new Vector3(board.hexGrid[i, 0].transform.position.x, board.hexGrid[i, 0].transform.position.y + 1f, -1), Quaternion.identity) as GameObject;
            newGameObject.GetComponent<Transform>().parent = ParentPanel.transform;
            newGameObject.transform.localScale = new Vector3( 0.05f, 0.05f, 1f);
            monsters.Add(newGameObject);
            i += 1;
        }
        */
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
            decompte = 90;
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
                    m.position = m._mouvement;
                }
                else
                {
                    
                }
            }
        }
    }
}
