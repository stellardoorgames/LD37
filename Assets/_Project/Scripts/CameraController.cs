using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;

enum CameraState
{
	Following,
	Centered,

}

public class CameraController : MonoBehaviour {

	//[Tooltip("If there's no target then the script will look for the object with the supplied tag.")]
	//public Transform followTarget;
	public List<GameObject> cameraTargets = new List<GameObject>();

	static public int numberOfTargets = 1;
	//public string FollowTag;

	//[Range(0f, 1f)]
	public float followSpeed = 0.01f;

	Vector3 velocity;

	static CameraController instance;

	void Awake()
	{
		instance = this;
	}

	void Update () 
	{
		
		//GameObject[] targets = GameObject.FindGameObjectsWithTag (FollowTag);
		//transform.position = Vector3.Lerp (transform.position, target.position, followSpeed);
		//GameObject[] targets = cameraTargets[cameraTargets.Count - 1];

		FollowTargets();
	}

	public static void TemporaryTarget(GameObject target, float duration)
	{
		instance.StartCoroutine(instance.TemporaryTargetCoroutine(target, duration));
	}

	public static void AddTarget(GameObject target)
	{
		instance.cameraTargets.Add(target);
	}

	public static void SetTarget(GameObject target)
	{
		RemoveLastTarget();

		instance.cameraTargets.Add(target);
	}

	public static void RemoveTarget(GameObject target)
	{
		if (instance.cameraTargets.Contains(target))
			instance.cameraTargets.Remove(target);
	}

	public static void RemoveLastTarget()
	{
		if (instance.cameraTargets.Count > 0)
			instance.cameraTargets.RemoveAt(instance.cameraTargets.Count - 1);
	}

	IEnumerator TemporaryTargetCoroutine(GameObject target, float duration)
	{
		instance.cameraTargets.Add(target);

		yield return new WaitForSeconds(duration);

		RemoveTarget(target);

	}

	void FollowTargets()
	{
		Vector3 target = Vector3.zero;

		if (numberOfTargets == 0 || cameraTargets.Count <= 0)
			target = transform.position + velocity;

		else if (numberOfTargets == 1)
			target = cameraTargets[cameraTargets.Count - 1].transform.position;

		transform.position = Vector3.SmoothDamp (transform.position, target, ref velocity, followSpeed);
	}

	/*void FollowTarget(GameObject[] targets)
	{
		Vector3 target;

		if (targets.Length == 0)
			target = transform.position + velocity;

		else if (targets.Length == 1)
			target = targets[0].transform.position;

		else //if (targets.Length > 1)
		{
			target = Vector3.zero;

			for (int i = 0; i < targets.Length; i++)
				target += targets [i].transform.position;
			
			target /= targets.Length;
		}

		transform.position = Vector3.SmoothDamp (transform.position, target, ref velocity, followSpeed);
	}*/

}
