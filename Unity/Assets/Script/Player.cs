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

	public void AddTest(string name, int power, int x, int y)
	{
		Vector3 position = new Vector3(x * 0.8f + 0.8f * y, y * 0.24f - 0.24f * x, -1);
			
		GameObject prefab = _mythologie.Monsters[0];
		GameObject monster =
			Instantiate(prefab, position, Quaternion.identity, _transform.transform) as GameObject;
		monster.tag = "Monster";
		monster.name = name;
		monster.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

		Unit unit = new Unit(name, _index, monster, new Vector2(x, y), power, prefabParticle);
		_monsters.Add(unit);

		Debug.Log("Add a monster test in the map");
	}

	// Ajoute un monstre dans une case libre du spawn du joueur
	public void Add(string name, int power)
	{
		GameObject[] spawns = GameObject.FindGameObjectsWithTag(_spawn);
		int l = spawns.Length;
		int i = 0;
		bool add = false;

		if (_monsters.Count < 6)
		{
			while (i < l && !add)
			{
				if (spawns[i].GetComponent<Tile>().isEmpty)
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
		Debug.Log(_monsters.Count);
		string name = monster.Name;
		_monsters.Remove(monster);
		monster.Delete();
		Debug.Log(_monsters.Count);
		
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
