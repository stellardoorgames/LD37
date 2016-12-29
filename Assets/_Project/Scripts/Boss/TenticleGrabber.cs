using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleGrabber : MonoBehaviour {
	
	public float grabRadius = 0.75f;

	public List<string> grabPriority = new List<string>();

	public bool isActive = false;

	Grabbable carryingObject;

	void Update () {
		
		if (isActive)
		{
			if (Input.GetButtonDown ("Grab"))
			{
				if (carryingObject != null)
				{
					OnGrabRelease();
				}
				else
				{
					//Collider[] colliders = Physics.OverlapSphere (transform.position, grabRadius);
					List<Collider> colliders = new List<Collider>(Physics.OverlapSphere (transform.position, grabRadius));

					if (colliders.Count > 0)
					{


						/*Collider col = null;

						colliders.Find(ct => ct.tag == "SoulGem");


						if (col == null)
							col = colliders.Find(ct => ct.tag == "Enemy");


						if (col != null)
							grabbedObject = col.GetComponent<Grabbable>();*/
						
						Grabbable grabbedObject = null;

						foreach(string grabTag in grabPriority)
						{
							foreach (Collider c in colliders)
							{
								if (c.tag == grabTag)
									grabbedObject = c.GetComponent<Grabbable>();
							}

							if (grabbedObject != null)
								break;
						}

						if (grabbedObject == null)
						{
							foreach (Collider c in colliders)
							{
								grabbedObject = c.GetComponent<Grabbable>();
								if (grabbedObject != null)
									break;
							}
							
						}
						
						if (grabbedObject != null)
							AttemptToGrab (grabbedObject);
						
					}

				}
			}
		}
	}

	void AttemptToGrab(Grabbable grabbable)
	{
		Vector3 grabPosition = new Vector3(grabbable.transform.position.x, transform.position.y, grabbable.transform.position.z);
		transform.position = grabPosition;

		bool grabWorked = grabbable.Grabbed (transform);
		if (grabWorked)
		{
			carryingObject = grabbable;
			carryingObject.OnEscaped += OnGrabRelease;
		}
	}

	void OnGrabRelease()
	{
		if (carryingObject != null)
		{
			carryingObject.Released ();
			carryingObject.OnEscaped -= OnGrabRelease;
		}
		
		carryingObject = null;
	}

}
