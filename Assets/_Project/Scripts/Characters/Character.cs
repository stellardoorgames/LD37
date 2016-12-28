using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using Prime31.StateKit;


public class Character : MonoBehaviour {

	public enum DeathType
	{
		Lava,
		Spikes
	}

	//public List<string> targetTags = new List<string>();
	//public string currentTarget;

	public List<DeathType> vulnerabilities = new List<DeathType>();

	public Text text;

	GameObject target;

	public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;

	public SKStateMachine<Character> stateMachine;

	[HideInInspector]
	public CharacterSpawnState spawnState;
	[HideInInspector]
	public CharacterSearchState searchState;
	[HideInInspector]
	public CharacterAttackState attackState;
	[HideInInspector]
	public CharacterCarryState carryState;
	[HideInInspector]
	public CharacterGrabbedState grabbedState;
	[HideInInspector]
	public CharacterDeathState deathState;

	//List<Vector3> wanderDirections;

	public virtual void Start()
	{
		agent = GetComponent<NavMeshAgent> ();

		spawnState = GetComponent<CharacterSpawnState>();
		searchState = GetComponent<CharacterSearchState>();
		attackState = GetComponent<CharacterAttackState>();
		carryState = GetComponent<CharacterCarryState>();
		grabbedState = GetComponent<CharacterGrabbedState>();
		deathState = GetComponent<CharacterDeathState>();

		stateMachine = new SKStateMachine<Character>(this, spawnState);
		stateMachine.addState(searchState);
		stateMachine.addState(attackState);
		stateMachine.addState(carryState);
		stateMachine.addState(grabbedState);
		stateMachine.addState(deathState);
		/*stateMachine.addState(GetComponent<CharacterHuntState>());
		stateMachine.addState(GetComponent<CharacterGrabbedState>());
		stateMachine.addState(GetComponent<CharacterAttackState>());
		stateMachine.addState(GetComponent<CharacterDeathState>());
		stateMachine.addState(GetComponent<CharacterCarryState>());*/

		/*wanderDirections = new List<Vector3> {
			Vector3.forward,
			Vector3.right,
			Vector3.back,
			Vector3.left
		};*/
	}

	public virtual void Update () 
	{
		stateMachine.update(Time.deltaTime);

	}

	public GameObject FindClosestTarget(GameObject[] targets)
	{
		if (targets == null)
			return null;
		
		GameObject newTarget = null;
		float dist = int.MaxValue;

		foreach(GameObject go in targets)
		{
			float d = Vector3.Distance (transform.position, go.transform.position);
			if (d < dist)
			{
				dist = d;
				newTarget = go;
			}
		}
		
		return newTarget;
	}

	public NavMeshPath GetPathToTarget(string tag, bool wander = true)
	{
		List<string> t = new List<string>()	{tag};
		return GetPathToTarget(t, wander);
	}

	public NavMeshPath GetPathToTarget(List<string> tags, bool wander = true)
	{
		NavMeshPath path = new NavMeshPath();

		foreach(string tt in tags)
		{
			target = FindClosestTarget(GameObject.FindGameObjectsWithTag (tt));
			if (target != null)
			{
				if (agent.CalculatePath(target.transform.position, path))
					break;
			}
		}

		if (!(path.status == NavMeshPathStatus.PathComplete) && wander)
		{
			path = GetWanderPath(3.5f);
		}

		return path;
	}

	NavMeshPath GetWanderPath(float distance = 1f)
	{
		Debug.Log("Auto-wander");

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Light");

		GameObject go = gos[UnityEngine.Random.Range(0, gos.Length - 1)];

		NavMeshPath path = new NavMeshPath();
		agent.CalculatePath(go.transform.position, path);
		return path;


		/*List<Vector3> dirs = wanderDirections;

		dirs.Shuffle<Vector3>();

		NavMeshPath path = new NavMeshPath();

		foreach(Vector3 v in dirs)
		{
			agent.CalculatePath(transform.position + v, path);
			if (path.status == NavMeshPathStatus.PathComplete)
				break;
		}

		return path;*/
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hazard")
		{
			Hazard h = other.GetComponent<Hazard>();
			if (h != null && vulnerabilities.Contains(h.hazardType))
			{
				Death (h.hazardType);
			}
		}

	}

	public virtual void Death(DeathType deathType)
	{
		if (deathState.enabled)
			return;

		deathState.StartDeath(deathType);
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}

}
