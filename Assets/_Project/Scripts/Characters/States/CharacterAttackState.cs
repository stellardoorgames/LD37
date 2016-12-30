using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterAttackState : SKState<Character> {

	public float attackInterval = 1f;
	public float damage = 1f;
	public float attackRange = 1f;

	float lastAttackTime;

	Damageable attackTarget;

	Animator anim;
	NavMeshAgent agent;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = _context.anim;
	}

	public void StartAttack(Damageable target)
	{
		attackTarget = target;
		_machine.changeState<CharacterAttackState>();
	}

	public override void begin ()
	{
		_context.debugMessage("Attacking");

		Debug.Log(string.Format("{0} is attacking {1}.", name, attackTarget));
		anim.SetBool ("isAttacking", true);
		agent.Stop ();

		lastAttackTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		
		if (Time.time >= lastAttackTime + attackInterval)
		{
			if (attackTarget == null || Vector3.Distance(transform.position, attackTarget.transform.position) > attackRange)
			{
				_machine.changeState<CharacterSearchState>();
				return;
			}

			anim.SetTrigger ("Attack");
			
			attackTarget.TakeDamage(damage);
			
			lastAttackTime = Time.time;
		}

	}

	public override void end ()
	{
		_context.debugMessage("");

		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}