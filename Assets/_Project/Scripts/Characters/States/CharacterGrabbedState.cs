using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterGrabbedState : SKState<Character> {

	Animator anim;
	NavMeshAgent agent;

	public override void onInitialized ()
	{
		anim = _context.anim;
		agent = GetComponent<NavMeshAgent>();
	}

	public override void begin ()
	{
		_context.isGrabbed = true;
		
		if (_context.carriedObject != null)
			_context.OnCarryRelease();

		_context.GrabEscape();

		Debug.Log ("Grabbed");
		if (agent != null)
		{
			agent.Stop ();
			agent.enabled = false;
		}

		if (anim != null)
			anim.SetBool ("isGrabbed", true);


		transform.position = _context.grabber.position;

		transform.SetParent (_context.grabber);

	}

	public override void update (float deltaTime)
	{
		//transform.position = Vector3.Lerp (transform.position, _context.grabber.position, 0.1f);
		transform.position = _context.grabber.position;
	}

	public override void end ()
	{
		Debug.Log ("Released");

		if (agent != null)
		{
			agent.enabled = true;
			agent.Resume ();
		}

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);

		_context.isGrabbed = false;
	}
}