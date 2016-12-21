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

	//public string stealTag;
	//public string attackTag;
	//public string escapeTag;

	//public List<string> targetTags;
	//[SerializeField]
	//string _currentTarget;
	public string currentTarget;
	/*{
		get {return _currentTarget;}
		set {Retarget(value);}
	}*/
	//public string alternateTargetTag;

	public Text text;

	Transform target;

    //public GameObject lavaDeathEffect;

	public bool isAttacking = false;

	//public float attackInterval = 1f;

	//public float damage = 1f;

	//public GameObject soulGemPrefab;

	public bool isDying = false;
	public DeathTypes deathType;

	public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;
	/*[HideInInspector]
	public Rigidbody rb;
	[HideInInspector]
	public Collider charCollider;*/

	[HideInInspector]
	public IDamagable attackTarget;

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
		//rb = GetComponent<Rigidbody> ();
		//charCollider = GetComponent<Collider> ();

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
		/*stateMachine.addState(new CharacterHuntState());
		stateMachine.addState(new CharacterGrabbedState());
		stateMachine.addState(new CharacterAttackState());
		stateMachine.addState(new CharacterDeathState());
		stateMachine.addState(new CharacterCarryState());*/

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
		if (isDying)
			return;
		
		if (other.tag == "Lava")
		{
			Debug.Log ("Lava");
            Death (DeathTypes.Lava);
		}

		/*if (!isGrabbed)
		{
			if (other.tag == stealTag && other.tag == currentTarget)
			{
				AttemptToGrab(other.gameObject);
			}
			if (other.tag == attackTag && other.tag == currentTarget)
			{
				IDamagable d = other.GetComponent<IDamagable>();
				if (d != null)
					Attack(d);
			}
		}*/
	}

	public virtual void Death(DeathTypes deathType)
	{
		if (isDying)
			return;
		
		//if (OnEscaped != null)
		//	OnEscaped();

		//if (deathType == DeathTypes.Lava)
		//	Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);

		this.deathType = deathType;

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
		/*if (grabbedObject != null)
			OnGrabRelease();
		
		if (OnEscaped != null)
			OnEscaped();*/


		this.grabber = grabber;
		//isGrabbed = true;

		/*Debug.Log ("Grabbed");
		if (agent != null)
			agent.Stop ();

		if (rb != null)
			rb.isKinematic = true;

		if (charCollider != null)
			charCollider.enabled = false;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);

		transform.position = grabber.position;

		transform.SetParent (grabber);

		//StartCoroutine(GrabbedCoroutine());
			stateMachine.changeState<CharacterGrabbedState>();
*/
		if (!isDying)
			stateMachine.changeState<CharacterGrabbedState>();
		
		return true;
	}

	public virtual void Released()
	{
		if (isGrabbed == false)
			return;
		//isGrabbed = false;

		/*Debug.Log ("Released");

		if (agent != null)
			agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;

		if (charCollider != null)
			charCollider.enabled = true;

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);*/

		if (!isDying)
			stateMachine.changeState<CharacterHuntState>();
	}

	public void GrabEscape()
	{
		if (OnEscaped != null)
			OnEscaped();
		
	}

	/*public void Attack(IDamagable target)
	{
		attackTarget = target;
		if (!isDying)
			stateMachine.changeState<CharacterAttackState>();
	}*/

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
		if (isDying)
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
