using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour {

    public GameObject pickupEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		TenticleGrabber tenticle = other.GetComponent<TenticleGrabber> ();

		if (tenticle != null)
		{
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
			//LevelManager.AddTreasure ();
			Destroy (gameObject);
		}
	}
}
