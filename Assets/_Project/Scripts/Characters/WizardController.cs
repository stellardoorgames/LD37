using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : CharController {

	public float attackInterval = 1;

	bool isAttacking;

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (other.tag == targetTag)
		{
			Debug.Log ("Attack");
			
			IDamagable d = other.GetComponent<IDamagable>();
			if (d != null)
				StartCoroutine (AttackCoroutine (d));
		}

	}

	public override void Update ()
	{
		base.Update();

		Collider[] colliders = Physics.OverlapSphere (transform.position, .5f);
		bool isTentacle = false;
		foreach (Collider c in colliders)
			if (c.tag == "Tentacle")
				isTentacle = true;
		isAttacking = isTentacle;
	}

	IEnumerator AttackCoroutine(IDamagable attackTarget)
	{
		isAttacking = true;

		anim.SetBool ("isAttacking", true);

		agent.Stop ();

		while (isAttacking)
		{
			anim.SetTrigger ("Attack");
			
			float nextAttackTime = Time.time + attackInterval;

			attackTarget.TakeDamage(damage);

			while (Time.time < nextAttackTime)
				yield return null;
			
		}

		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}
