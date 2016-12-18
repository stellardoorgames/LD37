using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour {

	public bool isOn = false;
	
	public UnityEvent OnSwitchOn;
	public UnityEvent OnSwitchOff;
	public UnityEvent OnSwitchToggle;

	public bool testActivate = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (testActivate && !isOn)
			SwitchON();
		
		if (!testActivate && isOn)
			SwitchOff();
	}

	public void SwitchON()
	{
		isOn = true;
		//Animation
		OnSwitchOn.Invoke();
		OnSwitchToggle.Invoke();
	}

	public void SwitchOff()
	{
		isOn = false;
		OnSwitchOff.Invoke();
		OnSwitchToggle.Invoke();
	}


}
