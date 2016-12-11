using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

public class TenticleCell
{
	public int index;
	CurvySplineSegment segment;
	CurvySpline spline;

	public TenticleCell(int index, CurvySplineSegment segment, CurvySpline spline)
	{
		this.index = index;
		this.segment = segment;
		this.spline = spline;

	}
}

public class TenticleController : MonoBehaviour {

	public Transform target;

	public CurvySpline spline;

	public float segmentLength = 2f;

	public int gridWidth = 20;

	public Stack<TenticleCell> cellStack;

	void Start()
	{
		spline.Add ();

		cellStack = new Stack<TenticleCell> ();
		foreach (CurvySplineSegment s in spline.Segments)
			cellStack.Push (new TenticleCell (1, s, spline));
	}
	
	// Update is called once per frame
	void Update () {

		CurvySplineSegment segment = spline.LastVisibleControlPoint;
		//CurvySplineSegment lastSegment = spline.Segments[spline.Count];

		segment.transform.position = target.position;


		//if ()

		//Debug.Log (segment.PreviousControlPoint.Length);

		if (segment.PreviousControlPoint.Length > segmentLength)
		{
			 
			segment = spline.Add ();
			segment.transform.position = target.position;
		}

		/*if (Input.GetKeyDown (KeyCode.P))
		{
			
			spline.ControlPoints [5].SplitSpline ();
			//segment = spline.ControlPoints[spline.ControlPointCount - 1];
			//transform.position = segment.transform.position;
		}*/

	}

	int GetIndexFromPosition(Vector3 pos)
	{
		return ((int)pos.x);
	}
}
