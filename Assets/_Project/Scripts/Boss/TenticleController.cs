using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;

public class TenticleController : MonoBehaviour {

	public float speed = 800f;
	public int tentacleID;
	//public float maxSpeed = 1f;
	public PlayerController playerController;
	public GameObject colliderObject;
	public CurvyGenerator generator;
	CurvySpline spline;
	MeshRenderer materialObject;
	//Material meshMaterial;
	CGMaterialSettingsEx materialSettings;


	public float segmentLength = 1f;
	public int selfCollideNumber = 8;

	public Color damageTint;
	Color startingTint;
	public float damageFlashDuration = 2;
	public int damageFlashNumber = 3;
	bool isFlashing = false;

	public bool isActive = false;

	[HideInInspector]
	public float tentacleLength;

	List<TentacleSection> tentacleSectionList = new List<TentacleSection>();

	Projector projector;
	Rigidbody rb;
	TenticleGrabber grabber;

	CurvySplineSegment segment;

	bool isRetracting = false;

	Vector2 textureOffset = Vector2.zero;

	float startingLength;

	Vector3 startingPosition;

	bool isInitialized = false;

	void Awake () 
	{
		projector = GetComponentInChildren<Projector> ();
		projector.enabled = false;

		rb = GetComponent<Rigidbody>();

		grabber = GetComponent<TenticleGrabber>();

		startingPosition = transform.position;
	}

	IEnumerator Start()
	{
		while (!generator.IsInitialized)
			yield return null;

		InputSplinePath[] splineArray = generator.GetComponentsInChildren<InputSplinePath>();
		foreach(InputSplinePath sp in splineArray)
			if (sp.name == "Input Spline Path" + tentacleID.ToString())
				spline = sp.GetComponentInChildren<CurvySpline>();
		
		BuildVolumeMesh[] meshes = generator.GetComponentsInChildren<BuildVolumeMesh>();
		foreach(BuildVolumeMesh bvm in meshes)
			if (bvm.name == "Volume Mesh" + tentacleID)
				materialSettings = bvm.MaterialSetttings[0];
		
		CGMeshResource[] meshResource = generator.GetComponentsInChildren<CGMeshResource>();
		foreach(CGMeshResource r in meshResource)
			if (r.name.Contains("Mesh00" + tentacleID))
				materialObject = r.Renderer;

		isInitialized = true;

		tentacleSectionList.Add (TentacleSection.Create (colliderObject, null, spline.LastVisibleControlPoint, this));

		startingLength = spline.Length;

		startingTint = materialObject.material.color;

		StartCoroutine(Swirl());
	}
	
	void Update () 
	{
		if (!isInitialized)
			return;
		
		segment = spline.LastVisibleControlPoint;//spline.ControlPoints [spline.Count - 1];//
		CurvySplineSegment previousSegment = segment.PreviousControlPoint;

		if (isActive)
		{
			if (playerController.totalTentacleLength > playerController.currentMaxLength)
			{
				Debug.Log("Exceeded Length");
				
				//If too long, only update position if the player is backtracking
				float dist1 = Vector3.Distance(transform.position, previousSegment.transform.position);
				Vector3 movement = GetMovement() + transform.position;
				float dist2 = Vector3.Distance(movement, previousSegment.transform.position);
				if (dist1 > dist2)
					UpdatePosition();
			}
			else
				UpdatePosition();
		}



		segment.transform.position = transform.position;


		if (previousSegment.Length > segmentLength)
		{
			segment = spline.InsertBefore (segment);
			tentacleSectionList.Add (TentacleSection.Create (colliderObject, transform, segment, this));

		}

		if (previousSegment.Length < segmentLength * 0.45f)
		{
			RemoveSegment(tentacleSectionList.Count - 1);
		}

		spline.Refresh ();

		float offset = spline.Length - tentacleLength;

		tentacleLength = spline.Length;

		textureOffset.y -= offset * 0.25f;
		//textureOffset.y += GetMovement().x * Time.deltaTime * 0.5f;

		//meshMaterial.mainTextureOffset = textureOffset;
		materialSettings.UVOffset = textureOffset;
		//first.SwirlTurns += offset * 0.0005f;

	}

	public void Activate(bool active)
	{
		isActive = active;
		grabber.isActive = active;

		if (active)
		{
			CameraController.SetTarget(gameObject);
			projector.enabled = true;
		}
		else
		{
			CameraController.RemoveTarget(gameObject);
			projector.enabled = false;
		}
	}

	public Vector3 GetMovement()
	{
		float horizontal = Input.GetAxis ("Horizontal" );
		float vertical = Input.GetAxis ("Vertical");

		return new Vector3 (horizontal, 0, vertical);
	}

	public void UpdatePosition()
	{
			Vector3 movement = GetMovement() * speed * Time.deltaTime;
		rb.AddForce(movement);
	}

	IEnumerator Swirl()
	{
		CurvySplineSegment first = spline.FirstVisibleControlPoint;

		Vector3 v1 = new Vector3(-0.0002f, 0f, 0f);
		Vector3 v2 = new Vector3(0.0002f, 0f, 0f);
		while(true)
		{
			v1 = -v1;
			v2 = -v2;
			float startTime = Time.time;
			float endTime = startTime + 3f;
			while (Time.time < endTime)
			{
				float t = Mathf.InverseLerp(startTime, endTime, Time.time);
				first.SwirlTurns += Vector3.Slerp(v1, v2, t).x;
				yield return null;
			}

		}
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
		if (tentacleSectionList.Count < 3)
			return;
		
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
		if (tentacleSectionList.Count <= 3)
		{
			transform.position = startingPosition;
			yield break;
		}

		if (segment.PreviousControlPoint == null)
			yield break;

		isRetracting = true;

		float startTime = Time.time;
		float endTime = Time.time + duration;

		Vector3 startPosition = transform.position;

		Vector3 newPosition = tentacleSectionList[tentacleSectionList.Count - 1].transform.position;

		while (Time.time < endTime)
		{
			float t = Mathf.InverseLerp(startTime, endTime, Time.time);
			 
			transform.position = Vector3.Lerp(startPosition, newPosition, t);
			
			yield return null;
		}

		transform.position = newPosition;
		isRetracting = false;
	}

	public void TakeDamage(float amount)
	{
		if (tentacleLength > startingLength + 0.25f)
			playerController.TakeDamage(amount);
		
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

	void OnTriggerStay(Collider other)
	{
		TentacleSection obstacle = other.GetComponent<TentacleSection> ();

		if (obstacle != null)
			SelfCollide (obstacle.gameObject);

	}
}
