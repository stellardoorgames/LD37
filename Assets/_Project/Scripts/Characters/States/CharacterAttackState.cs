using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterAttackState : SKState<Character> {

	float lastAttackTime;

	public override void begin ()
	{
		//_context.currentTarget = _context.attackTag;

		_context.isAttacking = true;

		_context.anim.SetBool ("isAttacking", true);

		_context.agent.Stop ();

	}

	public override void update (float deltaTime)
	{
		Collider[] colliders = Physics.OverlapSphere (_context.transform.position, .5f);

		bool isTargetThere = false;
		foreach (Collider c in colliders)
			if (c.tag == _context.attackTag)
				isTargetThere = true;
		
		if (!isTargetThere)
			_machine.changeStateToPrevious();



		if (Time.time >= lastAttackTime + _context.attackInterval)
		{
			_context.anim.SetTrigger ("Attack");
			
			_context.attackTarget.TakeDamage(_context.damage);
			
			lastAttackTime = Time.time;
		}

	}

	public override void end ()
	{
		_context.agent.Resume ();

		_context.anim.SetBool ("isAttacking", false);
	}
}