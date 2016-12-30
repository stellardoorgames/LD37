using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlash : MonoBehaviour {
	
	//public Renderer render;
	public Color flashColor;
	//Color startingTint;
	public float flashDuration = 2;
	public int flashNumber = 3;
	public AnimationCurve flashCurve;
	List<Material> isFlashing;

	void Start () 
	{
		isFlashing = new List<Material>();
		//startingTint = meshRenderer.material.color;	
	}

	public void FlashColor(Renderer renderer)
	{
		for (int i = 0; i < renderer.materials.Length; i++) 
			StartCoroutine(FlashColorCoroutine(renderer, i, flashColor, flashDuration, flashNumber));
	}

	IEnumerator FlashColorCoroutine(Renderer render, int index, Color color, float duration, int number)
	{
		if (isFlashing.Contains(render.materials[index]))
			yield break;
		
		isFlashing.Add(render.materials[index]);

		Color startingTint = render.materials[index].color;

		float flashTime = duration / (number);
		for ( int i = 0; i < number; i++)
		{
			float endTime = Time.time + flashTime;
			float startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				render.materials[index].color = Color.Lerp(startingTint, flashColor, flashCurve.Evaluate(t));
			}
		}

		render.materials[index].color = startingTint;

		isFlashing.Remove(render.materials[index]);
	}
}
