using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private string _name;
	public string Name => _name;
	
	private Player _player;
	public Player Player => _player;

	private int _power;
	public int Power => _power;
	
	public Enum _state;
	
	public Vector2 _mouvement;
	public Vector2 _attack;
	public GameObject prefabMove;
	public GameObject prefabAttack;
	
	public int x;
	public int y;

	public void SetUp(string name, Player player) //int power)
	{
		_name = name;
		_player = player;
		_state = State.ALIVE;
		_power = 2;
	}

	/*public void Add(string name, string tagSpawn) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag(tagSpawn);

		GameObject monster = Instantiate(prefabMonster, spawn[3].transform.position, Quaternion.identity) as GameObject;
		spawn[3].GetComponent<Tile>().isEmpty = false;
		monster.transform.parent = transform;
		monster.name = name;
		monster.tag = "Monster";
		monster.SetActive(true);
		
		monsters.Add(monster);

		Debug.Log("Add a monster in the map");
	}

	public void Move(Vector3 direction, Vector3 position) // to change for a lot of move
	{
		int i = 0;
		foreach (Vector3 position in positions)
		{
			Vector3 direcion = directions[i]
		 
			GameObject monster = monsters.Find(x => x.transform.position == position);
			monster.transform.position = direction;
		
			Debug.Log(monster.name + " move");
		}
	}*/

	public enum State
	{
		DEAD,
		HURT,
		ALIVE
	}
}
