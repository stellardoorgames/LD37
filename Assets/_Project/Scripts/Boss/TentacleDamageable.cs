using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamageable : Damageable {

	TentacleSection section;

	protected virtual void Awake()
	{
		section = GetComponent<TentacleSection>();
	}

	public override void TakeDamage (float damageAmount)
	{
		section.controller.TakeDamage();
	}
}
