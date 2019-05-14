﻿using System;
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
	
	// false correspond à vivant et true à bléssé
	public bool state;
	
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
		state = false;
		power = 2;
	}
}
