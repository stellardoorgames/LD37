using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabbable : Grabbable {

	Animator anim;

	public virtual void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public override bool Grabbed(Transform grabber)
	{
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
