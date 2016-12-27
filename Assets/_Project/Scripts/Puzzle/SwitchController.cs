using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SwitchType
{
	On_When_Held,
	Toggled
}

public class SwitchController : MonoBehaviour {

	public bool isOn = false;
	
	public UnityEvent OnSwitchOn;
	public UnityEvent OnSwitchOff;
	public UnityEvent OnSwitchToggle;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
