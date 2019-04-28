using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	string _name;
	Player _player;
	
	int _power;
	int _state;
	
	Vector2 _mouvement;
	Vector2 _attack;
	int x;
	int y;
	
	public GameObject prefabMonster;
	public List<GameObject> monsters;

	public void Move()
	{
		
	}

	public void SetUp()
	{
		monsters = new List<GameObject>();
	}

	public void Add(string name, string tagSpawn) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag(tagSpawn);

		GameObject monster = Instantiate(prefabMonster, spawn[3].transform.position, Quaternion.identity) as GameObject;
		spawn[3].GetComponent<Tile>().isEmpty = false;
		monster.transform.parent = transform;
		monster.name = name;
		monster.tag = "Monster";
		monster.SetActive(true);
		
		spawn[3].GetComponent<Tile>().monster = monster;
		monsters.Add(monster);

		Debug.Log("Add a monster in the map");
	}

	public void Move(Vector3 direction, Vector3 position) // to change for a lot of move
	{
		/* int i = 0;
		 * foreach (Vector3 position in positions)
		 * {
		 * 		Vector3 direcion = directions[i]
		 */
		GameObject monster = monsters.Find(x => x.transform.position == position);
		monster.transform.position = direction;
		
		Debug.Log(monster.name + " move");
	}
}
