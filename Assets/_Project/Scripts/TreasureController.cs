using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour {

	public int moneyAmount = 100;


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

			LevelManager.IncrementStat(Stats.TreasureCollected);
			LevelManager.IncrementStat(Stats.MoneyCollected, moneyAmount);

			Destroy (gameObject);
		}
	}
}
