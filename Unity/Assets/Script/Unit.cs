using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private string _name;
	public string Name => _name;
	
	private int _player;
	public int Player => _player;

	private int _power;
	public int Power => _power;
	
	// false correspond à vivant et true à blessé
	public bool wounded;
	
	public Vector2 _movement;
	public Vector2 _attack;
	private GameObject particleMove;
	private GameObject particleAttack;

	public Vector2 _position;

	private GameObject prefabMonster;

	public Unit(string name, int player, GameObject monster, Vector2 position, int power, GameObject prefabParticle)
	{
		prefabMonster = monster;
		_name = name;
		_player = player;
		_position = position;
		_movement = Vector2.zero;
		_attack = Vector2.zero;
		wounded = false;
		_power = power;
		
		particleMove = Instantiate(prefabParticle);
		var e = particleMove.GetComponent<ParticleSystem>().emission;
		e.enabled = false;
		var m = particleMove.GetComponent<ParticleSystem>().main;
		m.startColor = Color.blue;
		particleMove.transform.parent = prefabMonster.transform;
		particleMove.transform.position = monster.transform.position;
		
		particleAttack = Instantiate(prefabParticle);
		e = particleAttack.GetComponent<ParticleSystem>().emission;
		e.enabled = false;
		m = particleAttack.GetComponent<ParticleSystem>().main;
		m.startColor = Color.red;
		particleAttack.transform.parent = prefabMonster.transform;
		particleAttack.transform.position = monster.transform.position;
	}
	
	public void DefineMovement(Vector2 coordinates, Vector3 direction)
	{
		var origin = prefabMonster.transform.position;
		var particleSystem = particleMove.GetComponent<ParticleSystem>();
        float x = direction.x - origin.x, y = direction.y - origin.y;

		ClearParticleAttack();
		particleAttack.transform.position = new Vector3(direction.x, direction.y, -1);
		_movement = coordinates;
		_attack = Vector2.zero;
		
        var e = particleSystem.emission;
		e.enabled = true;
		
        double teta = Math.Atan2(y, x) * 180 / Math.PI;
		particleSystem.transform.eulerAngles = new Vector3((float)(-teta), 90, 90);

        double distance = Math.Sqrt(x * x + y * y);
        var m = particleSystem.main;
		m.startLifetime = (float)distance / 3;
    }

	public void DefineAttack(Vector2 coordinates, Vector3 direction)
	{
		var origin = particleAttack.transform.position;
		var particleSystem = particleAttack.GetComponent<ParticleSystem>();
        float x = direction.x - origin.x, y = direction.y - origin.y;

		_attack = coordinates;
		
        var e = particleSystem.emission;
		e.enabled = true;
		
        double teta = Math.Atan2(y, x) * 180 / Math.PI;
		particleSystem.transform.eulerAngles = new Vector3((float)(-teta), 90, 90);

        double distance = Math.Sqrt(x * x + y * y);
        var m = particleSystem.main;
		m.startLifetime = (float)distance;
	}

	public void MovePrefab(Vector3 direction)
	{
		var vec = new Vector3(direction.x, direction.y, -1);
		prefabMonster.transform.position = vec;
		particleAttack.transform.position = vec;
		particleMove.transform.position = vec;
	}

	public void ClearParticleMovement()
	{
		var p = particleMove.GetComponent<ParticleSystem>();
		var e = p.emission;
		e.enabled = false;
		p.Clear();
		p.transform.eulerAngles = new Vector3(0, 90, 90);
		particleMove.transform.position = prefabMonster.transform.position;
	}
	
	public void ClearParticleAttack()
	{
		var p = particleAttack.GetComponent<ParticleSystem>();
		var m = p.emission;
		m.enabled = false;
		p.Clear();
		p.transform.eulerAngles = new Vector3(0, 90, 90);
		particleAttack.transform.position = prefabMonster.transform.position;
	}

	public void Delete()
	{
		Destroy(prefabMonster);
		Destroy(particleAttack);
		Destroy(particleMove);
	}
}
