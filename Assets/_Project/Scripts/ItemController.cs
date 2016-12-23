using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour {

	Rigidbody rb;
	Animator anim;
	Collider itemCollider;

	/*[SerializeField]
	float _grabRange = 1f;
	public float grabRange {
		get {return _grabRange;}
		set {_grabRange = value;}
	}
	public bool isGrabbed {get; set;}
	public Transform grabber {get; set;}
	public Transform grabTransform {get; set;}
	public event Action OnEscaped;*/

	public float GetGrabRange(Vector3 grabberPosition)
	{
		return Vector3.Distance(grabberPosition, transform.position);
	}

	/*public virtual bool Grabbed(Transform grabber)
	{
		if (OnEscaped != null)
			OnEscaped();

		isGrabbed = true;

		this.grabber = grabber;

		if (rb != null)
			rb.isKinematic = true;

		if (itemCollider != null)
			itemCollider.enabled = false;

		if (anim != null)
			anim.SetBool ("isGrabbed", true);

		transform.position = grabber.position;

		transform.SetParent (grabber);

		StartCoroutine(GrabbedCoroutine());

		return true;
	}

	IEnumerator GrabbedCoroutine()
	{
		while(isGrabbed)
		{
			transform.position = Vector3.Lerp (transform.position, grabber.position, 0.1f);
			yield return null;
		}
	}

	public virtual void Released()
	{
		if (isGrabbed == false)
			return;
		isGrabbed = false;

		Debug.Log ("Released");

		if (rb != null)
			rb.isKinematic = false;

		if (itemCollider != null)
			itemCollider.enabled = true;

		if (anim != null)
			anim.SetBool ("isGrabbed", false);

		transform.SetParent (null);
	}*/

	public virtual void Destroy()
	{
		Grabbable g = GetComponent<Grabbable>();
		if (g != null)
			g.EscapedEvent();
		//if (OnEscaped != null)
		//	OnEscaped();

		Destroy(gameObject);
	}
}
