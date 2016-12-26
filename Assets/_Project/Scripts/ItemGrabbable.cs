using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabbable : Grabbable {

	Animator anim;

	public float minGrabDelay = 0.1f;

	public virtual void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public override bool Grabbed(Transform grabber)
	{
		if (Time.time < lastGrabbedTime + minGrabDelay)
			return false;

		lastGrabbedTime = Time.time;

		EscapedEvent();

		isGrabbed = true;

		this.grabber = grabber;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);

		transform.position = grabber.position;

		transform.SetParent (grabber);

		StartCoroutine(GrabbedCoroutine());

		return true;
	}

	IEnumerator GrabbedCoroutine()
	{
		while(isGrabbed)
		{
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.1f);
			yield return null;
		}
	}

	public override void Released()
	{
		if (isGrabbed == false)
			return;
		isGrabbed = false;

		Debug.Log ("Released");

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);
	}

}
