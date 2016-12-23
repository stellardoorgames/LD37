using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHuntState : SKState<Character> {
	
	public float retargetInterval = 3f;
	float lastRetarget;
	//bool isHunting = false;

	public override void begin ()
	{
		//isHunting = true;
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
		//isHunting = false;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		//Debug.Log(enabled);
		if (!enabled)
			return;

		if (_context.targetTags.Contains(other.tag))
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				AttemptToCarry(other.gameObject); //TODO: check if this can be Grabbable
			else
			{
				Damagable fight = other.GetComponent<Damagable>();
				if (fight != null)
					_context.attackState.StartAttack(fight);
				/*{
					Debug.Log("Fight!");
					_context.attackState.attackTarget = fight;
					_machine.changeState<CharacterAttackState>();
				}*/

			}
		}

	}

	public void AttemptToCarry(GameObject go)
	{
		Grabbable grabbable = go.GetComponent<Grabbable>();
		if (grabbable != null)
		{
			bool grabWorked = grabbable.Grabbed (transform);
			if (grabWorked)
			{
				_context.carryState.StartCarry(grabbable);
				//carriedObject = grabbable;

				//_machine.changeState<CharacterCarryState>();
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
