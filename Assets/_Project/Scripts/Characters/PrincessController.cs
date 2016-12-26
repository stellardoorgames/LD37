using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrincessController : Character {

	public UnityEvent OnKidnapped;
	public UnityEvent OnDeath;

	public override void Start ()
	{
		base.Start ();
	}


	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
		{
			OnKidnapped.Invoke();
			//LevelManager.LoseLevel ();
		}

		base.OnTriggerEnter(other);
	}

	public override void Death(Character.DeathTypes deathType)
	{
		//LevelManager.LoseLevel ();
		OnDeath.Invoke();

		base.Death (deathType);
	}
}
