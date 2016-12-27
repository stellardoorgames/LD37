using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;
using UnityEngine.AI;

public class CharacterWanderState : SKState<Character> {

	NavMeshAgent agent;

	public float distance = 1f;
	public float wanderDuration = 3f;
	float startTime;

	List<Vector3> wanderDirections;

	public override void onInitialized ()
	{
		agent = GetComponent<NavMeshAgent>();

		wanderDirections = new List<Vector3> {
			new Vector3(distance,0f,0f),
			new Vector3(-distance,0f,0f),
			new Vector3(0f,0f,distance),
			new Vector3(0f,0f,-distance),
		};
	}

	public override void begin ()
	{
		startTime = Time.time;
		agent.destination = transform.position + GetRandomDirection();

		Debug.Log(gameObject + "is wandering");
	}

	public override void update (float deltaTime)
	{
		if (Time.time > startTime + wanderDuration)
		{
			_machine.changeState<CharacterSearchState>();
			//agent.destination = transform.position + GetRandomDirection();
		}

	}

	public override void end ()
	{

	}

	Vector3 GetRandomDirection ()
	{
		
		return wanderDirections[Random.Range(0, 3)];
	}
}
