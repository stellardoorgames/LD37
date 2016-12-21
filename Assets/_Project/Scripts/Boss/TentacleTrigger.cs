using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TentacleTrigger : MonoBehaviour {

	public event Action<Character> OnEatSoul;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
		{
			Character enemy = other.GetComponent<Character>();

			if (enemy.isGrabbed && OnEatSoul != null && enemy != null)
				OnEatSoul(enemy);
			
		}

	}

}
