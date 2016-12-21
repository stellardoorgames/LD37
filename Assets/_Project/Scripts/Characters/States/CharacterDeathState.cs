﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterDeathState : SKState<Character> {

	public float deathTime = 2f;
	public GameObject lavaDeathEffect;
	public GameObject soulGemPrefab;

	[HideInInspector]
	public Character.DeathTypes deathType;

	float startTime;

	Animator anim;
	NavMeshAgent agent;

	public override void onInitialized ()
	{
		anim = _context.anim;
		agent = GetComponent<NavMeshAgent>();
	}

	public override void begin ()
	{
		_context.GrabEscape();

		if (deathType == Character.DeathTypes.Lava)
			Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);
		
		agent.Stop();

		LevelManager.AddKill ();
		
		_context.anim.SetTrigger("Death");

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