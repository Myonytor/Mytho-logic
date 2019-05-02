using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
    public List<GameObject> Prefabs = new List<GameObject>();
    private List<GameObject> monsters = new List<GameObject>();

    public GameObject ParentPanel;
    */
    
    public Board board;
    public Unit unit;
    public Player player;

    public List<Player> Players;
    // Start is called before the first frame update
    void Start()
    {
        board.Setup();
        player.Setup("Zeus", "Spawn1");
        player.Add("Sullivan");
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
    }
}
