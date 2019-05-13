using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Board boardParent;
    public Vector2 coordinate;
    public bool isEmpty;
    
    // Start is called before the first frame update
    public void SetUp(int x, int y)
    {
        coordinate = new Vector2(x, y);
        isEmpty = true;
    }
}
