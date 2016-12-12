using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Texture2D fadeTexture;

	float fadeAlpha;

	bool isFading = false;

	static Fader _instance;
	static Fader instance {
		get
		{
			if (_instance == null)
				_instance = new GameObject("_Fader").AddComponent<Fader>();
			return _instance;          
		}
	}

	/*void Start()
	{
		//Only to allow static methods to access this instance
		if (_instance == null)
			_instance = this;
	}*/

	void CreateTexture(Color color)
	{
		fadeTexture = new Texture2D(1, 1);
		fadeTexture.SetPixel(1, 1, color);
		fadeTexture.Apply();

	}

	public static void FadeIn(Color startColor, float duration)
	{ 
		instance.CreateTexture (startColor);
		instance.StartCoroutine(instance.FadeCoroutine(1f, 0f, duration)); 
	}
	public static void FadeIn(Texture2D startTexture, float duration)
	{
		instance.fadeTexture = startTexture;
		instance.StartCoroutine(instance.FadeCoroutine(1f, 0f, duration)); 
	}
	public static void FadeOut(Color endColor, float duration)
	{ 
		instance.CreateTexture (endColor);
		instance.StartCoroutine(instance.FadeCoroutine(0f, 1f, duration)); 
	}
	public static void FadeOut(Texture2D endTexture, float duration)
	{ 
		instance.fadeTexture = endTexture;
		instance.StartCoroutine(instance.FadeCoroutine(0f, 1f, duration)); 
	}

	/*public static void SetToClear()
	{
		instance.isFading = false;
	}*/

	IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration)
	{
		isFading = true;
		fadeAlpha = startAlpha;

		float startTime = Time.time;
		float endTime = startTime + duration;

		while (Time.time < endTime)
		{ 
			yield return null; 

			float t = Mathf.InverseLerp (startTime, endTime, Time.time);
			fadeAlpha = Mathf.Lerp (startAlpha, endAlpha, t);
		}

		fadeAlpha = endAlpha;

		yield return null;

		isFading = false;
	}

	void OnGUI()
	{
		if (! isFading)
			return;
		
		GUI.color = new Color(1f, 1f, 1f, fadeAlpha);
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), fadeTexture);

		//Might need this?
		GUI.color = Color.white;
	}
}
