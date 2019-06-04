using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    public float scrollSpeed = 0.5f;
    private int maxCameraDistance = 10;
    private int minCameraDistance = 2;
    private int maxXCameraPosition = 20;
    private int maxYCameraPosition = 10;
    private int minXCameraPosition = -5;
    private int minYCameraPosition = -10;

    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }
    
    void LateUpdate()
    {
        // Gestion du mouvement de la camera
        if (Input.GetMouseButton(2))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Vector3 PosFinal = Origin - Diference;
            if (PosFinal.x < maxXCameraPosition && PosFinal.y < maxYCameraPosition && PosFinal.x > minXCameraPosition && PosFinal.y > minYCameraPosition)
            Camera.main.transform.position = PosFinal;
        }
        // Gestion du zoom de la camera
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().orthographicSize > minCameraDistance)
        {
            GetComponent<Camera>().orthographicSize -= scrollSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().orthographicSize < maxCameraDistance)
        {
            GetComponent<Camera>().orthographicSize += scrollSpeed;
        }
    }
}
