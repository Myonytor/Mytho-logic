using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Board : MonoBehaviour
{
    public GameObject hexPrefab;
    public GameObject[,] hexGrid;

    int width = 11;
    int height = 11;

    float xOffset = 0.8f;
    float yOffset = 0.24f;

    Vector2 positionSpawn1 = new Vector2(0.75f, -2f);
    Vector2 positionSpawn2 = new Vector2(10.5f, 2.5f);

    public Vector2[] goal= new Vector2[3];

    // Start is called before the first frame update
    public void Setup()
    {
        goal[0] = new Vector2((int)(width/2), (int)(height/2));
        System.Random rnd = new System.Random();
        int i, j;
        do
        {
            i = rnd.Next(width);
            j = rnd.Next(height);
        } while (i == (int)goal[0].x && j == (int)goal[0].y);
        goal[1] = new Vector2(i, j);
        goal[2] = 2 * goal[0] - goal[1];
        
        
        // Apparition du Plateau
        hexGrid = new GameObject[width, height + 3];
        GameObject Hex = new GameObject("Hex");
        Hex.transform.parent = transform;
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                float xPos = x * xOffset + xOffset * y;
                float yPos = y * yOffset - yOffset * x;
                GameObject hex = Instantiate(hexPrefab, new Vector2(xPos, yPos), Quaternion.identity, Hex.transform) as GameObject;
                hex.GetComponent<Tile>().SetUp(x, y);
                hex.name = "Hex_x" + x + "_y" + y;
                hex.tag = "Tile";
                hexGrid[x, y] = hex;
                for(int k = 0; k < 3; k ++)
                {
                    if ((int) goal[k].x == x && (int) goal[k].y == y)
                    {
                        k = 3;
                        hex.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0.66f, 0f, 0.56f);
                    }
                }
            }
        }
        Hex.transform.position = new Vector3(0, 0, 0);

        // Apparition du Spawn 1
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
                    hexSpawn.GetComponent<Tile>().SetUp(spawnX, spawnY + height);
                    hexSpawn.name = "Hex_x" + spawnX + "_y" + (spawnY + height);
                    hexSpawn.GetComponent<Tile>().SetUp(spawnX, spawnY + 11);
                    hexSpawn.name = "Hex_x" + spawnX + "_y" + (spawnY + 11);
                    hexSpawn.tag = "Spawn1";
                    hexGrid[spawnX, spawnY + height] = hexSpawn;
                }
            }
        }
        Spawn1.transform.position = positionSpawn1;

        // Apparition du Spawn 2
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
                    hexSpawn.GetComponent<Tile>().SetUp(spawnX + 3, spawnY + height);
                    hexSpawn.name = "Hex_x" + (spawnX + 3) + "_y" + (spawnY + height);
                    hexSpawn.GetComponent<Tile>().SetUp(spawnX + 3, spawnY + 11);
                    hexSpawn.name = "Hex_x" + (spawnX + 3) + "_y" + (spawnY + 11);
                    hexSpawn.tag = "Spawn2";
                    hexGrid[spawnX + 3, spawnY + height] = hexSpawn;
                }
            }
        }
        Spawn2.transform.position = positionSpawn2;
    }
}
