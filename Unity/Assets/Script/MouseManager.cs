using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    public GameObject hoveredObject;
    public GameObject selectedObject;

    public Vector2[] goal= new Vector2[3];
    public bool onMenu;
    
    public Player player;
    private Unit unit; //unité sélectionné

    public GameObject AudioManager;

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

        if (hit.collider != null && !onMenu && !IsPointerOverUIObject())
        {
            GameObject hitObject = hit.collider.gameObject;

            SelectObject(hitObject);

            if (Input.GetMouseButtonDown(0))
            {
                int i = IsBelonged(hoveredObject.transform.parent.GetComponent<Tile>().coordinate);
                if (i != -1) // Sélectionne une unité ou réinitialise le mouvement de l'unité sélectionnée
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
                        AudioManager.GetComponent<AudioManager>().Play("" + unit.Name);
                    }
                }
                else if(!Equals(unit, null)) // Définie un mouvement
                {
                    Vector2 p = hoveredObject.transform.parent.GetComponent<Tile>().coordinate;
                    int x = (int) (p.x - unit._position.x), y = (int) (p.y - unit._position.y);
                    if (player.IsCaseEmpty(p))
                    {
                        if ((unit._position.y > 10 && ((unit._position.x < 3 && (int) p.y == 0) || (unit._position.x >= 3 && (int) p.y == 10))))
                            // Si le personnage est dans le spawn et la case d'arrivée sur le bord du plateau
                        {
                            unit.DefineMovement(new Vector2(x, y), hoveredObject.transform.position);
                        }
                        else if (unit._position.y <= 10 && IsClickable(x, y)) // Sinon, ou la case de départ n'est pas dans le spawn et la case d'arrivée est accessible
                        {
                            unit.DefineMovement(new Vector2(x, y), hoveredObject.transform.position);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1)) // Définie l'attaque
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
                if (IsAGoal(selectedObject.transform.parent.GetComponent<Tile>().coordinate))
                    selectedObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.38f, 0f);
                else
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
        if (IsAGoal(hoveredObject.transform.parent.GetComponent<Tile>().coordinate))
            hoveredObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.66f, 0f);
        else
            hoveredObject.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    void ClearSelection(GameObject objectToClear)
    {
        if (objectToClear == null)
            return;
        if (IsAGoal(objectToClear.transform.parent.GetComponent<Tile>().coordinate))
            objectToClear.GetComponent<SpriteRenderer>().color = new Color(1f, 0.66f, 0f, 0.56f);
        else
            objectToClear.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.56f);
        //objectToClear = null; //semble inutile
    }

    // Retourne -1 si la case n'appartient pas au joueur, sinon, retourne l'index du monstre
    int IsBelonged(Vector2 vect)
    {
        int i = 0;
        while (i < player._monsters.Count && vect != player._monsters[i]._position)
            i += 1;
        return (i == player._monsters.Count ? -1 : i);
    }

    // Teste si la case de départ et celle d'arrivée sont adjacentes
    bool IsClickable(int x, int y)
    {
        // x et y sont les différences des coordonnées de départ par celles d'arrivées
        return x <= 1 && x >= -1 && y <= 1 && y >= -1 && x + y <= 1 && x + y >= -1;
    }

    bool IsAGoal(Vector2 v)
    {
        bool output = false;
        for (int i = 0; i < goal.Length && !output; i ++)
        {
            output = v == goal[i];
        }

        return output;
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

    public void AddMonsterOnSpawn(int index)
    {
        player.Add(index);
    }
    
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    } 
}
