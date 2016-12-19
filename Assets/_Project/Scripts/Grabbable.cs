using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grabbable : MonoBehaviour {


	public Animator anim;
	protected NavMeshAgent agent;
	protected Rigidbody rb;
	protected Collider collider;

	public bool isGrabbed = false;
	Transform grabber;


	// Use this for initialization
	public virtual void Start () {
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		collider = GetComponent<Collider> ();
		//joint = GetComponentInChildren<FixedJoint> ();
	}
	
	public virtual void Update () 
	{
		if (isGrabbed)
		{
			//transform.localPosition = Vector3.zero;
			//transform.position = grabber.position;
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.05f);
		}
	}


	public void Grabbed(Transform grabber)
	{
		isGrabbed = true;

		this.grabber = grabber;

		Debug.Log ("Grabbed");
		if (agent != null)
			agent.Stop ();
		
		if (rb != null)
			rb.isKinematic = true;

		if (collider != null)
			collider.enabled = false;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);
		
		transform.position = grabber.position;

		//joint.connectedBody = grabber.gameObject.GetComponent<Rigidbody> ();

		transform.SetParent (grabber);

	}

	public void Released()
	{
		isGrabbed = false;

		Debug.Log ("Released");

		if (agent != null)
			agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;

		if (collider != null)
			collider.enabled = true;
		
		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		//joint.connectedBody = null;
		transform.SetParent (null);
	}

}
