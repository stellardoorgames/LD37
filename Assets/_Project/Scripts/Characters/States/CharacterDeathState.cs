using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterDeathState : SKState<Character> {

	public override void begin ()
	{
		LevelManager.AddKill ();

		_context.Destroy();
	}

	public override void update (float deltaTime)
	{

	}

	public override void end ()
	{

	}
}