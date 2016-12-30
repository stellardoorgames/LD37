using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlash : MonoBehaviour {
	
	//public Renderer render;
	public Color damageTint;
	//Color startingTint;
	public float damageFlashDuration = 2;
	public int damageFlashNumber = 3;
	bool isFlashing = false;

	void Start () 
	{
		//startingTint = meshRenderer.material.color;	
	}

	public void FlashColor(Renderer renderer)
	{
		if (!isFlashing)
		{
			for (int i = 0; i < renderer.materials.Length; i++) {
				StartCoroutine(FlashColorCoroutine(renderer, i, damageTint, damageFlashDuration, damageFlashNumber));
			}
		}
	}

	IEnumerator FlashColorCoroutine(Renderer render, int index, Color color, float duration, int number)
	{
		isFlashing = true;

		Color startingTint = render.materials[index].color;

		float flashTime = duration / (number * 2);
		for ( int i = 0; i < number; i++)
		{
			float endTime = Time.time + flashTime;
			float startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				render.materials[index].color = Color.Lerp(startingTint, damageTint, t);
			}
			endTime = Time.time + flashTime;
			startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				render.materials[index].color = Color.Lerp(damageTint, startingTint, t);
			}
		}

		isFlashing = false;
	}
}
