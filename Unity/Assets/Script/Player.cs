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

	public List<Unit> _monsters;

	public void Setup(string name, string spawn)
	{
		_name = name;
		_spawn = spawn;
		_mythologie = new Mythologie();
		_mythologie.Setup("Japonaise", new List<GameObject>(new GameObject[0]));
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag(_spawn);
		Unit unit = _monsters[0];
		
		GameObject monster = Instantiate(unit.PrefabMonster, spawn[3].transform.position, Quaternion.identity, transform) as GameObject;
		monster.tag = "Monster";
		monster.name = name;

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
