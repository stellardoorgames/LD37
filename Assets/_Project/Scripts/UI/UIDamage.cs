using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour {

	PlayerController controller;
	public Image materialImage;

	float startingValue;

	// Use this for initialization
	void Start () {
		controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		startingValue = materialImage.material.GetFloat("_Progress");
	}
	
	// Update is called once per frame
	void Update () {

		float t = Mathf.InverseLerp(0f, controller.maxLife, controller.currentLife);
		materialImage.material.SetFloat("_Progress", t);
		
	}

	void OnDisable()
	{
		materialImage.material.SetFloat("_Progress", startingValue);
	}
}
