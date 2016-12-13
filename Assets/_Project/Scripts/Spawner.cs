using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerSequenceOptions
{
	Loop,
	Random,
	Shuffle
}

public class Spawner : MonoBehaviour {

	public List<GameObject> spawnObjects;

	public SpawnerSequenceOptions sequenceOption;

	public float beginTime;
	public float intervalTime;
	public float stopTime;
	public int spawnNum;

	// Use this for initialization
	IEnumerator Start () 
	{
		float startTime = Time.time + beginTime;

		while (Time.time < startTime)
			yield return null;
		
		yield return StartCoroutine (SpawnCoroutine ());
	}

	IEnumerator SpawnCoroutine()
	{
		float sequenceEndTime = stopTime + Time.time;


		if (spawnNum == 0)
			spawnNum = int.MaxValue;

		for (int i = 0; i < spawnNum ; i++)
		{
			if (stopTime > 0f && Time.time > sequenceEndTime)
				yield break;
			
			if (sequenceOption == SpawnerSequenceOptions.Random)
				yield return StartCoroutine (SpawnRandom ());
			else if (sequenceOption == SpawnerSequenceOptions.Loop)
				yield return StartCoroutine (SpawnRandom ());
			
		}

	}

	IEnumerator SpawnRandom()
	{
		float nextSpawnTime = Time.time + intervalTime;

		int randomIndex = Random.Range (0, spawnObjects.Count - 1);

		GameObject go = spawnObjects [randomIndex];

		Instantiate (go);
		go.transform.position = transform.position;

		while (Time.time < nextSpawnTime)
			yield return null;
		
	}
}
