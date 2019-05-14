using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject hoveredObject;
    public GameObject selectedObject;

    public Player player;
    private Unit unit; //unité sélectionné

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        unit = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.transform.parent.name);
            GameObject hitObject = hit.collider.gameObject;

            SelectObject(hitObject);

            if (Input.GetMouseButtonDown(0))
            {
                int i = IsBelonged(hoveredObject.transform.parent.GetComponent<Tile>().coordinate);
                if (i != -1)
                {
                    ClearSelection(selectedObject);
                    selectedObject = hitObject;
                    unit = player._monsters[i];
                    Debug.Log("Sélection d'un monstre");
                }
                if(!Equals(unit, null))
                {
                    Vector2 p = hoveredObject.transform.parent.GetComponent<Tile>().coordinate;
                    if (unit._position.y > 9)
                    {
                        if((unit._position.x < 3 && p.y < 1)
                           || (unit._position.x >= 3 && p.y >= 9))
                            unit._movement = p;
                    }
                    else
                    {
                        int x = (int) (unit._position.x - p.x), y = (int) (unit._position.y - p.y);
                        if (x <= 1 && x >= -1 && y <= 1 && y >= -1)
                        {
                            if(x == 0 || y == 0 || x != y)
                                unit._movement = p;
                        }
                    }

                    Debug.Log("Ajout d'un mouvement");
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!Equals(unit, null))
                {
                    unit._attack = hoveredObject.transform.parent.GetComponent<Tile>().coordinate;
                    Debug.Log("Ajout d'une attaque");
                }
            }

            if (selectedObject != null)
            {
                selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public void ChangePlayer(Player p)
    {
        player = p;
    }
    
    void SelectObject(GameObject hitObject)
    {
        if (hoveredObject != null)
        {
            if (hitObject == hoveredObject)
                return;
            ClearSelection(hoveredObject);
        }

        hoveredObject = hitObject;
        hoveredObject.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    void ClearSelection(GameObject objectToClear)
    {
        if (objectToClear == null)
            return;
        objectToClear.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.56f);
        objectToClear = null;
    }

    int IsBelonged(Vector2 vect)//return -1 if the tile does not belong to the player, and return the index of the monster otherwise
    {
        int i = 0;
        while (i < player._monsters.Count && vect != player._monsters[i]._position)
            i += 1;
        return (i == player._monsters.Count ? -1 : i);
    }

    public void Clear()
    {
        ClearSelection(hoveredObject);
        ClearSelection(selectedObject);
        unit = null;
        selectedObject = null;
    }
}
