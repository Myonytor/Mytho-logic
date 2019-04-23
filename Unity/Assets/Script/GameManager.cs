using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    private List<GameObject> images = new List<GameObject>();

    public GameObject ParentPanel;
    
    public Board board;
    // Start is called before the first frame update
    void Start()
    {
        board.Setup();
        //board.hexGrid[1, 2].GetComponentInChildren<SpriteRenderer>().color = Color.red;
        
        foreach (var currentSprite in Sprites)
        {
            GameObject newGameObject = new GameObject();
            Image newImage = newGameObject.AddComponent<Image>();
            newImage.sprite = currentSprite;
            newGameObject.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
            newGameObject.SetActive(true);
            newImage.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
            //newImage.GetComponent<Transform>().
            images.Add(newGameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
