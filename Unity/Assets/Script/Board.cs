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

    Vector2 positionSpawn1 = new Vector2(0.75f, -2f);
    Vector2 positionSpawn2 = new Vector2(12.06f, 1.5f);

    // Start is called before the first frame update
    public void Setup()
    {
        //Apparition du Plateau
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

        //Apparition du Spawn 1
        GameObject Spawn1 = new GameObject("Spawn1");
        Spawn1.transform.parent = transform;
        for (int spawnY = 0; spawnY < 3; spawnY++)
        {
            for (int spawnX = 0; spawnX < 3; spawnX++)
            {
                if (!(spawnX == 0 && spawnY == 0) && !(spawnX == 2 && spawnY == 2))
                {
                    float xPos = spawnX * xOffset + xOffset * spawnY;
                    float yPos = spawnY * yOffset - yOffset * spawnX;
                    GameObject hexSpawn = Instantiate(hexPrefab, new Vector2(xPos,yPos), Quaternion.identity, Spawn1.transform) as GameObject;
                    hexSpawn.name = "Hex_x" + spawnX + "_y" + spawnY;
                }
            }
        }
        Spawn1.transform.position = positionSpawn1;

        //Apparition du Spawn 2
        GameObject Spawn2 = new GameObject("Spawn2");
        Spawn2.transform.parent = transform;
        for (int spawnY = 0; spawnY < 3; spawnY++)
        {
            for (int spawnX = 0; spawnX < 3; spawnX++)
            {
                if (!(spawnX == 0 && spawnY == 0) && !(spawnX == 2 && spawnY == 2))
                {
                    float xPos = spawnX * xOffset + xOffset * spawnY;
                    float yPos = spawnY * yOffset - yOffset * spawnX;
                    GameObject hexSpawn = Instantiate(hexPrefab, new Vector2(xPos, yPos), Quaternion.identity, Spawn2.transform) as GameObject;
                    hexSpawn.name = "Hex_x" + spawnX + "_y" + spawnY;
                }
            }
        }
        Spawn2.transform.position = positionSpawn2;
    }
}
