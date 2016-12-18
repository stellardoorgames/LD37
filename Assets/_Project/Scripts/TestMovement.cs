using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {
	public float speed = 1f;

	Rigidbody rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update () 
	{

		float horizontal = Input.GetAxis ("Horizontal" );
		float vertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (horizontal, 0, vertical) * speed * Time.deltaTime;

		rb.AddForce(movement);
		//transform.Translate (movement);
		//transform.Translate (p.x, 0f, p.y);


	}
}
