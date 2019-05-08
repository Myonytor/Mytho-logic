﻿using System.Collections;
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
	private GameObject _transform;

	public Player(string name, string spawn, List<GameObject> monsters, GameObject transform)
	{
		_name = name;
		_spawn = spawn;
		_monsters = new List<Unit>();
		_transform = transform;
		_mythologie = new Mythologie("nameMythologie", monsters);
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawns = GameObject.FindGameObjectsWithTag(_spawn);
		int l = spawns.Length;
		int i = 0;
		bool add = false;

		while (i < l && !add)
		{
			if (!spawns[i].GetComponent<Tile>().isEmpty)
			{
				Vector3 spawn = spawns[i].transform.position;
				spawns[i].GetComponent<Tile>().isEmpty = true;
				spawn.z = -1;

				GameObject prefab = _mythologie._monsters[0];
				GameObject monster = Instantiate(prefab, spawn, Quaternion.identity, _transform.transform) as GameObject;
				monster.tag = "Monster";
				monster.name = name;
				monster.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
				
				Unit unit = new Unit(name, this, monster);
				_monsters.Add(unit);

				add = true;
				Debug.Log("Add a monster in the map " + i);
			}

			i += 1;
		}

		if (!add) Debug.Log("Le monstre n'a pas pu être ajouté, manque de place");
		// message au joueur
	}

	public void Delete()
	{
		
	}
}
