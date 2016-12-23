using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TentacleTrigger : MonoBehaviour {

	public event Action<SoulGemController> OnEatSoul;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "SoulGem")
		{
			SoulGemController gem = other.GetComponent<SoulGemController>();

			if (gem != null && OnEatSoul != null)
				OnEatSoul(gem);
			
		}

	}

}
