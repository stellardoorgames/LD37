using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 800f;
	public float maxSpeed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	IGrabbable grabbable;
	IGrabbable carryingObject;

	Projector projector;

	Rigidbody rb;

	void Start () 
	{
		projector = GetComponentInChildren<Projector> ();
		projector.enabled = false;

		rb = GetComponent<Rigidbody>();

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
			Vector3 movement = GetMovement() * speed * Time.deltaTime;
			//if (rb.velocity.magnitude < maxSpeed)
			rb.AddForce(movement);
		}
	}

	void Update () {

		//Vector2 p = -(pos - Input.mousePosition) * 0.005f;
		//pos = Input.mousePosition;

		if (isActive)
		{
			/*if (carryingObject != null)
			{
				if (Vector3.Distance(transform.position, carryingObject.grabTransform.position) > 2)
				{
					carryingObject.Released ();
					carryingObject = null;
				}
			}*/
			//Debug.Log(carryingObject);
			if (Input.GetButtonDown ("Grab"))
			{
				if (carryingObject != null)
				{
					carryingObject.Released ();
					OnGrabRelease();
					//carryingObject = null;
				}
				else
				{
					if (grabbable != null)
					{
						//if (Vector3.Distance (transform.position, grabbable.grabTransform.position) < 1f)
						if (grabbable.GetGrabRange(transform.position) < grabbable.grabRange)
						{
							bool grabWorked = grabbable.Grabbed (transform);
							if (grabWorked)
							{
								carryingObject = grabbable;
								carryingObject.OnEscaped += OnGrabRelease;
							}
						}
						//enemy = null
					}
				}
			}
		}
	}

	void OnGrabRelease()
	{
		if (carryingObject != null)
			carryingObject.OnEscaped -= OnGrabRelease;
		
		carryingObject = null;
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
			tenticleController.SelfCollide (obstacle.gameObject);
		
		IGrabbable g = other.GetComponent<IGrabbable> ();
		if (g != null)
			grabbable = g;

	}

	public void HasReleased (IGrabbable grabbable)
	{
		
	}

}
