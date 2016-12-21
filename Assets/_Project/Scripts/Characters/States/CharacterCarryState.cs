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
		if (_context.grabbedObject == null)
			_machine.changeState<CharacterHuntState>();
	}

	public override void end ()
	{

	}
}