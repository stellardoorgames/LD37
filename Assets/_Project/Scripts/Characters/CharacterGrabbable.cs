using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrabbable : Grabbable {

	protected Character character;

	protected virtual void Awake()
	{
		character = GetComponent<Character>();
	}

	public override bool Grabbed (Transform grabber)
	{
		if (character.deathState.enabled)
			return false;

		//if (character.carryState.carriedObject != null)
		//	_context.carryState.OnCarryRelease();
		
		this.grabber = grabber;

		character.stateMachine.changeState<CharacterGrabbedState>();

		return true;
	}

	public override void Released ()
	{
		if (isGrabbed == false)
			return;

		if (!character.deathState.enabled)
			character.stateMachine.changeState<CharacterHuntState>();
	}
	
}
