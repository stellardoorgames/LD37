using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class CharController : MonoBehaviour, IGrabbable {

	public string targetTag;

	public Text text;

	Transform target;

    public GameObject lavaDeathEffect;

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
		if (isGrabbed)
		{
			//transform.localPosition = Vector3.zero;
			//transform.position = grabber.position;
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.1f);
		}

		if (Input.GetKeyDown(KeyCode.K))
			Death();
	}

	public void Retarget()
	{
		if (targetTag == "")
			return;
		
		GameObject[] targets = GameObject.FindGameObjectsWithTag (targetTag);

		if (targets.Length > 0)
		{
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

		return true;
	}

	public virtual void Released()
	{
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
}
