using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private string _name;
	public string Name => _name;

	private string _spawn;
	public string Spawn => _spawn;

	private Mythologie _mythologie;
	public Mythologie Mythologie => _mythologie;

	public List<GameObject> _monsters;

	public void Setup(string name, string spawn)
	{
		_name = name;
		_spawn = spawn;
		_monsters = new List<GameObject>();
		_mythologie = new Mythologie();
		_mythologie.Setup("Japonaise", new List<GameObject>(new GameObject[0]));
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag(_spawn);
		GameObject unit = Instantiate(_monsters[0], spawn[3].transform.position, Quaternion.identity, transform) as GameObject;
		unit.GetComponent<Unit>().SetUp(name, this);
		unit.tag = "Monster";
		unit.name = name;

		Debug.Log("Add a monster in the map");
	}

	public void Delete()
	{
		
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
