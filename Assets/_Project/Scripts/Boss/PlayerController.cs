using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxLife = 10;
	float currentLife;

	public List<TenticleController> tentacles = new List<TenticleController>();

	public List<Image> tentacleBarImages = new List<Image>();
	public float startingMaxTotalTentacleLength = 20f;
	public float currentMaxTotalTentacleLength = 20f;
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
		set {_activeTenticleIndex = value % tentacles.Count;
			SwitchTenticles (_activeTenticleIndex);}
	}
	TenticleLead activeTentacle;
	List<TenticleLead> tentacleLeads;

	public List<TentacleTrigger> tentacleTriggers = new List<TentacleTrigger>();

	public UnityEvent OnDeath;
	public UnityEvent OnFinish;

	static PlayerController instance;

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{

		currentLife = maxLife;

		tentacleLeads = new List<TenticleLead>();
		foreach(TenticleController tc in tentacles)
			tentacleLeads.Add(tc.lead);
	
		activeTentacle = tentacleLeads [activeTenticleIndex];

		activeTentacle.Activate(true);

		foreach(TentacleTrigger tt in tentacleTriggers)
		{
			tt.OnEatSoul += OnEatSoul;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Cycle"))
			activeTenticleIndex++;
		if (Input.GetButtonDown ("CyclePrevious"))
			activeTenticleIndex++;
		
		if (Input.GetButtonDown ("West"))
			activeTenticleIndex = 0;
		
		if (Input.GetButtonDown ("South"))
			activeTenticleIndex = 1;

		if (Input.GetButtonDown ("East"))
			activeTenticleIndex = 2;
		
		
		//if (Input.GetButtonDown ("North"))
		//	activeTenticleIndex = 2;
		float percent = 0f;
		for(int i = 0; i < tentacleBarImages.Count; i++)
		{
			percent += (tentacles[i].tentacleLength - 2.5f) / (currentMaxTotalTentacleLength - 7.5f);
			tentacleBarImages[i].material.SetFloat("_Progress", percent);
			
		}
	}

	public float GetTotalTentacleLength()
	{
		float len = 0;
		foreach (TenticleController tc in tentacles)
			len += tc.tentacleLength;
		return len;
	}

	void SwitchTenticles(int index)
	{
		foreach(TenticleLead tc in tentacleLeads)
		{
			tc.Activate(false);
		}

		tentacleLeads [index].Activate(true);


		/*for(int i = 0; i < tenticles.Count; i++)
		{
			if (i == )
		}*/
	}

	public void OnEatSoul(CharController enemy)
	{
		currentMaxTotalTentacleLength += addToMaxLengthPerSoul;
		enemy.Death();
	}

	public static void TakeDamage(float damage)
	{
		instance.currentLife -= damage;
		if (instance.currentLife <= 0f)
			instance.OnDeath.Invoke ();
	}
}
