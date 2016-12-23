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

	/*[SerializeField]
	float _grabRange = 1f;
	public float grabRange {
		get {return _grabRange;}
		set {_grabRange = value;}
	}
	public bool isGrabbed {get; set;}
	public Transform grabber {get; set;}
	public Transform grabTransform {get; set;}
	public event Action OnEscaped;*/

	public Grabbable carriedObject;

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

		deathState.deathType = deathType;
		stateMachine.changeState<CharacterDeathState>();

	}
	public void Destroy()
	{
		Destroy (gameObject);
	}

	/*public float GetGrabRange(Vector3 grabberPosition)
	{
		return Vector3.Distance(grabberPosition, transform.position);
	}*/

	/*public virtual bool Grabbed(Transform grabber)
	{
		if (deathState.enabled)
			return false;
		
		this.grabber = grabber;

		stateMachine.changeState<CharacterGrabbedState>();
		
		return true;
	}*/

	/*public virtual void Released()
	{
		if (isGrabbed == false)
			return;

		if (!deathState.enabled)
			stateMachine.changeState<CharacterHuntState>();
	}*/

	/*public void GrabEscape()
	{
		if (OnEscaped != null)
			OnEscaped();
		
	}*/

	public void OnCarryRelease()
	{
		if (carriedObject != null)
		{
			carriedObject.Released();
			carriedObject.OnEscaped -= OnCarryRelease;
		}

		carriedObject = null;

		currentTarget = "";

		//stateMachine.changeStateToPrevious();
		//stateMachine.changeState<CharacterHuntState>();
		//Retarget();
	}

	public void AttemptToCarry(GameObject go)
	{
		if (deathState.enabled)
			return;
		
		Grabbable grabbable = go.GetComponent<Grabbable>();
		if (grabbable != null)
		{
			bool grabWorked = grabbable.Grabbed (transform);
			if (grabWorked)
			{
				carriedObject = grabbable;
				carriedObject.OnEscaped += OnCarryRelease;
				stateMachine.changeState<CharacterCarryState>();
			}
		}
	}
}
