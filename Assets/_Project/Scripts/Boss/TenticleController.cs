using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;

public class TenticleController : MonoBehaviour {

	public TenticleLead lead;
	public PlayerController playerController;
	public CurvySpline spline;
	public GameObject colliderObject;
	public MeshRenderer materialObject;

	public float segmentLength = 1f;
	public int selfCollideNumber = 8;

	public Color damageTint;
	Color startingTint;
	public float damageFlashDuration = 2;
	public int damageFlashNumber = 3;
	bool isFlashing = false;

	[HideInInspector]
	public float tentacleLength;

	List<TentacleSection> tentacleSectionList = new List<TentacleSection>();

	CurvySplineSegment segment;

	bool isRetracting = false;

	Vector2 textureOffset = Vector2.zero;

	void Start()
	{
		foreach (CurvySplineSegment s in spline.Segments)
		{
			tentacleSectionList.Add (TentacleSection.Create (colliderObject, null, s, this));
		}

		startingTint = materialObject.material.color;

		textureOffset.y = -0.25f;
	}
	
	void Update () 
	{
		segment = spline.LastVisibleControlPoint;//spline.ControlPoints [spline.Count - 1];//
		CurvySplineSegment previousSegment = segment.PreviousControlPoint;

		if (playerController.totalTentacleLength > playerController.currentMaxLength)
		{
			Debug.Log("Exceeded Length");

			//If too long, only update position if the player is backtracking
			float dist1 = Vector3.Distance(lead.transform.position, previousSegment.transform.position);
			Vector3 movement = lead.GetMovement() + lead.transform.position;
			float dist2 = Vector3.Distance(movement, previousSegment.transform.position);
			if (dist1 > dist2)
				lead.UpdatePosition();
		}
		else
			lead.UpdatePosition();


		segment.transform.position = lead.transform.position;


		if (previousSegment.Length > segmentLength)
		{
			segment = spline.InsertBefore (segment);
			tentacleSectionList.Add (TentacleSection.Create (colliderObject, lead.transform, segment, this));
		}

		if (previousSegment.Length < segmentLength * 0.45f)
		{
			RemoveSegment(tentacleSectionList.Count - 1);
		}

		spline.Refresh ();

		float offset = spline.Length - tentacleLength;

		tentacleLength = spline.Length;

		textureOffset.x -= offset * 0.25f;
		//textureOffset.y += lead.GetMovement().x * Time.deltaTime * 0.5f;

		materialObject.material.mainTextureOffset = textureOffset;

	}

	public bool SelfCollide(GameObject go)
	{
		int minIndex = 2;
		int maxSection = tentacleSectionList.Count - 1;
		if (maxSection < minIndex)
			return false;
		
		int minSection = maxSection - selfCollideNumber;
		if (minSection < minIndex)
			minSection = minIndex;
		
		for (int i = minSection; i < maxSection; i++)
		{
			TentacleSection ts = tentacleSectionList [i];
			if (go == ts.gameObject)
			{
				RemoveSegment (i);
				return true;
			}
		}

		return false;
	}

	void RemoveSegment(int index)
	{
		for (int i = tentacleSectionList.Count - 1; i >= index; i--)
		{
			tentacleSectionList [i].Remove ();
			tentacleSectionList.RemoveAt (i);
		}

	}

	public void Retract(float speed = 0.25f)
	{
		if (!isRetracting)
			StartCoroutine(RetractCoroutine(speed));
	}

	IEnumerator RetractCoroutine(float duration)
	{
		if (tentacleSectionList.Count <= 2 || segment.PreviousControlPoint == null)
			yield break;

		isRetracting = true;

		float startTime = Time.time;
		float endTime = Time.time + duration;

		Vector3 startPosition = lead.transform.position;

		Vector3 newPosition = tentacleSectionList[tentacleSectionList.Count - 1].transform.position;

		while (Time.time < endTime)
		{
			float t = Mathf.InverseLerp(startTime, endTime, Time.time);
			 
			lead.transform.position = Vector3.Lerp(startPosition, newPosition, t);
			
			yield return null;
		}

		lead.transform.position = newPosition;
		isRetracting = false;
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
				materialObject.material.color = Color.Lerp(startingTint, damageTint, t);
			}
			endTime = Time.time + flashTime;
			startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				materialObject.material.color = Color.Lerp(damageTint, startingTint, t);
			}
		}

		isFlashing = false;
	}

}
