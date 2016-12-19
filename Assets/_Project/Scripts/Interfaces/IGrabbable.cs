using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {

	bool isGrabbed {get; set;}
	Transform grabber {get; set;}
	void Grabbed (Transform Grabber);
	void Released ();
	Transform grabTransform {get; set;}
}
