using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamageable : Damageable {

	TenticleController controller;

	protected virtual void Start()
	{
		if (controller == null)
		{
			TentacleSection ts = GetComponent<TentacleSection>();
			if (ts != null)
				controller = ts.controller;
		}

		if (controller == null)
			controller = GetComponent<TenticleController>();
	}

	public override void TakeDamage (float damageAmount)
	{
		controller.TakeDamage(damageAmount);
	}
}
