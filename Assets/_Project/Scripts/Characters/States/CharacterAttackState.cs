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

	[HideInInspector]
	public TentacleSection attackTarget;

	Animator anim;
	NavMeshAgent agent;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = _context.anim;
		//attackTag = GetComponent<CharacterHuntState>().attackTag;
	}

	public override void begin ()
	{
		
		anim.SetBool ("isAttacking", true);
		agent.Stop ();

	}

	public override void update (float deltaTime)
	{
		
		if (Time.time >= lastAttackTime + attackInterval)
		{
			if (attackTarget == null || Vector3.Distance(transform.position, attackTarget.transform.position) > attackRange)
			{
				_machine.changeState<CharacterHuntState>();
				return;
			}

			anim.SetTrigger ("Attack");
			
			attackTarget.TakeDamage(damage);
			
			lastAttackTime = Time.time;
		}

	}

	public override void end ()
	{
		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}