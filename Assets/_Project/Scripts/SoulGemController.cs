using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGemController : ItemController {
	public enum GemType
	{
		Standard
	}

	public GemType gemType;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
			Destroy();
	}

}
