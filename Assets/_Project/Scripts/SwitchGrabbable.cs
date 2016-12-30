using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGrabbable : Grabbable {

	SwitchController switchController;

	void Start () 
	{
		switchController = GetComponent<SwitchController>();
	}

	public override bool Grabbed (Transform grabber)
	{
		this.grabber = grabber;

		isGrabbed = true;

		switchController.SwitchON();

		StartCoroutine(GrabbedCoroutine());

		return true;
	}

	IEnumerator GrabbedCoroutine()
	{
		while(isGrabbed)
		{
			float dist = Vector3.Distance (transform.position, grabber.position);
			if (dist > grabRange)
			{
				EscapedEvent();
			}
			
			yield return null;
		}
	}

	public override void Released ()
	{
		isGrabbed = false;

		switchController.SwitchOff();
	}
}
