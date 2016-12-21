using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using Prime31.StateKit;

/*public enum CharacterStates
{
	Spawning,
	Hunting,
	Attacking,
	Carrying,
	Grabbed,
	Dying
}*/

public class Character : MonoBehaviour, IGrabbable {

	public string stealTag;
	public string attackTag;
	public string escapeTag;

	public List<string> targetTags;
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

    public GameObject lavaDeathEffect;

	public bool isAttacking = false;

	public float attackInterval = 1f;

	public float damage = 1f;

	public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;
	[HideInInspector]
	public Rigidbody rb;
	[HideInInspector]
	public Collider charCollider;

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

	protected IGrabbable grabbedObject;

	SKStateMachine<Character> stateMachine;
	//SKState<Character> spawning;
	//SKState<Character> hunting;

	public virtual void Start()
	{
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		charCollider = GetComponent<Collider> ();

		//grabTransform = transform;

		//spawning = new SKState<Character>();
		//hunting = new SKState<Character>();

		stateMachine = new SKStateMachine<Character>(this, new CharacterSpawnState());
		stateMachine.addState(new CharacterHuntState());
		stateMachine.addState(new CharacterGrabbedState());
		stateMachine.addState(new CharacterAttackState());
		stateMachine.addState(new CharacterDeathState());
		stateMachine.addState(new CharacterCarryState());

		//Retarget ();
	}

	public virtual void Update () 
	{
		stateMachine.update(Time.deltaTime);

		//if (Input.GetKeyDown(KeyCode.K))
		//	Death();
	}

	public void Retarget(string newTarget = null)
	{

		/*if (overrideTargetTag != null)
		{
			targets = GameObject.FindGameObjectsWithTag (overrideTargetTag);
			_currentTarget = overrideTargetTag;
		}
		else
		{
			foreach(string tag in targetTags)
			{
				targets = GameObject.FindGameObjectsWithTag (tag);
				if (targets.Length > 0)
				{
					currentTarget = tag;
					break;
				}
			}

		}

		if (targets == null || targets.Length == 0)
			return;
		*/

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
			//Animator anim = GetComponent<Animator> ();
			//anim.Play ("Death");
			Debug.Log ("Lava");
            Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);

            Death ();
		}

		if (!isGrabbed)
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
		}
	}

	public virtual void Death()
	{
		if (OnEscaped != null)
			OnEscaped();
		stateMachine.changeState<CharacterDeathState>();
		
		//LevelManager.AddKill ();
		//Destroy (gameObject);

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
		if (grabbedObject != null)
			OnGrabRelease();
		
		if (OnEscaped != null)
			OnEscaped();

		isGrabbed = true;

		this.grabber = grabber;

		Debug.Log ("Grabbed");
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

		return true;
	}

	/*IEnumerator GrabbedCoroutine()
	{
		while(isGrabbed)
		{
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.1f);
			yield return null;
		}
	}*/

	public virtual void Released()
	{
		if (isGrabbed == false)
			return;
		isGrabbed = false;

		Debug.Log ("Released");

		if (agent != null)
			agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;

		if (charCollider != null)
			charCollider.enabled = true;

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);

		stateMachine.changeState<CharacterHuntState>();
	}

	public void Attack(IDamagable target)
	{
		attackTarget = target;
		stateMachine.changeState<CharacterAttackState>();
	}

	/*protected IEnumerator AttackCoroutine(IDamagable attackTarget)
	{
		isAttacking = true;

		anim.SetBool ("isAttacking", true);

		agent.Stop ();

		while (isAttacking)
		{
			anim.SetTrigger ("Attack");

			float nextAttackTime = Time.time + attackInterval;

			attackTarget.TakeDamage(damage);

			while (Time.time < nextAttackTime)
				yield return null;

		}

		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}*/

	protected void OnGrabRelease()
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
