using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	public float maxLife = 10;
	float currentLife;

	public List<TenticleController> tentacles = new List<TenticleController>();

	public float startingMaxTotalTentacleLength = 10f;
	public float currentMaxTotalTentacleLength = 10f;
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

	public static void TakeDamage(float damage)
	{
		instance.currentLife -= damage;
		if (instance.currentLife <= 0f)
			instance.OnDeath.Invoke ();
	}
}
