using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : Character {
	
	public bool isSuicidal = false;

	public override void Start ()
	{
		base.Start ();

		if (isSuicidal)
			GetComponent<CharacterCarryState>().carryDestination = "Lava";
	}
}
