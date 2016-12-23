using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Grabbable : MonoBehaviour {
	
	public float grabRange;
	public bool isGrabbed;
	public Transform grabber;

	public event Action OnEscaped;

	public abstract bool Grabbed (Transform grabber);
	public abstract void Released ();
	
	public void EscapedEvent()
	{
		if (OnEscaped != null)
			OnEscaped();
	}

	/*abstract float grabRange {get; set;}
	abstract bool isGrabbed {get; set;}
	abstract Transform grabber {get; set;}
	abstract float GetGrabRange(Vector3 grabber);*/
}
