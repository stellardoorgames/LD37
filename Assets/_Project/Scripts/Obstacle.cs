using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public float distance;
	Transform target;

	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		target = go.transform;

		Scale ();
	}
	
	// Update is called once per frame
	void Update () 
	{
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
			float dist = Mathf.Clamp01 (Vector3.Distance (target.position, transform.position) * 0.8f);

			transform.localScale = new Vector3(dist, dist, dist);
		}
	}
}
