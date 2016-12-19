using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : CharController {

	IGrabbable grabbedObject;

	public override void Start ()
	{
		base.Start ();
	}

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (other.tag == "Princess" && !isGrabbed)
		{
			IGrabbable grab = other.GetComponent<IGrabbable>();

			if (grab != null)
			{
				grab.Grabbed(transform);
				grab.OnEscaped += OnGrabRelease;

				targetTag = "Exit";
				Retarget();
			}
		}
	}

	void OnGrabRelease()
	{
		if (grabbedObject != null)
			grabbedObject.OnEscaped -= OnGrabRelease;

		grabbedObject = null;

		targetTag = "Princess";
		Retarget();
	}

	public override bool Grabbed (Transform grabber)
	{
		if (grabbedObject != null)
		{
			grabbedObject.Released();
			OnGrabRelease();
		}

		return base.Grabbed (grabber);
	}

}
