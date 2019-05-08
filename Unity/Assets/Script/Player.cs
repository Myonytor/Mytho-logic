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

	public void Setup(string name, string spawn, List<GameObject> monsters)
	{
		_name = name;
		_spawn = spawn;
		_mythologie = gameObject.AddComponent<Mythologie>();
		_mythologie.Setup("Japonaise", monsters);
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawns = GameObject.FindGameObjectsWithTag(_spawn);
		Vector3 spawn = spawns[3].transform.position;
		spawn.z = -1;

		GameObject prefab = _mythologie._monsters[1];
		GameObject monster = Instantiate(prefab, spawn, Quaternion.identity, transform) as GameObject;
		monster.tag = "Monster";
		monster.name = name;
		monster.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		
		monster.AddComponent(typeof(Unit));
		monster.GetComponent<Unit>().SetUp(name, this, prefab);

		Debug.Log("Add a monster in the map");
	}

	public void Delete()
	{
		
	}
}
