using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	const int NumTenticles = 4;

	public float maxLife = 10;
	float currentLife;

	public TenticleLead tenticleNorth;
	public TenticleLead tenticleSouth;
	public TenticleLead tenticleEast;
	public TenticleLead tenticleWest;

	[SerializeField]
	int _activeTenticleIndex = 0;
	public int activeTenticleIndex {
		get {return _activeTenticleIndex;}
		set {_activeTenticleIndex = value % NumTenticles;
			SwitchTenticles (_activeTenticleIndex);}
	}
	TenticleLead activeTenticle;
	List<TenticleLead> tenticles;

	public UnityEvent OnDeath;
	public UnityEvent OnFinish;

	static PlayerController _instance;
	static PlayerController instance;
	/*{
		get
		{
			if (_instance == null)
				_instance = new GameObject("_PlayerController").AddComponent<PlayerController>();
			return _instance;          
		}
	}*/

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{

		currentLife = maxLife;

		tenticles = new List<TenticleLead>();
		tenticles.Add (tenticleSouth);
		tenticles.Add (tenticleWest);
		tenticles.Add (tenticleNorth);
		tenticles.Add (tenticleEast);

		activeTenticle = tenticles [activeTenticleIndex];

		activeTenticle.Activate(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Cycle"))
			activeTenticleIndex++;
		if (Input.GetButtonDown ("CyclePrevious"))
			activeTenticleIndex++;
		
		if (Input.GetButtonDown ("South"))
			activeTenticleIndex = 0;

		if (Input.GetButtonDown ("West"))
			activeTenticleIndex = 1;
		
		if (Input.GetButtonDown ("North"))
			activeTenticleIndex = 2;

		if (Input.GetButtonDown ("East"))
			activeTenticleIndex = 3;
		
	}

	void SwitchTenticles(int index)
	{
		foreach(TenticleLead tc in tenticles)
		{
			tc.Activate(false);
		}

		tenticles [index].Activate(true);


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
