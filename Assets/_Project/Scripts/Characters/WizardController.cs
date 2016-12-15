using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : EnemyController {


	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (other.tag == targetTag)
		{
			Debug.Log ("Attack");
			
			agent.Stop ();
			anim.SetBool ("isAttacking", true);
			//Start attack
		}

	}
}
