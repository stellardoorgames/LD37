using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterSpawnState : SKState<Character> {

	float startTime;
	float waitTime = 1f;

	public override void begin ()
	{
		startTime = Time.time;

	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + waitTime)
			_machine.changeState<CharacterHuntState>();
	}

	public override void end ()
	{
		
	}
}
