using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum KeyTypes
{
	None,
	Generic,
	Red,
	Green,
	Blue
}

public class GateController : MonoBehaviour {

	public bool isOpen = false;
	
	public KeyTypes lockType;

	public List<SwitchController> switches = new List<SwitchController>();

	public UnityEvent OnOpen;
	public UnityEvent OnClose;

	void Start () 
	{
		foreach(SwitchController sc in switches)
		{
			sc.OnSwitchOn.AddListener(OnSwitch);
			sc.OnSwitchOff.AddListener(OnSwitch);
		}

		//if (isOpen)
		//	Open();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSwitch()
	{
		bool isUnlocked = true;
		foreach(SwitchController sc in switches)
		{
			if (!sc.isOn)
				isUnlocked = false;
		}

		if (isUnlocked && !isOpen)
			Open();
		if (!isUnlocked && isOpen)
			Close();
	}

	/*public void OnKey(KeyTypes KeyType)
	{
		if (KeyType == lockType && !isOpen)
			Open();
	}*/

	public void Open()
	{
		isOpen = true;

		OnOpen.Invoke();
	}

	public void Close()
	{
		isOpen = false;

		OnClose.Invoke();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Key")
		{
			KeyController kc = other.GetComponent<KeyController>();
			if (kc.keyType == lockType && !isOpen)
				Open();
		}
	}
}
