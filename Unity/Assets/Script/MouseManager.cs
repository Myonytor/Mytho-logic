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
                if (i != -1)//selectionne une unite ou reinitialise le mouvement de l'unite selectionnee
                {
                    if (!Equals(null, unit) && unit._position == player._monsters[i]._position)
                    {
                        unit._movement = Vector2.zero;
                        unit._attack = Vector2.zero;
                        unit.ClearParticleAttack();
                        unit.ClearParticleMovement();
                    }
                    else
                    {
                        ClearSelection(selectedObject);
                        selectedObject = hitObject;
                        unit = player._monsters[i];
                        Debug.Log("Sélection d'un monstre");
                    }
                }
                else if(!Equals(unit, null))//definie un mouvement
                {
                    Vector2 p = hoveredObject.transform.parent.GetComponent<Tile>().coordinate;
                    int x = (int) (p.x - unit._position.x), y = (int) (p.y - unit._position.y);
                    if (player.IsCaseEmpty(p))
                    {
                        if ((unit._position.y > 9 && ((unit._position.x < 3 && (int) p.y == 0) || (unit._position.x >= 3 && (int) p.y == 9)))) //personnage dans le spawn et case d'arrivee sur le bord du plateau
                        {
                            unit.DefineMovement(new Vector2(x, y), hoveredObject.transform.position);
                        }
                        else if (unit._position.y <= 9 && IsClickable(x, y)) //ou la case de depart n'est pas dans le spawn et la case d'arrivee est accessible 
                        {
                            unit.DefineMovement(new Vector2(x, y), hoveredObject.transform.position);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))//definie l'attaque
            {
                if (!Equals(unit, null))
                {
                    Vector2 p = hoveredObject.transform.parent.GetComponent<Tile>().coordinate;
                    int x = (int) (p.x - unit._position.x - unit._movement.x), y = (int) (p.y - unit._position.y - unit._movement.y);
                    if (IsClickable(x, y))
                    {
                        unit.DefineAttack(new Vector2(x, y), hoveredObject.transform.position);
                        Debug.Log("Ajout d'une attaque");
                    }
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
        //objectToClear = null; //semble inutile
    }

    int IsBelonged(Vector2 vect)//return -1 if the tile does not belong to the player, and return the index of the monster otherwise
    {
        int i = 0;
        while (i < player._monsters.Count && vect != player._monsters[i]._position)
            i += 1;
        return (i == player._monsters.Count ? -1 : i);
    }

    bool IsClickable(int x, int y)//x et y sont les differences des coordonnees de depart par celles d'arrivees
    {//cette fonction teste si la case de depart et celle d'arrivee sont adjacentes
        return x <= 1 && x >= -1 && y <= 1 && y >= -1 && x + y <= 1 && x + y >= -1;
    }

    public void Clear()
    {
        ClearSelection(hoveredObject);
        ClearSelection(selectedObject);
        unit = null;
        selectedObject = null;
        foreach (var m in player._monsters)
        {
            m.ClearParticleMovement();
            m.ClearParticleAttack();
        }
        player = null;
    }
}
