using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : CharController {


	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (isGrabbed)
			return;
		
		if (other.tag == currentTarget)
		{
			//Debug.Log ("Attack");
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

		if (isGrabbed)
			isAttacking = false;
	}

}
