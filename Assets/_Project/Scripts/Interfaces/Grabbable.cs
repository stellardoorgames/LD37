using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Grabbable : MonoBehaviour {

	public bool isStatic = false;
	public float grabRange;
	public bool isGrabbed;
	[HideInInspector]
	public Transform grabber;
	[HideInInspector]
	public float lastGrabbedTime = 0f;

	public event Action OnEscaped;

	public event Action OnGrab;
	public event Action OnRelease;

	
	//public abstract bool Grabbed (Transform grabber);
	//public abstract void Released ();

	public void EscapedEvent()
	{
		if (OnEscaped != null)
			OnEscaped();
	}

	public virtual bool Grabbed(Transform grabber)
	{
		EscapedEvent();

		isGrabbed = true;

		lastGrabbedTime = Time.time;

		this.grabber = grabber;

		if (OnGrab != null)
			OnGrab();

		return true;
	}

	public virtual void Released()
	{
		isGrabbed = false;

		grabber = null;

		if (OnRelease != null)
			OnRelease();
	}
}
