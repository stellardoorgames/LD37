using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grabbable : MonoBehaviour {

	bool isGrabbed = false;

	protected NavMeshAgent agent;
	protected Rigidbody rb;
	public Animator anim;


	// Use this for initialization
	public virtual void Start () {
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (isGrabbed)
		{
			transform.localPosition = Vector3.zero;
		}
	}


	public void Grabbed(Transform grabber)
	{
		Debug.Log ("Grabbed");
		//if (Vector3.Distance(transform.position, sucker.position) < 1)
		//{
		if (agent != null)
			agent.Stop ();
		
		if (rb != null)
			rb.isKinematic = true;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);
		
		transform.position = grabber.position;
		transform.SetParent (grabber);

		//}
	}

	public void Released()
	{
		if (agent != null)
			agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;
		
		if (anim != null)
			anim.SetBool ("isGrabbed", false);
		
		transform.SetParent (null);
	}

}
