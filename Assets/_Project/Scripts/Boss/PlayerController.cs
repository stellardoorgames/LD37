using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

	public float maxLife = 10;
	float currentLife;

	public List<TenticleController> tentacles = new List<TenticleController>();
	public List<ButtonNames> tentacleButtonNames = new List<ButtonNames>();
	List<Buttons> tentacleButtons = new List<Buttons>();
	public List<TentacleTrigger> tentacleTriggers = new List<TentacleTrigger>();

	public float startingMaxLength = 20f;
	public float currentMaxLength = 20f;
	public float maxMaxLength = 80f;
	public float addToMaxLengthPerSoul = 10f;
	public float currentTotalTentacleLength;
	public float totalTentacleLength{
		get {currentTotalTentacleLength = GetTotalTentacleLength();
			return currentTotalTentacleLength;}
	}

	[SerializeField]
	int _activeTenticleIndex = 0;
	public int activeTenticleIndex {
		get {return _activeTenticleIndex;}
		set {_activeTenticleIndex = (int)Mathf.Repeat(value, tentacles.Count);//value % tentacles.Count;
			SwitchTenticles (_activeTenticleIndex);}
	}
	TenticleLead activeTentacle;
	List<TenticleLead> tentacleLeads;

	UITentacleLength lengthUI;


	public UnityEvent OnDeath;
	//public UnityEvent OnFinish;

	void Start () 
	{

		currentLife = maxLife;

		tentacleLeads = new List<TenticleLead>();
		foreach(TenticleController tc in tentacles)
			tentacleLeads.Add(tc.lead);
	
		activeTentacle = tentacleLeads [activeTenticleIndex];

		activeTentacle.Activate(true);

		foreach(TentacleTrigger tt in tentacleTriggers)
			tt.OnEatSoul += OnEatSoul;
		
		foreach(ButtonNames n in tentacleButtonNames)
			tentacleButtons.Add(Buttons.CreateButton(n));

		lengthUI = GetComponent<UITentacleLength>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Cycle"))
			activeTenticleIndex++;
		if (Input.GetButtonDown ("CyclePrevious"))
			activeTenticleIndex--;

		for(int i = 0; i < tentacleButtons.Count; i++)
		{
			//if (Input.GetButtonUp(tentacleButtons[i]))
			if (tentacleButtons[i].isHeld)
				tentacles[i].Retract(0.1f);
			if (tentacleButtons[i].isTapReleased)
				activeTenticleIndex = i;
			if (tentacleButtons[i].isDoubleTapped)
				Debug.Log("Use Special");
		}

/*
		if (Input.GetButtonUp ("West"))
			activeTenticleIndex = 0;
		
		if (Input.GetButtonUp ("South"))
			activeTenticleIndex = 1;

		if (Input.GetButtonUp ("East"))
			activeTenticleIndex = 2;
*/
		if(Input.GetKeyDown(KeyCode.M))
			OnEatSoul(null);
		
		
		//if (Input.GetButtonDown ("North"))
		//	activeTenticleIndex = 2;

	}

	public float GetTotalTentacleLength()
	{
		float length = 0;
		foreach (TenticleController tc in tentacles)
			length += tc.tentacleLength;
		return length;
	}

	void SwitchTenticles(int index)
	{
		foreach(TenticleLead tc in tentacleLeads)
			tc.Activate(false);
		
		tentacleLeads [index].Activate(true);
	}

	public void OnEatSoul(SoulGemController gem)
	{
		StartCoroutine(ExtendMaxLength(addToMaxLengthPerSoul, 1f));
		StartCoroutine(lengthUI.ColorFlash(1f, 1));
		if (gem != null)
			gem.Destroy();
	}

	IEnumerator ExtendMaxLength(float amount, float duration)
	{
		float startTime = Time.time;
		float endTime = startTime + duration;

		float startLength = currentMaxLength;
		float endLength = startLength + amount;

		while (Time.time < endTime)
		{
			float t = Mathf.InverseLerp(startTime, endTime, Time.time);
			currentMaxLength = Mathf.Lerp(startLength, endLength, t);

			yield return null;
		}
	}

}
