using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        board.Setup();
        unit.SetUp();
        unit.Add("Sullivan", "Spawn1");
        unit.Add("Bob", "Spawn2");
        //unit.Add("Boggs", "Spawn2");
        //board.hexGrid[1, 2].GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
