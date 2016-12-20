using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public enum CharacterStates
{
	Spawning,
	Hunting,
	Attacking,
	Carrying,
	Grabbed,
	Dying
}

public class CharController : MonoBehaviour, IGrabbable {

	public List<string> targetTags;
	[SerializeField]
	string _currentTarget;
	public string currentTarget{
		get {return _currentTarget;}
		set {Retarget(value);}
	}
	//public string alternateTargetTag;

	public Text text;

	Transform target;

    public GameObject lavaDeathEffect;

	public bool isAttacking = false;

	public float attackInterval = 1f;

	public float damage = 1f;

	public Animator anim;
	protected NavMeshAgent agent;
	protected Rigidbody rb;
	protected Collider charCollider;

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

	public virtual void Start()
	{
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		charCollider = GetComponent<Collider> ();

		//grabTransform = transform;

		Retarget ();
	}

	public virtual void Update () 
	{
	
		if (Input.GetKeyDown(KeyCode.K))
			Death();
	}

	public void Retarget(string overrideTargetTag = null)
	{
		GameObject[] targets = null;

		if (overrideTargetTag != null)
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
					_currentTarget = tag;
					break;
				}
			}

		}

		if (targets == null || targets.Length == 0)
			return;

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
	}

	public virtual void Death()
	{
		if (OnEscaped != null)
			OnEscaped();
		
		LevelManager.AddKill ();
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

		StartCoroutine(GrabbedCoroutine());

		return true;
	}

	IEnumerator GrabbedCoroutine()
	{
		while(isGrabbed)
		{
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.1f);
			yield return null;
		}
	}

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
	}


	protected IEnumerator AttackCoroutine(IDamagable attackTarget)
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
	}

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
			}
		}
	}
}
