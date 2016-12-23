using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHuntState : SKState<Character> {
	
	public float retargetInterval = 3f;
	float lastRetarget;

	public override void begin ()
	{
		_context.Retarget();
		lastRetarget = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > lastRetarget + retargetInterval)
		{
			_context.Retarget();
			lastRetarget = Time.time;
		}
	}

	public override void end ()
	{
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (!enabled)
			return;

		if (_context.targetTags.Contains(other.tag))
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
