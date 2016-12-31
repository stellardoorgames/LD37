using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvasOnPlay : MonoBehaviour {

	//public List<GameObject> objectsToEnable;

	void Awake () 
	{
		Canvas[] canvases = GetComponentsInChildren<Canvas>(true) ;
		foreach(Canvas c in canvases)
			c.gameObject.SetActive(true);
	}

}
