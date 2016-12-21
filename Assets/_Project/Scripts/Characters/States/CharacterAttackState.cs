using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterAttackState : SKState<Character> {

	public float attackInterval = 1f;
	public float damage = 1f;

	float lastAttackTime;

	string attackTag;

	Animator anim;
	NavMeshAgent agent;

	public override void begin ()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = _context.anim;
		attackTag = GetComponent<CharacterHuntState>().attackTag;
		//_context.currentTarget = _context.attackTag;

		_context.isAttacking = true;

		anim.SetBool ("isAttacking", true);

		agent.Stop ();

	}

	public override void update (float deltaTime)
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, .5f);

		bool isTargetThere = false;
		foreach (Collider c in colliders)
			if (c.tag == attackTag) //_context.attackTag)
				isTargetThere = true;
		
		if (!isTargetThere)
			_machine.changeStateToPrevious();



		if (Time.time >= lastAttackTime + attackInterval)
		{
			anim.SetTrigger ("Attack");
			
			_context.attackTarget.TakeDamage(damage);
			
			lastAttackTime = Time.time;
		}

	}

	public override void end ()
	{
		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}