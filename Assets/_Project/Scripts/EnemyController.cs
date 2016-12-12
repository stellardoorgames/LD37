using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public string targetTag;

	public AnimationClip deathAnim;

	public Text text;

	Transform target;

	NavMeshAgent agent;
	Rigidbody rb;

	bool isGrabbed = false;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();

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

			agent.destination = target.position;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		//agent.speed = 2f;

		if (isGrabbed)
		{
			transform.localPosition = Vector3.zero;
		}

	}


	public void Grabbed(Transform sucker)
	{
		Debug.Log ("Grabbed");
		if (Vector3.Distance(transform.position, sucker.position) < 1)
		{
			agent.Stop ();
			if (rb != null)
				rb.isKinematic = true;
			transform.position = sucker.position;
			transform.SetParent (sucker);
			
		}
	}

	public void Released()
	{
		agent.Resume ();

		if (rb != null)
			rb.isKinematic = false;
		
		transform.SetParent (null);
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Lava")
		{
			//Animator anim = GetComponent<Animator> ();
			//anim.Play ("Death");
			Debug.Log ("Lava");
			//Destroy (gameObject);
		}
	}

	public void Death()
	{
		Destroy (gameObject);

	}

}
