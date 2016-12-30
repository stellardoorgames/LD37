using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrabbable : Grabbable {

	protected Character character;

	CharacterDeathState deathState;

	protected virtual void Awake()
	{
		character = GetComponent<Character>();

		deathState = GetComponent<CharacterDeathState>();
	}

	public override bool Grabbed (Transform grabber)
	{
		if (deathState != null && deathState.enabled)
			return false;
		
		base.Grabbed(grabber);

		character.stateMachine.changeState<CharacterGrabbedState>();

		return true;
	}

	public override void Released ()
	{
		if (isGrabbed == false)
			return;

		base.Released();

		if (deathState != null && deathState.enabled)
			return;

		character.stateMachine.changeState<CharacterSearchState>();
	}
	
}
