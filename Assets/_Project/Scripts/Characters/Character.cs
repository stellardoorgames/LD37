using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using Prime31.StateKit;


public class Character : MonoBehaviour {

	public enum DeathTypes
	{
		Lava,
		Spikes
	}

	public List<string> targetTags = new List<string>();
	public string currentTarget;

	public Text text;

	Transform target;

	public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;

	public SKStateMachine<Character> stateMachine;

	[HideInInspector]
	public CharacterSpawnState spawnState;
	[HideInInspector]
	public CharacterHuntState huntState;
	[HideInInspector]
	public CharacterAttackState attackState;
	[HideInInspector]
	public CharacterCarryState carryState;
	[HideInInspector]
	public CharacterGrabbedState grabbedState;
	[HideInInspector]
	public CharacterDeathState deathState;

	public virtual void Start()
	{
		agent = GetComponent<NavMeshAgent> ();

		spawnState = GetComponent<CharacterSpawnState>();
		huntState = GetComponent<CharacterHuntState>();
		attackState = GetComponent<CharacterAttackState>();
		carryState = GetComponent<CharacterCarryState>();
		grabbedState = GetComponent<CharacterGrabbedState>();
		deathState = GetComponent<CharacterDeathState>();

		stateMachine = new SKStateMachine<Character>(this, spawnState);
		stateMachine.addState(huntState);
		stateMachine.addState(attackState);
		stateMachine.addState(carryState);
		stateMachine.addState(grabbedState);
		stateMachine.addState(deathState);
		/*stateMachine.addState(GetComponent<CharacterHuntState>());
		stateMachine.addState(GetComponent<CharacterGrabbedState>());
		stateMachine.addState(GetComponent<CharacterAttackState>());
		stateMachine.addState(GetComponent<CharacterDeathState>());
		stateMachine.addState(GetComponent<CharacterCarryState>());*/

	}

	public virtual void Update () 
	{
		stateMachine.update(Time.deltaTime);

		//if (Input.GetKeyDown(KeyCode.K))
		//	Death();
	}

	public void Retarget(string newTarget = null)
	{
		if (newTarget != null)
			currentTarget = newTarget;

		if (agent == null || !agent.isOnNavMesh)
			return;

		GameObject[] targets = null;


		if (currentTarget != null && currentTarget != "")
			targets = GameObject.FindGameObjectsWithTag (currentTarget);


		if (targets == null || targets.Length == 0)
		{
			foreach(string tt in targetTags)
			{
				targets = GameObject.FindGameObjectsWithTag (tt);
				if (targets.Length > 0)
				{
					currentTarget = tt;
					break;
				}
			}

			if (targets == null || targets.Length == 0)
			{
				Wander();
				return;
			}
		}

		float dist = int.MaxValue;

		foreach(GameObject go in targets)
		{
			float d = Vector3.Distance (transform.position, go.transform.position);
			if (d < dist)
			{
				dist = d;
				target = go.transform;
			}
		}
		if (target != null)
			agent.destination = target.position;

		if (!agent.hasPath)
		{
			Debug.Log("No Path, fix this");
		}
	}

	public virtual void Wander()
	{
		float distance = 1f;
		List<Vector3> wanderDirections = new List<Vector3> {
			new Vector3(distance,0f,0f),
			new Vector3(-distance,0f,0f),
			new Vector3(0f,0f,distance),
			new Vector3(0f,0f,-distance),
		};
		UnityEngine.Random.InitState((int)(Input.mousePosition.x + Input.mousePosition.y));
		int dir = UnityEngine.Random.Range(0, 3);

		agent.destination = wanderDirections[dir];

	}


	public virtual void Death(DeathTypes deathType)
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
