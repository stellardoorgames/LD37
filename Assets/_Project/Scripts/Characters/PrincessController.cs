using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : Character, IGrabbable {

	public override void Start ()
	{
		base.Start ();
	}


	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Lava")
		{
			Debug.Log ("Lava");
			Death (DeathTypes.Lava);
		}

		if (other.tag == "Exit")
			LevelManager.LoseLevel ();
	}

	public override void Death(Character.DeathTypes deathType)
	{
		LevelManager.LoseLevel ();

		base.Death (deathType);
	}
}
