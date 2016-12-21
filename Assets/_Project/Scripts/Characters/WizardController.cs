using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : Character {


	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Lava")
		{
			Debug.Log ("Lava");
			Death (DeathTypes.Lava);
		}
	}

}
