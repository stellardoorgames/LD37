using UnityEngine;
using System.Collections;

enum CameraState
{
	Following,
	Centered,

}

public class CameraController : MonoBehaviour {

	//[Tooltip("If there's no target then the script will look for the object with the supplied tag.")]
	//public Transform followTarget;
	public string FollowTag;

	//[Range(0f, 1f)]
	public float followSpeed = 0.01f;

	Vector3 velocity;


	void Start () 
	{
		//if (followTarget == null)
	}
	
	void Update () 
	{
		
		GameObject[] targets = GameObject.FindGameObjectsWithTag (FollowTag);
		//transform.position = Vector3.Lerp (transform.position, target.position, followSpeed);
		FollowTarget(targets);
	}

	void FollowTarget(GameObject[] targets)
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
	}

	void CenterOn(Vector3 target)
	{
		
	}
}
