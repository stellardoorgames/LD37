using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TentacleTrigger : MonoBehaviour {

	public event Action<CharController> OnEatSoul;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
		{
			CharController enemy = other.GetComponent<CharController>();

			if (enemy.isGrabbed && OnEatSoul != null && enemy != null)
				OnEatSoul(enemy);
			
		}

	}

}
