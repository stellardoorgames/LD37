using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterSpawnState : SKState<Character> {

	public float waitTime = 1f;
	float startTime;

	public override void begin ()
	{
		_context.debugMessage("Spawning");
		startTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + waitTime)
			_machine.changeState<CharacterSearchState>();
	}

	public override void end ()
	{
		_context.debugMessage("");
	}

}
