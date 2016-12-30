using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterHoldState : SKState<Character> {

	public List<string> holdTags;

	Grabbable grabbable;
	CharacterGrabbable characterGrabbable;

	NavMeshAgent agent;

	public override void onInitialized ()
	{
		characterGrabbable = GetComponent<CharacterGrabbable>();
		agent = GetComponent<NavMeshAgent>();
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
		_context.debugMessage("Holding");

		characterGrabbable.EscapedEvent();

		agent.SetDestination(grabbable.transform.position);
		//agent.Stop();
	}

	public override void update (float deltaTime)
	{
	}

	public override void end ()
	{
		_context.debugMessage("End Hold");

		grabbable.Released();

		//agent.Resume();
	}

	/*void OnTriggerEnter(Collider other)
	{
		Debug.Log("trigger enter hold");

		if (holdTags.Contains(other.tag))
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				StartHold(grab);

		}
	}*/
}
