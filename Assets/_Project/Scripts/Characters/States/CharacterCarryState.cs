﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterCarryState : SKState<Character> {

	public string carryDestination = "Exit";

	Grabbable carriedObject;
	
	public void StartCarry(Grabbable grabbable)
	{
		bool canGrab = grabbable.Grabbed(transform);

		if (canGrab)
		{
			carriedObject = grabbable;
			_machine.changeState<CharacterCarryState>();
		}
	}

	public override void begin ()
	{
		carriedObject.OnEscaped += OnCarryEscape;
		_context.Retarget(carryDestination);
	}

	public override void update (float deltaTime)
	{
	}

	public override void end ()
	{
		Debug.Log("Drop");

		if (carriedObject != null)
		{
			carriedObject.OnEscaped -= OnCarryEscape;
			carriedObject.Released();
		}

		carriedObject = null;
		
		_context.currentTarget = "";

	}

	public void OnCarryEscape()
	{
		_machine.changeState<CharacterHuntState>();
	}
}