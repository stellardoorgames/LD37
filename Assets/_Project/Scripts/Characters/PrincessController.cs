using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : Character {

	public override void Start ()
	{
		base.Start ();
	}


	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
			LevelManager.LoseLevel ();

		base.OnTriggerEnter(other);
	}

	public override void Death(Character.DeathTypes deathType)
	{
		LevelManager.LoseLevel ();

		base.Death (deathType);
	}
}
