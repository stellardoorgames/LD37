using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterDeathState : SKState<Character> {

	float deathTime = 2f;
	float startTime;

	public override void begin ()
	{
		LevelManager.AddKill ();

		if (_context.soulGemPrefab != null)
		{
			GameObject go = GameObject.Instantiate(_context.soulGemPrefab);
			go.transform.position = _context.transform.position;
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