using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHuntState : SKState<Character> {
	
	public float retargetInterval = 3f;
	float lastRetarget;
	bool isHunting = false;

	public override void begin ()
	{
		isHunting = true;
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
		isHunting = false;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		//Debug.Log(enabled);
		if (!enabled)
			return;

		if (_context.targetTags.Contains(other.tag))
		{
			IGrabbable grab = other.GetComponent<IGrabbable>();
			if (grab != null)
				_context.AttemptToCarry(other.gameObject);
			else
			{
				IDamagable fight = other.GetComponent<IDamagable>();
				if (fight != null)
				{
					Debug.Log("Fight!");
					_context.attackState.attackTarget = fight;
					_context.attackState.attackTag = other.tag;
					_machine.changeState<CharacterAttackState>();
				}

			}
		}

	}
/*
	protected virtual void OnTriggerStay(Collider other)
	{
		//Debug.Log(enabled);
		if (!enabled)
			return;

		if (_context.targetTags.Contains(other.tag))
		{
			IGrabbable grab = other.GetComponent<IGrabbable>();
			if (grab != null)
				_context.AttemptToCarry(other.gameObject);
			else
			{
				IDamagable fight = other.GetComponent<IDamagable>();
				if (fight != null)
				{
					Debug.Log("Fight!");
					_context.attackState.attackTarget = fight;
					_machine.changeState<CharacterAttackState>();
				}

			}
		}

	}*/
}
