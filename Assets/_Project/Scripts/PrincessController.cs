using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : Grabbable {

	public override void Start ()
	{
		base.Start ();
	}

	public override void Update ()
	{
		base.Update ();
	}

	void OnTriggerEnter(Collider other)
	{
		EnemyController enemy = other.GetComponent<EnemyController> ();

		if (enemy != null)
		{
			LevelManager.LoseLevel ();
		}
	}
}
