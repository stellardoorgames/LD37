using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerSequenceOptions
{
	Loop,
	Shuffle,
	Random
}

public class Spawner : MonoBehaviour {

	public SpawnerSequenceOptions sequenceOption;
	public List<GameObject> spawnObjects = new List<GameObject>();
	public List<float> intervalTimes = new List<float>();


	//public float beginTime;
	public float stopAfterTime;
	public int stopAfterNum;

	int _spawnIndex;
	int spawnIndex
	{
		get {return _spawnIndex;}
		set {_spawnIndex = value;
			if (_spawnIndex > spawnObjects.Count - 1)
				_spawnIndex = 0;}
	}

	int _intervalIndex;
	int intervalIndex
	{
		get {return _intervalIndex;}
		set {_intervalIndex = value;
			if (_intervalIndex > intervalTimes.Count - 1)
				_intervalIndex = intervalTimes.Count - 1;}
	}

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SpawnCoroutine ());
	}

	IEnumerator SpawnCoroutine()
	{
		float sequenceEndTime = stopAfterTime + Time.time;

		if (stopAfterNum == 0)
			stopAfterNum = int.MaxValue;

		for (int i = 0; i < stopAfterNum ; i++)
		{
			yield return new WaitForSeconds(intervalTimes[intervalIndex]);

			if (stopAfterTime > 0f && Time.time > sequenceEndTime)
				yield break;

			if (spawnIndex == 0 && sequenceOption == SpawnerSequenceOptions.Shuffle)
				spawnObjects.Shuffle();

			if (sequenceOption == SpawnerSequenceOptions.Random)
				spawnIndex = Random.Range (0, spawnObjects.Count - 1);


			Spawn (spawnIndex);
			

			spawnIndex++;
			intervalIndex++;
		}
	}

	void Spawn(int index)
	{
		GameObject go = spawnObjects [index];

		Instantiate (go);
		go.transform.position = transform.position;
	}
}
