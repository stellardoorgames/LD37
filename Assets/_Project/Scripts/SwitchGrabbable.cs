using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGrabbable : Grabbable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool Grabbed (Transform grabber)
	{
		Debug.Log("Grab Switch todo");
		throw new System.NotImplementedException ();
	}

	public override void Released ()
	{
		throw new System.NotImplementedException ();
	}
}
