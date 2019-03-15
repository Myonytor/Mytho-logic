using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Board : MonoBehaviour
{
    public GameObject hexPrefab;
    public GameObject[,] hexGrid;

    int width = 10;
    int height = 10;

    float xOffset = 0.8f;
    float yOffset = 0.24f;

    // Start is called before the first frame update
    public void Setup()
    {
        hexGrid = new GameObject[width, height];
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                float xPos = x * xOffset + xOffset * y;
                float yPos = y * yOffset - yOffset * x;
                GameObject hex = Instantiate(hexPrefab, new Vector2(xPos, yPos), Quaternion.identity) as GameObject;
                hex.transform.parent = transform;
                hex.name = "Hex_x" + x + "_y" + y;
                hexGrid[x, y] = hex;
            }
        }
    }
}
