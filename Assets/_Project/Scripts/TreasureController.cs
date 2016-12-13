using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		TenticleLead tenticle = other.GetComponent<TenticleLead> ();

		if (tenticle != null)
		{
			LevelManager.AddTreasure ();
			Destroy (gameObject);
		}
	}
}
