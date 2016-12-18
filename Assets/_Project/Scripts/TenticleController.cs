using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

public class TenticleController : MonoBehaviour {

	public Transform target;

	public CurvySpline spline;

	public float segmentLength = 1f;

	public GameObject obstacle;

	public Material tentacleMaterial;
	//public MeshRenderer materialObject;

	public int selfCollidenumber = 8;

	[HideInInspector]
	public List<TentacleSection> tentacleSectionList = new List<TentacleSection>();

	CurvySplineSegment segment;

	float length;

	void Start()
	{
		foreach (CurvySplineSegment s in spline.Segments)
		{
			tentacleSectionList.Add (TentacleSection.Create (obstacle, null, s));
		}


	}
	
	// Update is called once per frame
	void Update () {

		spline.Refresh ();

		segment = spline.LastVisibleControlPoint;//spline.ControlPoints [spline.Count - 1];//

		float offset = spline.Length - length;//Vector3.Distance (segment.transform.position, target.position);
		length = spline.Length;

		//Material mat = materialObject.material;

		Vector2 v = tentacleMaterial.mainTextureOffset;
		v.x -= offset * 0.25f;

		tentacleMaterial.mainTextureOffset = v;

		//TODO: figure out how to get to the material on the mesh that Curvy generates at runtime
		/*mat.SetTextureOffset("_MainTex", v);
		mat.SetTextureOffset("_BumpMap", v);
		mat.SetTextureOffset("_EmissionMap", v);*/

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
				tentacleSectionList.Add (TentacleSection.Create (obstacle, target, segment));
			
		}

		if (Input.GetKeyDown (KeyCode.P))
		{
			
			spline = spline.ControlPoints [5].SplitSpline ();
			segment = spline.ControlPoints[spline.ControlPointCount - 1];
			transform.position = segment.transform.position;
		}

		spline.Refresh ();

	}

	public bool SelfCollide(GameObject go)
	{
		bool retVal = false;

		int maxSection = tentacleSectionList.Count - 1;
		Debug.Log ("RetractTest");
		for (int i = 1; i < selfCollidenumber; i++)
		{
			if (i >= 0 && i < tentacleSectionList.Count - 1)
			{
				TentacleSection ts = tentacleSectionList [tentacleSectionList.Count - 1];
				if (go == ts.gameObject)
				{
					RemoveSegment (tentacleSectionList.Count - 1);
				}
				retVal = true;
			}
		}

		return retVal;
	}

	void RemoveSegment(int index)
	{
		for (int i = tentacleSectionList.Count - 1; i >= index; i--)
		{
			tentacleSectionList [i].Remove ();
			tentacleSectionList.RemoveAt (i);
		}

	}

	void OnDestroy()
	{
		Debug.Log("Resetting Texture UVs");
		tentacleMaterial.mainTextureOffset = Vector2.zero;
	}
}
