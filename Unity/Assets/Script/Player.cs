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

	private GameObject prefabParticle;

	public Player(string name, string spawn, List<GameObject> monsters, GameObject transform, GameObject particle, int mythologie, int id)
	{
		_name = name;
		_index = id;
		_spawn = spawn;
		_monsters = new List<Unit>();
		_transform = transform;
		_mythologie = new Mythologie(mythologie, monsters, id);
		prefabParticle = particle;
	}

	// Ajoute un monstre dans une case libre du spawn du joueur
	public void Add(string name, int power)
	{
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
					spawn.z = -1;

					GameObject prefab = _mythologie.Monsters[0];
					GameObject monster =
						Instantiate(prefab, spawn, Quaternion.identity, _transform.transform) as GameObject;
					monster.tag = "Monster";
					monster.name = name;
					monster.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

					Unit unit = new Unit(name, _index, monster, spawns[i].GetComponent<Tile>().coordinate, power, prefabParticle);
					_monsters.Add(unit);

					add = true;
				}

				i += 1;
			}

			if (!add) Debug.Log("Le monstre n'a pas pu être ajouté, manque de place");
			// message au joueur
		}
		else
		{
			Debug.Log("Il y a déjà 6 monstres sur le plateau");
		}
	}

	// Supprime un monstre
	public void Delete(Unit monster)
	{
		string name = monster.Name;
		for (int i = 0; i < _monsters.Count; i++)
		{
			if (_monsters[i].Name == name)
			{
				_monsters.RemoveAt(i);
				i = _monsters.Count;
			}
		}
		monster.Delete();
		
		Debug.Log(name + " à été tué");
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
