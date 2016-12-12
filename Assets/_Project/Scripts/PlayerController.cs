using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	const int numTenticles = 4;

	public TenticleLead tenticleNorth;
	public TenticleLead tenticleSouth;
	public TenticleLead tenticleEast;
	public TenticleLead tenticleWest;

	[SerializeField]
	int _activeTenticleIndex = 0;
	public int activeTenticleIndex {
		get {return _activeTenticleIndex;}
		set {_activeTenticleIndex = value % numTenticles;
			SwitchTenticles (_activeTenticleIndex);}
	}
	TenticleLead activeTenticle;
	List<TenticleLead> tenticles;

	// Use this for initialization
	void Start () {

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
}
