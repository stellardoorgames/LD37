using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : CharacterController, IGrabbable {

	public override void Start ()
	{
		base.Start ();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);

		CharacterController enemy = other.GetComponent<CharacterController> ();

		if (enemy != null)
		{
			LevelManager.LoseLevel ();
		}
	}
}
