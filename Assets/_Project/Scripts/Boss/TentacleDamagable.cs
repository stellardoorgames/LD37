using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamagable : Damagable {

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
