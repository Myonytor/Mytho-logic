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

	private int power;
	public int Power => power;
	
	public Enum state;
	
	public Vector2 _movement;
	public Vector2 _attack;
	public GameObject prefabMove;
	public GameObject prefabAttack;

	public Vector2 _position;

	public GameObject PrefabMonster;

	public Unit(string name, Player player, GameObject monster, Vector2 position) //int power)
	{
		PrefabMonster = monster;
		_name = name;
		_player = player;
		_position = position;
		_movement = Vector2.negativeInfinity;
		_attack = Vector2.negativeInfinity;
		state = State.ALIVE;
		power = 2;
	}

	/*
	 * Avant d'appelle de cette fonction, il faut changer le isEmpty de la case
	 * sur laquelle se trouve le monstre ainsi que celui de sa case d'arrivée.
	 */
	public void Move()
	{
		if (Equals(_movement, Vector2.negativeInfinity))
		{
			Debug.Log("Il n'y a pas de mouvement à faire pour " + _name);
		}
		else
		{
			float xOffset = 0.8f;
			float yOffset = 0.24f;

			_position = _movement;
			PrefabMonster.transform.position = new Vector3(_position.x * xOffset + _position.y * xOffset,
				_position.y * yOffset - _position.x * yOffset);
			_movement = new Vector2(-1, -1);

			Debug.Log(_name + " c'est déplacer");
		}
	}

	public enum State
	{
		DEAD,
		HURT,
		ALIVE
	}
}
