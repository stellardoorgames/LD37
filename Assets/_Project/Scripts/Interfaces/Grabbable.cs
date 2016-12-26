using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Grabbable : MonoBehaviour {
	
	public float grabRange;
	public bool isGrabbed;
	public Transform grabber;

	public event Action OnEscaped;

	public float lastGrabbedTime = 0f;
	
	public abstract bool Grabbed (Transform grabber);
	public abstract void Released ();

	public void EscapedEvent()
	{
		if (OnEscaped != null)
			OnEscaped();
	}
}
