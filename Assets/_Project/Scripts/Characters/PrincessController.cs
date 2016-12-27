using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrincessController : Character {

	public UnityEvent OnKidnapped;
	public UnityEvent OnDeath;

	public CharacterHoldState holdState;

	public override void Start ()
	{
		base.Start ();

		holdState = GetComponent<CharacterHoldState>();
		stateMachine.addState(holdState);
	}


	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
		{
			OnKidnapped.Invoke();
			//LevelManager.LoseLevel ();
		}
		else if (other.tag == "Switch")
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				carryState.StartCarry(grab);
			
		}

		base.OnTriggerEnter(other);
	}

	public override void Death(Character.DeathType deathType)
	{
		//LevelManager.LoseLevel ();
		OnDeath.Invoke();

		base.Death (deathType);
	}
}
