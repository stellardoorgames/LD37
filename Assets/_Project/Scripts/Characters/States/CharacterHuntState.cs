using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHuntState : SKState<Character> {

	//public List<string> targetTags = new List<string>();
	//public string stealTag;
	//public string attackTag;

	public float retargetInterval = 3f;
	float lastRetarget;

	public override void begin ()
	{
		/*if (stealTag != null && stealTag != "")
			_context.Retarget(stealTag);
		else
			_context.Retarget(attackTag);*/
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
				_context.AttemptToGrab(other.gameObject);
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

		/*if (other.tag == stealTag)
		{
			_context.AttemptToGrab(other.gameObject);
		}
		if (other.tag == attackTag)
		{
			IDamagable d = other.GetComponent<IDamagable>();
			if (d != null)
			{
				_context.attackState.attackTarget = d;
				_machine.changeState<CharacterAttackState>();
			}
				//_context.Attack(d);
		}*/
	}
}
