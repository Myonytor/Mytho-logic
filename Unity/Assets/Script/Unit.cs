using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour 
{
	public GameObject prefabMonster;
	public List<GameObject> monsters;

	public void SetUp()
	{
		monsters = new List<GameObject>();
	}

	public void Add(string name) // give in parameter the monster and player
	{
		GameObject[] spawn = GameObject.FindGameObjectsWithTag("Spawn1");
		int i = 0;
		int l = spawn.Length;
		bool add = false;

		while (i < l && !add)
		{
			if (spawn[i])
			{
				GameObject monster = Instantiate(prefabMonster, new Vector2(0, 0), Quaternion.identity) as GameObject;
				monster.transform.parent = transform;
				monster.name = name;
				monster.tag = "Monster";
				monster.SetActive(true);
				
				monsters.Add(monster);

				Debug.Log("Add a monster in the map");
				add = true;
			}
			
			i += 1;
		}
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
