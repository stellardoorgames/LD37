using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : Grabbable {

	public string targetTag;

	//public AnimationClip deathAnim;
	//public Animator anim;

	public Text text;

	Transform target;

    public GameObject lavaDeathEffect;

	public override void Start()
	{
		base.Start ();

		Retarget ();
	}

	public void Retarget()
	{
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
			if (agent != null)
				agent.destination = target.position;

		}
	}


	protected virtual void OnTriggerEnter(Collider other)
	{

		//if (other.tag == "Lava")
		//{
		//	//Animator anim = GetComponent<Animator> ();
		//	//anim.Play ("Death");
		//	Debug.Log ("Lava");
  //          Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);

  //          Death ();
		//}
	}

	public void Death()
	{
		LevelManager.AddKill ();
		Destroy (gameObject);

	}

}
