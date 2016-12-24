using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ButtonNames
{
	West,
	South,
	East
}

public class Buttons : MonoBehaviour {

	public static Dictionary<ButtonNames, Buttons> buttons = new Dictionary<ButtonNames, Buttons>();

	public static Buttons lastActiveButton = null;

	public ButtonNames buttonName;
	public string buttonNameString;
	public float timeToStartHold = 0.2f;
	public float doubleTapSpeed = 0.25f;

	public bool isPressed = false;
	public bool isHeld = false;
	public bool isReleased = false;
	public bool isTapReleased = false;
	public bool isHeldReleased = false;
	public bool isDoubleTapped = false;

	public float heldTime;
	public float startHoldTime;
	public float pressedTime;
	public float releasedTime;

	public event Action<Buttons> OnPress;
	public event Action<Buttons> OnTapRelease;
	public event Action<Buttons> OnHoldStart;
	public event Action<Buttons> OnHold;
	public event Action<Buttons> OnHoldRelease;
	public event Action<Buttons> OnRelease;
	public event Action<Buttons> OnDoubleTap;


	public static Buttons CreateButton(ButtonNames buttonName)
	{
		GameObject go = new GameObject("Button " + buttonName.ToString());
		Buttons b = go.AddComponent<Buttons>();
		b.buttonName = buttonName;

		return b;
	}

	void Start () 
	{
		buttons.Add(buttonName, this);
		buttonNameString = buttonName.ToString();
	}
	
	void Update () 
	{
		isPressed = false;
		isReleased = false;
		isTapReleased = false;
		isHeldReleased = false;
		isDoubleTapped = false;

		if (Input.GetButtonDown(buttonNameString))
		{
			lastActiveButton = this;

			if (Time.time - pressedTime <= doubleTapSpeed)
			{
				isDoubleTapped = true;
				if (OnDoubleTap != null)
					OnDoubleTap(this);
			}
			
			pressedTime = Time.time;
			isPressed = true;
			if (OnPress != null)
				OnPress(this);
		}

		else if (Input.GetButton(buttonNameString))
		{
			if (!isHeld)
			{
				if (Time.time - pressedTime >= timeToStartHold)
				{
					isHeld = true;
					startHoldTime = Time.time;
				}
			}

			if (isHeld)
			{
				heldTime = Time.time - startHoldTime;

				lastActiveButton = this;
				if (OnHold != null)
					OnHold(this);
			}
		}

		else if (Input.GetButtonUp(buttonNameString))
		{
			lastActiveButton = this;
			
			releasedTime = Time.time;

			if (isHeld)
			{
				isHeldReleased = true;
				if (OnHoldRelease != null)
					OnHoldRelease(this);
			}
			else
			{
				isTapReleased = true;
				if (OnTapRelease != null)
					OnTapRelease(this);
			}

			isHeld = false;
			isReleased = true;
			if (OnRelease != null)
				OnRelease(this);
		}
	}
}
