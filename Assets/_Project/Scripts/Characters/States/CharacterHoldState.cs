using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHoldState : SKState<Character> {

	Grabbable grabbable;
	CharacterGrabbable characterGrabbable;

	public override void onInitialized ()
	{
		characterGrabbable = GetComponent<CharacterGrabbable>();
		
	}

	public void StartHold(Grabbable target)
	{
		grabbable = target;

		bool canGrab = grabbable.Grabbed(transform);

		if (canGrab)
			_machine.changeState<CharacterHoldState>();
	}

	public override void begin ()
	{
		characterGrabbable.EscapedEvent();
	}

	public override void update (float deltaTime)
	{
	}

	public override void end ()
	{
		grabbable.Released();
	}

}
