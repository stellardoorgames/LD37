using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public interface IGrabbable {

	float grabRange {get; set;}
	bool isGrabbed {get; set;}
	Transform grabber {get; set;}
	bool Grabbed (Transform grabber);
	void Released ();
	event Action OnEscaped;
	float GetGrabRange(Vector3 grabber);
	//Transform grabTransform {get; set;}
}
