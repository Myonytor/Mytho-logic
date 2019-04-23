using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Board boardParent;
    public Vector2 coordinates;
    
    public bool isEmpty;
    
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
