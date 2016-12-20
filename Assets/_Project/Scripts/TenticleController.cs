using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

public class TenticleController : MonoBehaviour {

	public TenticleLead lead;
	public PlayerController playerController;
	//public Transform target;

	public CurvySpline spline;

	public float segmentLength = 1f;

	public GameObject obstacle;

	public Material tentacleMaterial;
	//public MeshRenderer materialObject;

	public int selfCollidenumber = 8;

	public Color damageTint;
	Color startingTint;
	public float damageFlashDuration = 2;
	public int damageFlashNumber = 3;
	bool isFlashing = false;

	[HideInInspector]
	public float tentacleLength;

	[HideInInspector]
	public List<TentacleSection> tentacleSectionList = new List<TentacleSection>();

	CurvySplineSegment segment;

	float length;

	void Start()
	{
		foreach (CurvySplineSegment s in spline.Segments)
		{
			tentacleSectionList.Add (TentacleSection.Create (obstacle, null, s, this));
		}

		startingTint = tentacleMaterial.color;
	}
	
	// Update is called once per frame
	void Update () {

		//spline.Refresh ();

		if (playerController.totalTentacleLength > playerController.currentMaxTotalTentacleLength)
		{
			float dist1 = Vector3.Distance(lead.transform.position, segment.PreviousControlPoint.transform.position);
			Vector3 movement = lead.GetMovement() + lead.transform.position;
			float dist2 = Vector3.Distance(movement, segment.PreviousControlPoint.transform.position);
			if (dist1 > dist2)
				lead.UpdatePosition();

			Debug.Log("Exceeded Length");
		}
		else
			lead.UpdatePosition();

		segment = spline.LastVisibleControlPoint;//spline.ControlPoints [spline.Count - 1];//

		float offset = spline.Length - length;//Vector3.Distance (segment.transform.position, target.position);
		length = spline.Length;

		Vector2 v = tentacleMaterial.mainTextureOffset;
		v.x -= offset * 0.25f;

		tentacleMaterial.mainTextureOffset = v;

		//TODO: figure out how to get to the material on the mesh that Curvy generates at runtime
		//Material mat = materialObject.material;
		/*mat.SetTextureOffset("_MainTex", v);
		mat.SetTextureOffset("_BumpMap", v);
		mat.SetTextureOffset("_EmissionMap", v);*/

		segment.transform.position = lead.transform.position;//target.position;

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
				tentacleSectionList.Add (TentacleSection.Create (obstacle, lead.transform, segment, this));
			
		}

		if (Input.GetKeyDown (KeyCode.P) && lead.isActive)
		{
			Retract();
			//TakeDamage();
			/*spline = spline.ControlPoints [5].SplitSpline ();
			segment = spline.ControlPoints[spline.ControlPointCount - 1];
			transform.position = segment.transform.position;*/
		}

		spline.Refresh ();


		tentacleLength = spline.Length;
	}

	public bool SelfCollide(GameObject go)
	{
		bool retVal = false;

		int maxSection = tentacleSectionList.Count - 1;
		Debug.Log ("Retracting");
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

	void Retract()
	{
		if (tentacleSectionList.Count <= 2 || segment.PreviousControlPoint == null)
			return;
		
		Vector3 newPosition = segment.PreviousControlPoint.transform.position;
		tentacleSectionList[tentacleSectionList.Count - 1].Remove();
		tentacleSectionList.RemoveAt(tentacleSectionList.Count - 1);
		lead.transform.position = newPosition;
	}

	public void TakeDamage()
	{
		if (!isFlashing)
			StartCoroutine(ColorFlash(damageTint, damageFlashDuration, damageFlashNumber));
		Retract();
	}

	public IEnumerator ColorFlash(Color color, float duration, int number)
	{
		isFlashing = true;

		float flashTime = duration / (number * 2);
		for ( int i = 0; i < number; i++)
		{
			float endTime = Time.time + flashTime;
			float startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				tentacleMaterial.color = Color.Lerp(startingTint, damageTint, t);
			}
			endTime = Time.time + flashTime;
			startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				tentacleMaterial.color = Color.Lerp(damageTint, startingTint, t);
			}
		}

		isFlashing = false;
	}

	void OnDestroy()
	{
		Debug.Log("Resetting Texture UVs");
		tentacleMaterial.mainTextureOffset = Vector2.zero;
		tentacleMaterial.color = startingTint;
	}
}
