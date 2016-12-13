using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

public class TenticleCell
{
	public Obstacle obstacle;
	public CurvySplineSegment segment;

	public TenticleCell(Obstacle obstacle, CurvySplineSegment segment)
	{
		this.obstacle = obstacle;
		this.segment = segment;
	}
}

public class TenticleController : MonoBehaviour {

	public Transform target;

	public CurvySpline spline;

	public float segmentLength = 1f;

	public GameObject obstacle;

	public Material tenticleMaterial;

	List<TenticleCell> cellStack;

	CurvySplineSegment segment;

	float length;

	void Start()
	{
		//spline.Add ();

		cellStack = new List<TenticleCell> ();
		foreach (CurvySplineSegment s in spline.Segments)
			cellStack.Add (new TenticleCell (null, s));
	}
	
	// Update is called once per frame
	void Update () {

		spline.Refresh ();

		segment = spline.LastVisibleControlPoint;//spline.ControlPoints [spline.Count - 1];//

		float offset = spline.Length - length;//Vector3.Distance (segment.transform.position, target.position);
		length = spline.Length;

		Vector3 v = tenticleMaterial.mainTextureOffset;
		v.x -= offset * 0.25f;

		tenticleMaterial.mainTextureOffset = v;

		segment.transform.position = target.position;

		//Debug.Log (segment.PreviousControlPoint.Length);

		if (segment.PreviousControlPoint.Length > segmentLength)
		{
			 
			segment = spline.InsertBefore (segment);
			//segment = spline.Add ();
			//segment.transform.position = target.position;
			//spline.Refresh ();

			CurvySplineSegment previousSegment = null;
			if (segment.PreviousControlPoint != null)
				previousSegment = segment.PreviousControlPoint;
			
			if (previousSegment != null)
			{
				GameObject go = Instantiate (obstacle);
				go.transform.position = previousSegment.transform.position;

				Obstacle ob = go.GetComponent<Obstacle> ();
				ob.target = target;

				cellStack.Add (new TenticleCell (ob, segment));
			}
		}

		if (Input.GetKeyDown (KeyCode.P))
		{
			
			spline = spline.ControlPoints [5].SplitSpline ();
			segment = spline.ControlPoints[spline.ControlPointCount - 1];
			transform.position = segment.transform.position;
		}

		spline.Refresh ();

	}

	public bool Collide(GameObject go)
	{
		bool retVal = false;

		for (int i = 1; i < 8; i++)
		{
			if (i >= 0 && i < cellStack.Count - 1)
			{
				TenticleCell tc = cellStack [cellStack.Count - i];
				if (go == tc.obstacle.gameObject)
				{
					Debug.Log ("Retracting");

					RemoveSegment (cellStack.Count - i);
					
					retVal = true;
				}
				
			}

		}

		return retVal;
	}

	void RemoveSegment(int index)
	{
		for (int i = cellStack.Count - 1; i >= index; i--)
		{
			TenticleCell tc = cellStack [i];
			
			GameObject.Destroy(tc.obstacle.gameObject);
			tc.segment.Delete ();
			cellStack.Remove (tc);//.RemoveAt (cellStack.Count - i);
			
		}
	}
}
