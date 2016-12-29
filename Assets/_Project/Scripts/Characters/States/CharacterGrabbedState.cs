using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterGrabbedState : SKState<Character> {

	Animator anim;
	NavMeshAgent agent;
	Grabbable grabbable;

	public override void onInitialized ()
	{
		anim = _context.anim;
		agent = GetComponent<NavMeshAgent>();
		grabbable = GetComponent<Grabbable>();
	}

	public override void begin ()
	{
		grabbable.isGrabbed = true;

		//If the character is being grabbed away from another grabber? Right now that won't happen
		//grabbable.EscapedEvent();

		Debug.Log (name + " Grabbed by " + grabbable.grabber.name);
		if (agent != null)
		{
			if (agent.isOnNavMesh)
				agent.Stop ();
			agent.enabled = false;
		}

		if (anim != null)
			anim.SetBool ("isGrabbed", true);


		transform.position = grabbable.grabber.position;

		transform.SetParent (grabbable.grabber);

	}

	public override void update (float deltaTime)
	{
		//transform.position = Vector3.Lerp (transform.position, _context.grabber.position, 0.1f);
		transform.position = grabbable.grabber.position;
	}

	public override void end ()
	{
		Debug.Log ("Released");

		if (agent != null)
		{
			agent.enabled = true;
			if (agent.isOnNavMesh)
				agent.Resume ();
		}

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);

		grabbable.isGrabbed = false;
	}
}