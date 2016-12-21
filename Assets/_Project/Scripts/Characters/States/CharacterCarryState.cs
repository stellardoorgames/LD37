using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterCarryState : SKState<Character> {

	public string carryDestination = "Exit";

	public override void begin ()
	{
		_context.Retarget(carryDestination);
	}

	public override void update (float deltaTime)
	{
		
	}

	public override void end ()
	{

	}
}