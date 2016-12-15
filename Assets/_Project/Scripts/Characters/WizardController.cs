using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : EnemyController {

	public float attackInterval = 1;

	bool isAttacking;

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (other.tag == targetTag)
		{
			Debug.Log ("Attack");
			


			StartCoroutine (AttackCoroutine ());
		}

	}

	public override void Update ()
	{
		base.Update ();

		Collider[] colliders = Physics.OverlapSphere (transform.position, 1f);
		bool isTentacle = false;
		foreach (Collider c in colliders)
			if (c.tag == "Tentacle")
				isTentacle = true;
		isAttacking = isTentacle;
	}

	IEnumerator AttackCoroutine()
	{
		isAttacking = true;

		anim.SetBool ("isAttacking", true);

		agent.Stop ();

		while (isAttacking)
		{
			anim.SetTrigger ("Attack");
			
			float nextAttackTime = Time.time + attackInterval;
			
			while (Time.time < nextAttackTime)
				yield return null;
			
		}

		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}
