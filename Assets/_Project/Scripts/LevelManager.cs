using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Stats
{
	PrincessNabbed,
	PrincessLost,
	PrincessDeath,
	EnemiesSpawned,
	EnemiesGrabbed,
	EnemiesKilled,
	SoulStonesSpawned,
	SoulStonesGrabbed,
	SoulStonesConsumed,
	SoulStonesNabbed,
	SoulStonesLost,
}

public class LevelManager : MonoBehaviour {
	
	public int winConditionKills = 0;
	int _currentKills = 0;
	int currentKills {
		get { return _currentKills; }
		set {_currentKills = value;
			if (winConditionKills > 0 && _currentKills >= winConditionKills)
				OnWin.Invoke ();}
	}

	public int winConditionTreasure = 0;
	int _currentTreasure = 0;
	int currentTreasure {
		get { return _currentTreasure; }
		set {_currentTreasure = value;
			if (winConditionTreasure > 0 && _currentTreasure >= winConditionTreasure)
				OnWin.Invoke ();}
	}
	public float winConditionTime = 0f;

	public UnityEvent OnLose;
	public UnityEvent OnWin;

	public UnityEvent OnFirstPrincessNab;
	public UnityEvent OnFirstPrincessSteal;
	public UnityEvent OnFirstEnemySpawn;
	public UnityEvent OnFirstEnemyGrab;
	public UnityEvent OnFirstEnemyKill;
	public UnityEvent OnFirstSoulStoneDrop;
	public UnityEvent OnFirstSoulStoneGrab;
	public UnityEvent OnFirstSoulStoneConsume;
	public UnityEvent OnFirstSoulStoneSteal;
	public UnityEvent OnFirstSoulStoneLost;

	static LevelManager instance;

	Dictionary<Stats, int> levelStats = new Dictionary<Stats, int>();
	static Dictionary<Stats, int> totalStats = new Dictionary<Stats, int>();

	// Use this for initialization
	void Start () 
	{
		instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void IncrementStat(Stats stat)
	{
		if (!instance.levelStats.ContainsKey(stat))
		{
			instance.levelStats.Add(stat, 0);
			instance.FirstStatEvent(stat);
		}
		
		instance.levelStats[stat]++;

		if (!totalStats.ContainsKey(stat))
			totalStats.Add(stat, 0);

		totalStats[stat]++;
	}

	static public void IncrementKills(string tagName)
	{
		if (tagName == "Enemy")
			IncrementStat(Stats.EnemiesKilled);
		else if (tagName == "Princess")
			IncrementStat(Stats.PrincessDeath);
	}

	void FirstStatEvent(Stats stat)
	{
		switch (stat) {
		case Stats.PrincessNabbed:
			OnFirstPrincessNab.Invoke();
			break;
		case Stats.EnemiesGrabbed:
			OnFirstEnemyGrab.Invoke();
			break;
		case Stats.SoulStonesGrabbed:
			OnFirstSoulStoneGrab.Invoke();
			break;
		case Stats.SoulStonesSpawned:
			OnFirstSoulStoneDrop.Invoke();
			break;
		default:
			break;
		}

		Debug.Log(stat + " incremented");
	}

}
