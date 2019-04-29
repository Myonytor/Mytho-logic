using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private string _name;
	public string Name => _name;

	private string _spawn;
	public string Spawn => _spawn;

	private Mythologie _mythologie;
	public Mythologie Mythologie => _mythologie;

	public List<Unit> monsters;

	public void Setup(string name, string spawn)
	{
		_name = name;
		_spawn = spawn;
		monsters = new List<Unit>();
		_mythologie = new Mythologie();
		_mythologie.Setup("Japonaise", new List<GameObject>(new GameObject[0]));
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag(_spawn);

		Unit unit = new Unit();
		unit.monster = Instantiate(unit.prefabMonster, spawn[3].transform.position, Quaternion.identity) as GameObject;
		unit.monster.transform.parent = transform;
		unit.monster.tag = "Monster";

		unit.SetUp(name, this);
		spawn[3].GetComponent<Tile>().isEmpty = false;
		monsters.Add(unit);

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
