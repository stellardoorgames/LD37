using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterGrabbedState : SKState<Character> {

	NavMeshAgent agent;
	Rigidbody rb;
	Animator anim;
	Collider characterCollider;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		anim = _context.anim;
		characterCollider = GetComponent<Collider>();
	}

	public override void begin ()
	{
		_context.isGrabbed = true;
		
		if (_context.grabbedObject != null)
			_context.OnGrabRelease();

		_context.GrabEscape();

		Debug.Log ("Grabbed");
		if (agent != null)
			agent.Stop ();

		if (rb != null)
			rb.isKinematic = true;

		if (characterCollider != null)
			characterCollider.enabled = false;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);

		transform.position = _context.grabber.position;

		transform.SetParent (_context.grabber);

	}

	public override void update (float deltaTime)
	{
		transform.position = Vector3.Lerp (transform.position, _context.grabber.position, 0.1f);

		//if (!_context.isGrabbed)
		//	_machine.changeState<CharacterHuntState>();
	}

	public override void end ()
	{
		Debug.Log ("Released");

		if (agent != null)
			agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;

		if (characterCollider != null)
			characterCollider.enabled = true;

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);

		_context.isGrabbed = false;
		//_context.stateMachine.changeState<CharacterHuntState>();
	}
}