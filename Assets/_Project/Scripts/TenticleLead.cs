using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 800f;
	public float maxSpeed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	Grabbable grabbable;
	Grabbable carryingObject;

	Projector projector;

	Rigidbody rb;

	void Start () 
	{
		projector = GetComponentInChildren<Projector> ();
		projector.enabled = false;

		rb = GetComponent<Rigidbody>();
	}

	public void Stop()
	{
		rb.velocity = Vector3.zero;

	}

	public Vector3 GetMovement()
	{
		float horizontal = Input.GetAxis ("Horizontal" );
		float vertical = Input.GetAxis ("Vertical");


		return new Vector3 (horizontal, 0, vertical);
	}

	public void UpdatePosition()
	{

		if (isActive)
		{
			float horizontal = Input.GetAxis ("Horizontal" );
			float vertical = Input.GetAxis ("Vertical");


			Vector3 movement = new Vector3 (horizontal, 0, vertical) * speed * Time.deltaTime;
			//Debug.Log(rb.velocity.magnitude);
			//if (rb.velocity.magnitude < maxSpeed)
			rb.AddForce(movement);
		}
	}

	void Update () {

		//Vector2 p = -(pos - Input.mousePosition) * 0.005f;
		//pos = Input.mousePosition;

		if (isActive)
		{
			
			/*float horizontal = Input.GetAxis ("Horizontal" );
			float vertical = Input.GetAxis ("Vertical");


			Vector3 movement = new Vector3 (horizontal, 0, vertical) * speed * Time.deltaTime;
			//Debug.Log(rb.velocity.magnitude);
			//if (rb.velocity.magnitude < maxSpeed)
				rb.AddForce(movement);

			//transform.Translate (movement);
			//transform.Translate (p.x, 0f, p.y);
*/
			if (carryingObject != null)
			{
				if (Vector3.Distance(transform.position, grabbable.transform.position) > 2)
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
						if (Vector3.Distance (transform.position, grabbable.transform.position) < 1)
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
		{
			gameObject.tag = "CameraFollow";
			projector.enabled = true;
		}
		else
		{
			gameObject.tag = "Untagged";
			projector.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		TentacleSection obstacle = other.GetComponent<TentacleSection> ();

		if (obstacle != null)
		{
			tenticleController.SelfCollide (obstacle.gameObject);

		}

		Grabbable ec = other.GetComponent<Grabbable> ();

		if (ec != null)
		{
			grabbable = ec;
		}
	}

}
