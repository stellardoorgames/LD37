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

		if (other.tag == _context.currentTarget)
		{
			IGrabbable grab = other.GetComponent<IGrabbable>();
			if (grab != null)
				_context.AttemptToCarry(other.gameObject);
			else
			{
				IDamagable fight = other.GetComponent<IDamagable>();
				if (fight != null)
				{
					_context.attackState.attackTarget = fight;
					_machine.changeState<CharacterAttackState>();
				}

			}
		}

	}
}
