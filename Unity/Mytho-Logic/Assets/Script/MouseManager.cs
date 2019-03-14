using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject hoveredObject;
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.transform.parent.name);
            GameObject hitObject = hit.collider.gameObject;

            SelectObject(hitObject);

            if (Input.GetMouseButton(0))
            {
                ClearSelection(selectedObject);
                selectedObject = hitObject;
            }
            if (selectedObject != null)
            {
                selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            ClearSelection(hoveredObject);
        }
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
        objectToClear.GetComponent<SpriteRenderer>().color = Color.white;
        objectToClear = null;
    }
}
