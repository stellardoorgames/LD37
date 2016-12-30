using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterSearchState : SKState<Character> {

	public List<string> targetTags;

	//GameObject target;

	public float retargetInterval = 3f;
	float lastRetarget;

	NavMeshAgent agent;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public override void begin ()
	{
		_context.debugMessage("Searching");

		agent.path = _context.GetPathToTarget(targetTags);

		lastRetarget = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > lastRetarget + retargetInterval)
		{
			_context.debugMessage("Searching");
			//if (agent.isPathStale)
			{
				agent.path = _context.GetPathToTarget(targetTags);

				lastRetarget = Time.time;
			}
			
		}
		
	}

	public override void end ()
	{
		_context.debugMessage("");
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (!enabled)
			return;

		if (targetTags.Contains(other.tag))
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				_context.carryState.StartCarry(grab);
			else
			{
				Damageable fight = other.GetComponent<Damageable>();
				if (fight != null)
					_context.attackState.StartAttack(fight);
			}
		}

	}

}
