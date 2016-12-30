using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;
using UnityEngine.Events;

public class CharacterDeathState : SKState<Character> {

	public enum DeathType
	{
		Lava,
		Spikes
	}

	public List<DeathType> vulnerabilities = new List<DeathType>();

	public float deathTime = 2f;

	public GameObject soulGemPrefab;

	public UnityEvent OnDeath;

	DeathType deathType;

	float startTime;

	Animator anim;
	NavMeshAgent agent;

	Grabbable grabbable;

	public override void onInitialized ()
	{
		anim = _context.anim;
		agent = GetComponent<NavMeshAgent>();
		grabbable = GetComponent<Grabbable>();
	}

	public void StartDeath(DeathType deathType)
	{
		if (enabled)
			return;
		
		this.deathType = deathType;
		_machine.changeState<CharacterDeathState>();
	}

	public override void begin ()
	{
		_context.debugMessage("Dying");
		Debug.Log (string.Format("{0} died from {1}.", name, deathType));

		tag = "Untagged";

		grabbable.EscapedEvent();

		agent.Stop();

		LevelManager.IncrementKills(tag);
		
		anim.SetTrigger("Death");

		OnDeath.Invoke();

		startTime = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + deathTime)
		{
			if (soulGemPrefab != null)
			{
				GameObject go = GameObject.Instantiate(soulGemPrefab);
				go.transform.position = transform.position;
			}
			_context.Destroy();
		}
	}

	public override void end ()
	{
		_context.debugMessage("");
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (enabled)
			return;
		
		if (other.tag == "Hazard")
		{
			Hazard h = other.GetComponent<Hazard>();
			if (h != null && vulnerabilities.Contains(h.hazardType))
			{
				StartDeath (h.hazardType);
			}
		}

	}

}