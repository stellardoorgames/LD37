using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCommon;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	int princessStolen = 0;
	int enemiesGrabbed = 0;
	int soulStonesDropped = 0;
	int soulStonesGrabbed = 0;
	int soulStonesConsumed = 0;
	int soulStonesStolen = 0;


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

	static LevelManager instance;

	// Use this for initialization
	void Start () {

		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void AddKill()
	{
		instance.currentKills++;
	}

	static public void AddTreasure()
	{
		instance.currentTreasure++;
	}

}
