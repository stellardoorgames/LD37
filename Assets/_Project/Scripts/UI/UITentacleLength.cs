using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITentacleLength : MonoBehaviour {

	//public List<Image> tentacleBarImages = new List<Image>();
	public List<RectTransform> tentacleBars = new List<RectTransform>();
	float barImageWidth = 347f;

	public RectTransform panel1;
	public RectTransform panel2;
	public RectTransform object3;

	float startingTentacleLength;
	public float startingTotalTentacleLength = 7.5f;
	
	public Image flashImage;
	bool isFlashing = false;
	public Color startingColor;
	public Color flashingColor;

	Vector2 barPosition;
	Vector2 barPositionOffset;
	//float barWidth;

	//float previousMaxLength;

	PlayerController controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController>();

		startingTentacleLength = startingTotalTentacleLength / controller.tentacles.Count;
		//previousMaxLength = controller.currentMaxLength;

		//barWidth = panel1.rect.width;
		barPosition = panel1.anchoredPosition;


	}
	
	// Update is called once per frame
	void Update () {

		/*if (controller.currentMaxTotalTentacleLength == previousMaxLength)
			return;

		previousMaxLength = controller.currentMaxTotalTentacleLength;
*/
		//StartCoroutine(ColorFlash(flashingColor, 1f, 2));
		//float t = Mathf.InverseLerp(controller.startingMaxTotalTentacleLength, controller.maxMaxTotalTentacleLength, controller.currentMaxTotalTentacleLength);
		//barPositionOffset = Vector2.Lerp(barPosition, Vector2.zero, t);
		//bar.anchoredPosition = barPositionOffset;

		//float percent = 0f;
		//float percent = 1f - t;//Mathf.Lerp (.8f, 0f, t);//
		//float p = 1f - percent;
		/*for(int i = 0; i < tentacleBarImages.Count; i++)
		{
			float tentacleLength = controller.tentacles[i].tentacleLength - startingTentacleLength;
			float maxTentacleLength = controller.currentMaxTotalTentacleLength - startingTotalTentacleLength;
			percent += tentacleLength / maxTentacleLength;
			tentacleBarImages[i].material.SetFloat("_Progress", percent);

		}*/

		float t = Mathf.InverseLerp(controller.startingMaxLength, controller.maxMaxLength, controller.currentMaxLength);
		barPositionOffset = Vector2.Lerp(barPosition, Vector2.zero, t);
		panel1.anchoredPosition = barPositionOffset;
		panel2.position = Vector2.zero;

		Vector2 fullBarPosition =  object3.position;//new Vector2(barImageWidth, 0f);
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


	public IEnumerator ColorFlash(float duration, int number)
	{
		isFlashing = true;

		float flashTime = duration / (number * 2);
		for ( int i = 0; i < number; i++)
		{
			float endTime = Time.time + flashTime;
			float startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				flashImage.color = Color.Lerp(startingColor, flashingColor, t);
			}
			endTime = Time.time + flashTime;
			startingTime = Time.time;
			while (Time.time < endTime)
			{
				yield return null;
				float t = Mathf.InverseLerp(startingTime, endTime, Time.time);
				flashImage.color = Color.Lerp(startingColor, flashingColor, t);
			}
		}

		flashImage.color = startingColor;
			
		isFlashing = false;
	}

}
