using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterDeathState : SKState<Character> {

	public float deathTime = 2f;
	public GameObject lavaDeathEffect;
	public GameObject soulGemPrefab;

	float startTime;

	public override void begin ()
	{
		_context.isDying = true;

		_context.GrabEscape();

		if (_context.deathType == Character.DeathTypes.Lava)
			Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);
		
		LevelManager.AddKill ();

		if (soulGemPrefab != null)
		{
			GameObject go = GameObject.Instantiate(soulGemPrefab);
			go.transform.position = transform.position;
		}

		_context.anim.SetTrigger("Death");

		startTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + deathTime)
			_context.Destroy();
	}

	public override void end ()
	{

	}
}