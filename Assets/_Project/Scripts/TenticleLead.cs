using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	Rigidbody rb;

	Vector3 pos;

	// Use this for initialization
	void Start () {

		rb = GetComponentInChildren <Rigidbody> ();

		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive)
		{
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			
			transform.Translate (horizontal * speed, 0f, vertical * speed);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Obstacle obsticle = other.GetComponent<Obstacle> ();

		if (obsticle != null)
		{
			Debug.Log ("Trigger");

			tenticleController.Collide (obsticle.gameObject);


		}
	}

}
