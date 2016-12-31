using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Stats
{
	PlayerKilled,
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
	TreasureCollected,
	MoneyCollected
}

public class LevelManager : MonoBehaviour {

	public List<Stats> winConditions;
	public List<int> winAmount;
	public bool winRequireAll = false;

	public List<Stats> loseConditions;
	public List<int> loseAmount;
	public bool loseRequireAll = false;


	/*public int winConditionKills = 0;
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
	public float winConditionTime = 0f;*/

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

	Dictionary <Stats, int> winCon;
	Dictionary <Stats, int> loseCon;

	// Use this for initialization
	void Start () 
	{
		instance = this;

		winCon = new Dictionary<Stats, int>();
		for(int i = 0; i < winConditions.Count; i++)
			winCon.Add(winConditions[i], winAmount[i]);

		loseCon = new Dictionary<Stats, int>();
		for(int i = 0; i < loseConditions.Count; i++)
			loseCon.Add(loseConditions[i], loseAmount[i]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void IncrementStat(Stats stat, int amount = 1)
	{
		if (!instance.levelStats.ContainsKey(stat))
		{
			instance.levelStats.Add(stat, 0);
			instance.FirstStatEvent(stat);
		}
		
		instance.levelStats[stat] += amount;

		if (!totalStats.ContainsKey(stat))
			totalStats.Add(stat, 0);

		totalStats[stat] += amount;

		instance.testConditions(stat);
	}

	static public void IncrementKills(string tagName)
	{
		if (tagName == "Enemy")
			IncrementStat(Stats.EnemiesKilled);
		else if (tagName == "Princess")
			IncrementStat(Stats.PrincessDeath);
	}

	bool testConditions(Stats stat)
	{
		bool conditionFullfilled = false;

		if (winCon.ContainsKey(stat) && winCon[stat] >= levelStats[stat])
		{
			conditionFullfilled = true;

			if (winRequireAll)
			{
				foreach(KeyValuePair<Stats, int> wc in winCon)
				{
					if (levelStats[wc.Key] < wc.Value)
						conditionFullfilled = false;
				}
				/*for (int i = 0; i < winConditions.Count; i++) 
				{
					if (levelStats[winConditions[i]] < winAmount[i])
						conditionFullfilled = false;
				}*/
			}

			if (conditionFullfilled)
				OnWin.Invoke();
		}

		if (loseCon.ContainsKey(stat) && loseCon[stat] >= levelStats[stat])
		{
			conditionFullfilled = true;

			if (loseRequireAll)
			{
				foreach(KeyValuePair<Stats, int> lc in loseCon)
				{
					if (levelStats[lc.Key] < lc.Value)
						conditionFullfilled = false;
				}
				/*for (int i = 0; i < loseConditions.Count; i++) 
				{
					if (levelStats[loseConditions[i]] < loseAmount[i])
						conditionFullfilled = false;
				}*/
			}

			if (conditionFullfilled)
				OnLose.Invoke();
		}
		
		return conditionFullfilled;
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
