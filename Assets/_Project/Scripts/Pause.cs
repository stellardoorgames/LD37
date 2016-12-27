using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	float previousTimeScale;

	void OnEnable ()
	{
		previousTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}

	void OnDisable ()
	{
		Time.timeScale = previousTimeScale;
	}
}
