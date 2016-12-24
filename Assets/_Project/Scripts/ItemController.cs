using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour {
	
	public virtual void Destroy()
	{
		Grabbable g = GetComponent<Grabbable>();
		if (g != null)
			g.EscapedEvent();
		
		Destroy(gameObject);
	}
}
