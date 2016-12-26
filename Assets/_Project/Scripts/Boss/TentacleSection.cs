using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;

public class TentacleSection : MonoBehaviour {

	public float distanceMultiplier = 0.5f;
	public Transform target;

	public CurvySplineSegment segment;
	public TenticleController controller;

	bool stillScaling = true;
	float scale;

	// Use this for initialization
	void Start () 
	{
		Scale ();
/*
		if (segment.SegmentIndex % 3 == 0)
		{
			segment.OrientationAnchor = true;
			segment.Swirl = CurvyOrientationSwirl.Segment;
			StartCoroutine(Swirl());
			
		}*/
	}

	public static TentacleSection Create(GameObject prefab, Transform target, CurvySplineSegment segment, TenticleController controller)
	{
		GameObject go = Instantiate (prefab);
		go.transform.position = segment.PreviousControlPoint.transform.position;//segment.transform.position; //
		TentacleSection ts = go.GetComponent<TentacleSection> ();
		ts.target = target;
		ts.segment = segment;
		ts.controller = controller;
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

	/*IEnumerator Swirl()
	{
		//CurvySplineSegment first = spline.FirstVisibleControlPoint;

		Vector3 v1 = new Vector3(-0.0000f, 0f, 0f);
		Vector3 v2 = new Vector3(0.0005f, 0f, 0f);
		while(true)
		{
			v1 = -v1;
			v2 = -v2;
			float startTime = Time.time;
			float endTime = startTime + 3f;
			while (Time.time < endTime)
			{
				float t = Mathf.InverseLerp(startTime, endTime, Time.time);
				segment.SwirlTurns += Vector3.Slerp(v1, v2, t).x;
				yield return null;
			}

		}
	}*/
	/*public void TakeDamage(float damage)
	{
		controller.TakeDamage();
	}*/
}
