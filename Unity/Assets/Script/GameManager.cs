using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    // Start is called before the first frame update
    void Start()
    {
        board.Setup();
        //board.hexGrid[1, 2].GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
