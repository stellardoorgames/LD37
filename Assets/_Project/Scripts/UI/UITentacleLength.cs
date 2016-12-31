using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITentacleLength : MonoBehaviour {

	public PlayerController controller;
	public List<RectTransform> tentacleBars = new List<RectTransform>();

	public RectTransform tentacleLengthPanel;
	public RectTransform zeroPanel;
	public RectTransform endOfTentacleMarker;

	float startingTentacleLength;
	public float startingTotalTentacleLength = 7.5f;
	
	public Image outlineImage;
	List<Image> isFlashing;
	public Color flashingColor;

	Vector2 barPosition;
	Vector2 barPositionOffset;

	List<Image> tentacleImages;

	void Start () 
	{
		startingTentacleLength = startingTotalTentacleLength / controller.tentacles.Count;

		barPosition = tentacleLengthPanel.anchoredPosition;

		tentacleImages = new List<Image>();
		foreach(RectTransform rt in tentacleBars)
			tentacleImages.Add(rt.GetComponent<Image>());

		isFlashing = new List<Image>();

		controller.OnGrowMaxLength += () => StartCoroutine(ColorFlash(outlineImage, 1f, 1));
		controller.OnExceedLength += () => StartCoroutine(ColorFlash(outlineImage, 1f, 1));
		foreach(TenticleController tc in controller.tentacles)
			tc.OnTakeDamage += (int i) => StartCoroutine(ColorFlash(tentacleImages[i], 1f, 1));
		
	}
	
	void Update () 
	{
		float t = Mathf.InverseLerp(controller.startingMaxLength, controller.maxMaxLength, controller.currentMaxLength);
		barPositionOffset = Vector2.Lerp(barPosition, Vector2.zero, t);
		tentacleLengthPanel.anchoredPosition = barPositionOffset;
		zeroPanel.position = Vector2.zero;

		Vector2 fullBarPosition =  endOfTentacleMarker.position;//new Vector2(barImageWidth, 0f);
		Vector2 currentBarPosition = Vector2.zero;
		for (int i = 0; i < tentacleBars.Count; i++)
		{
			float tentacleLength = controller.tentacles[i].tentacleLength - startingTentacleLength;
			float maxTentacleLength = controller.currentMaxLength - startingTotalTentacleLength;
			float u = Mathf.InverseLerp(0f, maxTentacleLength, tentacleLength);
			currentBarPosition += Vector2.Lerp (Vector2.zero, fullBarPosition, u);
			tentacleBars[i].anchoredPosition = currentBarPosition;
		}
	}

	public IEnumerator ColorFlash(Image image, float duration, int number)
	{
		if (isFlashing.Contains(image))
			yield break;

		isFlashing.Add(image);

		Color startingColor = image.color;

		float flashTime = duration / (number * 2);
		for ( int i = 0; i < number; i++)
		{
			float endTime = Time.time + flashTime;
			float startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				image.color = Color.Lerp(startingColor, flashingColor, t);
			}
			endTime = Time.time + flashTime;
			startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				image.color = Color.Lerp(startingColor, flashingColor, t);
			}
		}

		image.color = startingColor;
			
		isFlashing.Remove(image);
	}

}
