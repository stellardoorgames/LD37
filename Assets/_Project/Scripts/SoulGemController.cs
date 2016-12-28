using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGemController : ItemController {
	public enum GemType
	{
		Standard
	}

	public GemType gemType;

	ItemGrabbable grabbable;

	void Start()
	{
		LevelManager.IncrementStat(Stats.SoulStonesSpawned);
		grabbable = GetComponent<ItemGrabbable>();
		grabbable.OnGrab += OnGrabbed;
	}

	void OnGrabbed()
	{
		if (grabbable.grabber != null)
		{
			if (grabbable.grabber.tag == "Enemy")
				LevelManager.IncrementStat(Stats.SoulStonesNabbed);
			if (grabbable.grabber.tag == "Tentacle")
				LevelManager.IncrementStat(Stats.SoulStonesGrabbed);
			
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Exit")
			Destroy();
	}

}
