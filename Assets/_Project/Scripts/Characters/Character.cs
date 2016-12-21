using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using Prime31.StateKit;


public class Character : MonoBehaviour, IGrabbable {

	public enum DeathTypes
	{
		Lava,
		Spikes
	}
	public string currentTarget;

	public Text text;

	Transform target;

	//public bool isAttacking = false;

	//public DeathTypes deathType;

	public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;

	//[HideInInspector]
	//public IDamagable attackTarget;

	[SerializeField]
	float _grabRange = 1f;
	public float grabRange {
		get {return _grabRange;}
		set {_grabRange = value;}
	}
	public bool isGrabbed {get; set;}
	public Transform grabber {get; set;}
	public Transform grabTransform {get; set;}
	public event Action OnEscaped;

	public IGrabbable grabbedObject;

	SKStateMachine<Character> stateMachine;

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

		if (currentTarget == null || currentTarget == "")
			return;
		
		GameObject[] targets = null;
		targets = GameObject.FindGameObjectsWithTag (currentTarget);

		float dist = int.MaxValue;

		foreach(GameObject go in targets)
		{
			float d = Vector3.Distance (transform.position, go.transform.position);
			if (d < dist)
				target = go.transform;
		}
		if (agent != null)
			agent.destination = target.position;
	
	}


	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Lava")
		{
			Debug.Log ("Lava");
            Death (DeathTypes.Lava);
		}
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

	public float GetGrabRange(Vector3 grabberPosition)
	{
		return Vector3.Distance(grabberPosition, transform.position);
	}

	public virtual bool Grabbed(Transform grabber)
	{
		if (deathState.enabled)
			return false;
		
		this.grabber = grabber;

		stateMachine.changeState<CharacterGrabbedState>();
		
		return true;
	}

	public virtual void Released()
	{
		if (isGrabbed == false)
			return;
	
		if (!deathState.enabled)
			stateMachine.changeState<CharacterHuntState>();
	}

	public void GrabEscape()
	{
		if (OnEscaped != null)
			OnEscaped();
		
	}

	public void OnGrabRelease()
	{
		if (grabbedObject != null)
		{
			grabbedObject.Released();
			grabbedObject.OnEscaped -= OnGrabRelease;
		}

		grabbedObject = null;

		Retarget();
	}

	public void AttemptToGrab(GameObject go)
	{
		if (deathState.enabled)
			return;
		
		IGrabbable grabbable = go.GetComponent<IGrabbable>();
		if (grabbable != null)
		{
			bool grabWorked = grabbable.Grabbed (transform);
			if (grabWorked)
			{
				grabbedObject = grabbable;
				grabbedObject.OnEscaped += OnGrabRelease;
				stateMachine.changeState<CharacterCarryState>();
			}
		}
	}
}
