using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*transform.LookAt(Camera.main.transform.position);
		Vector3 rotate = transform.rotation.eulerAngles;
		rotate.y = 0;
		rotate.x = 0;
		transform.rotation = Quaternion.Euler(rotate);*/

		transform.rotation = Quaternion.identity;
	}
}
