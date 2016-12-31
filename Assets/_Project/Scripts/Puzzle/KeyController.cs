using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : ItemController {

	public enum Types
	{
		None,
		Generic,
		Red,
		Green,
		Blue
	}

	public Types keyType;

	public Renderer MeshRenderer;

	public static Dictionary<Types, Color> keyColors =new Dictionary<Types, Color>
	{
		{Types.Generic, Color.white},
		{Types.Red, Color.red},
		{Types.Green, Color.green},
		{Types.Blue, Color.blue},
		{Types.None, Color.white}
	};

	void Start()
	{
		MeshRenderer.material.color = keyColors[keyType];
	}
}
