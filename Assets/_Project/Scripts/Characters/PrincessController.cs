using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : CharController, IGrabbable {

	public override void Start ()
	{
		base.Start ();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);

		/*CharController enemy = other.GetComponent<CharController> ();

		if (enemy != null)
		{
		}*/

		if (other.tag == "Exit")
			LevelManager.LoseLevel ();
	}

	public override void Death ()
	{
		LevelManager.LoseLevel ();

		base.Death ();
	}
}
