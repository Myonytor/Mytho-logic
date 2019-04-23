using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Board boardParent;
    public Vector2 coordinate;
    public GameObject monster;
    public bool isEmpty;
    
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        monster = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
