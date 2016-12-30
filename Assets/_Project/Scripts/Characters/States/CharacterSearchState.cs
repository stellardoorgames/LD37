using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterSearchState : SKState<Character> {

	public List<string> searchTags;

	public float retargetInterval = 3f;
	float lastRetarget;

	NavMeshAgent agent;

	CharacterCarryState carryState;
	CharacterAttackState attackState;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
		carryState = GetComponent<CharacterCarryState>();
		attackState = GetComponent<CharacterAttackState>();
	}

	public override void begin ()
	{
		_context.debugMessage("Searching");

		agent.path = _context.GetPathToTarget(searchTags);

		lastRetarget = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > lastRetarget + retargetInterval)
		{
			_context.debugMessage("Searching");
			//if (agent.isPathStale)
			{
				agent.path = _context.GetPathToTarget(searchTags);

				lastRetarget = Time.time;
			}
			
		}
		
	}

	public override void end ()
	{
		_context.debugMessage("");
	}

	protected virtual void OnTriggerStay(Collider other)
	{
		if (!enabled)
			return;

		if (carryState != null && carryState.carryTags.Contains(other.tag))
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				carryState.StartCarry(grab);
		}
		else if (attackState != null && attackState.attackTags.Contains(other.tag))
		{
			Damageable fight = other.GetComponent<Damageable>();
			if (fight != null)
				attackState.StartAttack(fight);
		}

	}

}
