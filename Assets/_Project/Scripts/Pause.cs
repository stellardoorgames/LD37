using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	float previousTimeScale;

	public void UnPause()
	{
		Time.timeScale = 1f;
	}

	void OnEnable ()
	{
		previousTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}

	void OnDisable ()
	{
		UnPause();
	}
}
