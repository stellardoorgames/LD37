using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

	Text _textObject;
	Text textObject{
		get { 
			if (_textObject == null)
				_textObject = GetComponent<Text> ();
			return _textObject;
			}
	}

	Color startingColor
	{
		get {return textObject.color;}
	}
	//string currentColor;

	/*void Start () 
	{
		textObject = GetComponent<Text> ();
		startingColor = textObject.color;
	}
	void OnEnable () 
	{
		textObject = GetComponent<Text> ();
		startingColor = textObject.color;
	}*/

	public void ClearText()
	{
		textObject.text = "";
	}
	public void SetText(string text, Color? color = null)
	{
		Color newColor = color ?? startingColor;
		string currentColor = ColorToHex (newColor);

		textObject.text = string.Format("<color=#{0}>{1}</color>", currentColor, text);
	}

	public void DisplayText(string[] textString, Color? color = null)
	{
		StopAllCoroutines ();
		StartCoroutine (DisplayTextCoroutine (textString, color));
	}
	public void DisplayText(string textString, Color? color = null)
	{
		StopAllCoroutines ();
		StartCoroutine (DisplayTextCoroutine (textString, color));
	}


	public IEnumerator DisplayTextCoroutine(string textLine, Color? color = null)
	{
		//TODO: Make this framerate independent

		Color newColor = color ?? startingColor;
		string currentColor = ColorToHex (newColor);
	
		for(int i = 0; i <= textLine.Length; i++)
		{
			yield return null;

			if (Input.anyKeyDown)
			{
				i = textLine.Length;
				
				yield return null;
			}

			textObject.text = string.Format ("<color=#{0}>{1}</color><color=#00000000>{2}</color>", currentColor, textLine.Substring (0, i), textLine.Substring (i));
			
		}


		yield return StartCoroutine(WaitForInput());
	}

	public IEnumerator DisplayTextCoroutine(string[] textString, Color? color = null)
	{
		foreach(string textLine in textString)
		{
			yield return StartCoroutine (DisplayTextCoroutine (textLine, color));
		}

	}

	IEnumerator WaitForInput()
	{
		while(! Input.anyKeyDown)
		{
			yield return null;
		}
	}

	// Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
	string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		return hex.ToLower();
	}

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
}
