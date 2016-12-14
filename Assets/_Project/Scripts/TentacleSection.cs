using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;

public class TentacleSection : MonoBehaviour {

	public float distanceMultiplier = 0.5f;
	public Transform target;

	public CurvySplineSegment segment;

	bool stillScaling = true;
	float scale;

	// Use this for initialization
	void Start () 
	{
		Scale ();
	}

	public static TentacleSection Create(GameObject prefab, Transform target, CurvySplineSegment segment)
	{
		GameObject go = Instantiate (prefab);
		go.transform.position = segment.PreviousControlPoint.transform.position;
		TentacleSection ts = go.GetComponent<TentacleSection> ();
		ts.target = target;
		ts.segment = segment;
		return ts;
	}

	public void Remove()
	{
		CurvySpline cs = segment.Spline;
		cs.Delete (segment);
		//Destroy (segment);
		cs.Refresh ();
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (stillScaling)
			Scale ();
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
