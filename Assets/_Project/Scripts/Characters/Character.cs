﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using Prime31.StateKit;


public class Character : MonoBehaviour {
	
	public Text text;

	GameObject target;

	public Animator anim;

	public bool debug = false;

	NavMeshAgent agent;

	public SKStateMachine<Character> stateMachine;

	public virtual void Start()
	{
		agent = GetComponent<NavMeshAgent> ();

		stateMachine = new SKStateMachine<Character>(this, GetComponent<CharacterSpawnState>());
		stateMachine.addState(GetComponent<CharacterSearchState>());
		stateMachine.addState(GetComponent<CharacterGrabbedState>());
		stateMachine.addState(GetComponent<CharacterAttackState>());
		stateMachine.addState(GetComponent<CharacterDeathState>());
		stateMachine.addState(GetComponent<CharacterCarryState>());
	}

	public virtual void Update () 
	{
		stateMachine.update(Time.deltaTime);

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
				Vector3 point = target.transform.position;// + ((transform.position - target.transform.position).normalized * .25f) ;
				//Instantiate(deathState.soulGemPrefab, point, Quaternion.identity);
				if (agent.CalculatePath(point, path))
					break;
			}

			/*foreach(GameObject go in GameObject.FindGameObjectsWithTag (tt))
			{
				if (agent.CalculatePath(go.transform.position, path) && (path.status == NavMeshPathStatus.PathComplete))
					break;
			}*/
		}

		if (path.status == NavMeshPathStatus.PathInvalid && wander)
		{
			path = GetWanderPath(3.5f);
		}

		return path;
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

	/*public List<GameObject> SortByDistance(GameObject[] targets)
	{
		
	}*/

	NavMeshPath GetWanderPath(float distance = 1f)
	{
		debugMessage("Wandering");
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

	public void debugMessage(string text)
	{
		if (debug && this.text != null)
			this.text.text = text;
	}

	public void SetSpeech(string text, float duration)
	{
		if (!debug && this.text != null)
			StartCoroutine(SetSpeechCoroutine(text, duration));
	}

	IEnumerator SetSpeechCoroutine(string text, float duration)
	{
		this.text.text = text;

		if (duration == 0f)
			yield break;
		else
			yield return new WaitForSeconds(duration);

		this.text.text = "";
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}

}
