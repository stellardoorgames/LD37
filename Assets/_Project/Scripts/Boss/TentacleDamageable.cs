using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamageable : Damageable {

	public TenticleController controller;

	protected virtual void Start()
	{
		if (controller == null)
			controller = GetComponent<TentacleSection>().controller;
	}

	public override void TakeDamage (float damageAmount)
	{
		controller.TakeDamage();
	}
}
