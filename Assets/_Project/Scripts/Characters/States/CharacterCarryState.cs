using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterCarryState : SKState<Character> {

	public string carryDestination = "Exit";

	public float retargetInterval = 3f;
	float retargetTime;

	Grabbable carriedObject;

	NavMeshAgent agent;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
	}

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
		_context.debugMessage("Carrying");

		carriedObject.OnEscaped += OnCarryEscape;

		agent.path = _context.GetPathToTarget(carryDestination);
		retargetTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > retargetTime + retargetInterval)
		{
			if (carriedObject == null)
			{
				_machine.changeState<CharacterSearchState>();
			}
			else if (agent.isPathStale)
			{
				agent.path = _context.GetPathToTarget(carryDestination);
				retargetTime = Time.time;
			}
		}
	}

	public override void end ()
	{
		_context.debugMessage("");

		Debug.Log("Drop");

		if (carriedObject != null)
		{
			carriedObject.OnEscaped -= OnCarryEscape;
			carriedObject.Released();
		}

		carriedObject = null;
		
		//_context.currentTarget = "";

	}

	public void OnCarryEscape()
	{
		_machine.changeState<CharacterSearchState>();
	}
}