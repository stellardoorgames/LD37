using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrincessController : Character {

	public UnityEvent OnKidnapped;
	public UnityEvent OnDeath;

	public CharacterHoldState holdState;

	CharacterGrabbable grabbable;

	public override void Start ()
	{
		base.Start ();

		holdState = GetComponent<CharacterHoldState>();
		stateMachine.addState(holdState);

		grabbable = GetComponent<CharacterGrabbable>();
		grabbable.OnGrab += OnGrabbed;
	}

	void OnGrabbed()
	{
		if (grabbable.grabber != null)
		{
			if (grabbable.grabber.tag == "Enemy")
				LevelManager.IncrementStat(Stats.PrincessNabbed);
		}
	}


	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
		{
			LevelManager.IncrementStat(Stats.PrincessLost);
			OnKidnapped.Invoke();
		}
		/*else if (other.tag == "Switch")
		{
			Grabbable grab = other.GetComponent<Grabbable>();
			if (grab != null)
				carryState.StartCarry(grab);
			
		}*/

		base.OnTriggerEnter(other);
	}

	public override void Death(Character.DeathType deathType)
	{
		LevelManager.IncrementStat(Stats.PrincessDeath);
		OnDeath.Invoke();

		base.Death (deathType);
	}
}
