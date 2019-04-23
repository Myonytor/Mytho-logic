using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Board boardParent;
    public int x;
    public int y;
    
    public bool isEmpty;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        float xPos = position.x;
        float yPos = position.y;
        float xOffset = 0.8f;
        float yOffset = 0.24f;
        int i = 0;
        int j = 0;
        int length = 10;
        bool here = false;

        while (i < length && !here)
        {
            while (j < length && !here)
            {
                if (i * xOffset + j * yOffset == xPos && j * xOffset - i * yOffset == yPos)
                {
                    x = i;
                    y = j;
                    here = true;
                }

                if (j * xOffset + i * yOffset == xPos && i * xOffset - j * yOffset == yPos)
                {
                    x = j;
                    y = i;
                    here = true;
                }

                j += 1;
            }

            i += 1;
        }
        
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, transform.right * 0.5f, Color.red);

        if (Physics.Raycast(transform.position, transform.right, out hit, 0.5f))
        {
            isEmpty = false;
            Debug.Log("Le raycast touche un monstre !");
        }
        else isEmpty = true;
    }
}
