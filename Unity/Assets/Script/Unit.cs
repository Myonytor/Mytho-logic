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
	
	public Vector2 _mouvement;
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
		state = State.ALIVE;
		power = 2;
	}

	public enum State
	{
		DEAD,
		HURT,
		ALIVE
	}
}
