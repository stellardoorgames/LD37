using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrincessController : Character {

	public UnityEvent OnKidnapped;

	CharacterGrabbable grabbable;

	public override void Start ()
	{
		base.Start ();

		grabbable = GetComponent<CharacterGrabbable>();
		grabbable.OnGrab += OnGrabbed;
		grabbable.OnRelease += OnReleased;
	}

	void OnGrabbed()
	{
		if (grabbable.grabber != null)
		{
			if (grabbable.grabber.tag == "Enemy")
			{
				SetSpeech("Help!!", 0f);
				LevelManager.IncrementStat(Stats.PrincessNabbed);
			}
		}
	}

	void OnReleased()
	{
		SetSpeech("", 0f);
	}


	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
		{
			LevelManager.IncrementStat(Stats.PrincessLost);
			OnKidnapped.Invoke();
		}

		//base.OnTriggerEnter(other);
	}

	/*public override void Death(Character.DeathType deathType)
	{
		LevelManager.IncrementStat(Stats.PrincessDeath);
		OnDeath.Invoke();

		base.Death (deathType);
	}*/
}
