using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public string targetTag;

	public Text text;

	Transform target;

	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		GameObject[] targets = GameObject.FindGameObjectsWithTag (targetTag);

		if (targets.Length > 0)
		{
			float dist = int.MaxValue;

			foreach(GameObject go in targets)
			{
				float d = Vector3.Distance (transform.position, go.transform.position);
				if (d < dist)
					target = go.transform;
			}

			agent.destination = target.position;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		agent.speed = 2f;


	}
}
