using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	//Vector3 pos;

	// Use this for initialization
	void Start () {

		//pos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		//Vector2 p = -(pos - Input.mousePosition) * 0.005f;
		//pos = Input.mousePosition;

		if (isActive)
		{
			float horizontal = Input.GetAxis ("Horizontal" );
			float vertical = Input.GetAxis ("Vertical");
			
			transform.Translate (horizontal * speed, 0f, vertical * speed);
			//transform.Translate (p.x, 0f, p.y);
		}
	}

	public void Activate(bool active)
	{
		isActive = active;

		if (active)
			gameObject.tag = "CameraFollow";
		else
			gameObject.tag = "Untagged";
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
