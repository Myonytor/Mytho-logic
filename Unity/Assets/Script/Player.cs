﻿using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private string _name;
	public string Name => _name;

	private int _index;
	public int Index => _index;

	private string _spawn;
	public string Spawn => _spawn;

	private Mythologie _mythologie;
	public Mythologie Mythologie => _mythologie;

	public List<Unit> _monsters;
	private GameObject _transform;

	private List<GameObject> prefabSmall;
	private GameObject prefabParticle;

    public Player(string name, string spawn, List<GameObject> monsters, GameObject transform, GameObject particle, int mythologie, int id)
	{
		_name = name;
		_index = id;
		_spawn = spawn;
		_monsters = new List<Unit>();
		_transform = transform;
		_mythologie = new Mythologie(mythologie, monsters.GetRange(0, 12), id);
		prefabSmall = monsters.GetRange(12, 6);
		prefabParticle = particle;
	}

	// Ajoute un monstre dans une case libre du spawn du joueur
	public void Add(int index)
	{
		var t = _mythologie.GetArg(index);
		var name = t.Item1;
		var power = t.Item2;
		GameObject[] spawns = GameObject.FindGameObjectsWithTag(_spawn);
		int l = spawns.Length;
		int i = 0;
		bool add = false;

		if (_monsters.Count < 6) // Empêche d'ajouter plus de 6 monstres
		{
			while (i < l && !add) // Tant qu'il y a des cases de spawn
			{
				if (spawns[i].GetComponent<Tile>().isEmpty) // Vérifie si la case de spwan sélectionnée est vide
				{
					Vector3 spawn = spawns[i].transform.position;
					spawns[i].GetComponent<Tile>().isEmpty = false;
					spawn.z = -5 + spawn.y;
					spawn.y += 0.125f;

					// Récupération de la prefab du monstre voulu
					GameObject prefab;
					if (index * 2 < 12) prefab = _mythologie.Monsters[index * 2 % _mythologie.Monsters.Count];
					else prefab = prefabSmall[index * 2 % 12];
					
					GameObject monster =
						Instantiate(prefab, spawn, Quaternion.identity, _transform.transform) as GameObject;
					monster.name = name;
					monster.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

					Unit unit = new Unit(name, _index, monster, spawns[i].GetComponent<Tile>().coordinate, power, prefabParticle);
					_monsters.Add(unit);

					add = true;
				}

				i += 1;
			}
		}
		else
		{
			// message au joueur
			Debug.Log("Il y a déjà 6 monstres sur le plateau");
		}
	}

	// Supprime un monstre
	public void Delete(Unit monster)
	{
		string name = monster.Name;
		for (int i = 0; i < _monsters.Count; i++)
		{
			if (_monsters[i]._position == monster._position)
			{
				_monsters.RemoveAt(i);
				i = _monsters.Count;
			}
		}
		monster.Delete();
        FindObjectOfType<AudioManager>().Play("Die1Sound");
	}
	
	// Vérifie si la case est vide
	public bool IsCaseEmpty(Vector2 destination)
	{
		bool output = true;
		for (int i = 0; i < _monsters.Count && output; i++)
		{
			if(_monsters[i]._position.y > 9)
				output = _monsters[i]._movement != destination || _monsters[i]._movement == Vector2.zero;
			else
				output = _monsters[i]._movement + _monsters[i]._position != destination;
		}

		return output;
	}
}
