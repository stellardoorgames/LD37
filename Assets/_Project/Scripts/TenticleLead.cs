using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	Grabbable grabbable;
	Grabbable carryingObject;


	void Start () 
	{

	}
	
	void Update () {

		//Vector2 p = -(pos - Input.mousePosition) * 0.005f;
		//pos = Input.mousePosition;

		if (isActive)
		{
			float horizontal = Input.GetAxis ("Horizontal" );
			float vertical = Input.GetAxis ("Vertical");
			
			transform.Translate (horizontal * speed, 0f, vertical * speed);
			//transform.Translate (p.x, 0f, p.y);

			if (carryingObject != null)
			{
				if (Vector3.Distance(transform.position, grabbable.transform.position) > 1)
				{
					carryingObject.Released ();
					carryingObject = null;
				}
			}

			if (Input.GetButtonDown ("Grab"))
			{
				if (carryingObject != null)
				{
					carryingObject.Released ();
					carryingObject = null;
				}
				else
				{
					if (grabbable != null)
					{
						if (Vector3.Distance(transform.position, grabbable.transform.position) < 1)
						grabbable.Grabbed (transform);
						carryingObject = grabbable;
						//enemy = null
					}
				}
			}
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
		Obstacle obstacle = other.GetComponent<Obstacle> ();

		if (obstacle != null)
		{
			Debug.Log ("Trigger");

			tenticleController.Collide (obstacle.gameObject);

		}

		Grabbable ec = other.GetComponent<Grabbable> ();

		if (ec != null)
		{
			grabbable = ec;
		}
	}

}
