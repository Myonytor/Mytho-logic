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
	
	// false correspond à vivant et true à bléssé
	public bool state;
	
	public Vector2 _movement;
	public Vector2 _attack;
	private GameObject particleMove;
	private GameObject particleAttack;

	public Vector2 _position;

	public GameObject prefabMonster;

	public Unit(string name, int player, GameObject monster, Vector2 position, int power, GameObject prefabParticle)
	{
		prefabMonster = monster;
		_name = name;
		_player = player;
		_position = position;
		_movement = Vector2.negativeInfinity;
		_attack = Vector2.negativeInfinity;
		state = false;
		_power = power;
		
		particleMove = Instantiate(prefabParticle);
		particleMove.GetComponent<ParticleSystem>().enableEmission = false;
		particleMove.GetComponent<ParticleSystem>().startColor = Color.blue;
		particleMove.transform.parent = prefabMonster.transform.parent;
		
		particleAttack = Instantiate(prefabParticle);
		particleAttack.GetComponent<ParticleSystem>().enableEmission = false;
		particleAttack.GetComponent<ParticleSystem>().startColor = Color.red;
		particleAttack.transform.parent = prefabMonster.transform.parent;
	}
	
	public void DefineParticleMovement(Vector3 direction)
	{
		var origin = prefabMonster.transform.position;
		var particleSystem = particleMove.GetComponent<ParticleSystem>();
        float x = direction.x - origin.x, y = direction.y - origin.y;

		particleAttack.transform.position = direction;
		ClearParticleAttack();
		
		particleMove.transform.position = origin;
        particleSystem.enableEmission = true;
		
        double teta = Math.Atan2(y, x) * 180 / Math.PI;
        particleSystem.transform.Rotate(0, (float)(teta), 0);

        double distance = Math.Sqrt(x * x + y * y);
        particleSystem.startLifetime = (float)distance / 3;
    }

	public void DefineParticleAttack(Vector3 direction)
	{
		var origin = particleAttack.transform.position;
		var particleSystem = particleAttack.GetComponent<ParticleSystem>();
        float x = direction.x - origin.x, y = direction.y - origin.y;
       
        particleSystem.enableEmission = true;
		
        double teta = Math.Atan2(y, x) * 180 / Math.PI;
        particleSystem.transform.Rotate(0, (float)(teta), 0);

        double distance = Math.Sqrt(x * x + y * y);
        particleSystem.startLifetime = (float)distance;
	}

	public void ClearParticleMovement()
	{
		var p = particleMove.GetComponent<ParticleSystem>();
		p.enableEmission = false;
		p.Clear();
		p.transform.eulerAngles = new Vector3(0, 90, 90);
	}
	
	public void ClearParticleAttack()
	{
		var p = particleAttack.GetComponent<ParticleSystem>();
		p.enableEmission = false;
		p.Clear();
		p.transform.eulerAngles = new Vector3(0, 90, 90);
	}
}
