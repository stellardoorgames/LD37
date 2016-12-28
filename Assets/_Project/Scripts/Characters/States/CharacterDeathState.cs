using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterDeathState : SKState<Character> {

	public float deathTime = 2f;
	//public GameObject lavaDeathEffect;
	public GameObject soulGemPrefab;

	Character.DeathType deathType;

	float startTime;

	Animator anim;
	NavMeshAgent agent;

	Grabbable grabbable;

	public override void onInitialized ()
	{
		anim = _context.anim;
		agent = GetComponent<NavMeshAgent>();
		grabbable = GetComponent<Grabbable>();
	}

	public void StartDeath(Character.DeathType deathType)
	{
		this.deathType = deathType;
		_machine.changeState<CharacterDeathState>();
	}

	public override void begin ()
	{
		Debug.Log (string.Format("{0} died from {1}.", name, deathType));

		tag = "Untagged";

		grabbable.EscapedEvent();

		//if (deathType == Character.DeathType.Lava)
		//	Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);
		
		agent.Stop();

		//LevelManager.AddKill ();
		if (gameObject.tag == "Enemy")
			LevelManager.IncrementStat(Stats.EnemiesKilled);
		
		anim.SetTrigger("Death");

		startTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + deathTime)
		{
			if (soulGemPrefab != null)
			{
				GameObject go = GameObject.Instantiate(soulGemPrefab);
				go.transform.position = transform.position;
			}
			_context.Destroy();
		}
	}

	public override void end ()
	{

	}
}