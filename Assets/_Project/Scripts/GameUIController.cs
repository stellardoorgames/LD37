using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	[SerializeField]
	Text scoreText;

	public int currentTime = 0;
	public SpriteRenderer TimeUI;

	//[SerializeField]
	//CanvasRotation canvasRotation;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () {

	}

	private void HandleHealth () {

        //update currenthealth here
        //use currenthealth to update colors
        TimeUI.color = Color.Lerp(Color.red,Color.green, currentTime);

	}

}
