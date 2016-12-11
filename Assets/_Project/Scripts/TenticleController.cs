using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

public class TenticleCell
{
	public Obstacle obstacle;
	public CurvySplineSegment segment;
	public CurvySpline spline;

	public TenticleCell(Obstacle obstacle, CurvySplineSegment segment, CurvySpline spline)
	{
		this.obstacle = obstacle;
		this.segment = segment;
		this.spline = spline;

	}
}

public class TenticleController : MonoBehaviour {

	public Transform target;

	public CurvySpline spline;

	public float segmentLength = 2f;

	public int gridWidth = 20;

	public GameObject obstacle;

	public List<TenticleCell> cellStack;

	CurvySplineSegment segment;

	void Start()
	{
		//spline.Add ();

		cellStack = new List<TenticleCell> ();
		foreach (CurvySplineSegment s in spline.Segments)
			cellStack.Add (new TenticleCell (null, s, spline));
	}
	
	// Update is called once per frame
	void Update () {

		segment = spline.LastVisibleControlPoint;

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
				previousSegment = segment;
			
			if (previousSegment != null)
			{
				GameObject go = Instantiate (obstacle);
				go.transform.position = previousSegment.transform.position;

				cellStack.Add (new TenticleCell (go.GetComponent<Obstacle> (), segment, spline));
			}
		}

		if (Input.GetKeyDown (KeyCode.P))
		{
			
			spline = spline.ControlPoints [5].SplitSpline ();
			segment = spline.ControlPoints[spline.ControlPointCount - 1];
			transform.position = segment.transform.position;
		}

	}

	public bool Collide(GameObject go)
	{
		bool retVal = false;
		TenticleCell tc = cellStack [cellStack.Count - 1];
		if (go == tc.obstacle.gameObject)
		{
			Debug.Log ("Collide");
			
			cellStack.RemoveAt (cellStack.Count - 1);
			GameObject.Destroy(tc.obstacle.gameObject);
			tc.segment.Delete ();
			//Obstacle ob = go2.GetComponent<Obstacle> ();
			//ob.Remove ();

			retVal = true;
		}

		tc = cellStack [cellStack.Count - 2];
		if (go == tc.obstacle.gameObject)
		{
			Debug.Log ("Collide");

			cellStack.RemoveAt (cellStack.Count - 2);
			GameObject.Destroy(tc.obstacle.gameObject);
			tc.segment.Delete ();
			//Obstacle ob = go2.GetComponent<Obstacle> ();
			//ob.Remove ();

			retVal = true;
		}

		return retVal;
	}

	/*int GetIndexFromPosition(Vector3 pos)
	{
		return (int)(pos.x * pos.y);
	}*/


}
