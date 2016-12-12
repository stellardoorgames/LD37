using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1f;

	public TenticleController tenticleController;

	Rigidbody rb;

	Vector3 pos;

	// Use this for initialization
	void Start () {

		rb = GetComponentInChildren <Rigidbody> ();

		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		//pos += new Vector3 (horizontal, 0f, vertical) * speed;

		//rb.MovePosition (pos);
		//rb.AddForce (new Vector3 (horizontal, 0f, vertical) * 5f, ForceMode.VelocityChange);
		//rb.ad
		transform.Translate (horizontal * speed, 0f, vertical * speed);
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
