using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;

public class Obstacle : MonoBehaviour {

	public float distanceMultiplier = 0.5f;
	public Transform target;

	//public float distanceAtCreation;

	bool stillScaling = true;
	float scale;

	// Use this for initialization
	void Start () 
	{
		//GameObject go = GameObject.FindGameObjectWithTag ("Player");
		//target = go.transform;

		Scale ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (stillScaling)
			Scale ();
	}

	public void Remove()
	{
		GameObject.Destroy (gameObject);
	}

	void Scale()
	{
		if (target != null)
		{
			float dist = Mathf.Clamp01 (Vector3.Distance (target.position, transform.position) * distanceMultiplier);

			if (dist > scale)
				transform.localScale = Vector3.one * dist;

			scale = dist;

			if (dist >= 0.9f)
				stillScaling = false;
		}

	}

	void TakeHit(float damage)
	{
		
	}
}
