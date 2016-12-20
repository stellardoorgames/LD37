using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : CharController {

	public override void Start ()
	{
		base.Start ();
	}

	public override void Update ()
	{
		base.Update();



		Collider[] colliders = Physics.OverlapSphere (transform.position, .5f);
		bool isTentacle = false;
		foreach (Collider c in colliders)
			if (c.tag == "Tentacle")
				isTentacle = true;
		isAttacking = isTentacle;

		if (isGrabbed || grabbedObject != null)
			isAttacking = false;
	}
	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		if (!isGrabbed)
		{
			if (other.tag == "Princess")
			{
				AttemptToGrab(other.gameObject);
				/*IGrabbable grab = other.GetComponent<IGrabbable>();
				
				if (grab != null)
				{
					grab.Grabbed(transform);
					grab.OnEscaped += OnGrabRelease;
					grabbedObject = grab;
					
				}*/
				Retarget("Exit");
			}
			if (other.tag == "Tentacle" && grabbedObject == null)
			{
				//Debug.Log("KnightAttack");
				IDamagable d = other.GetComponent<IDamagable>();
				if (d != null)
					StartCoroutine (AttackCoroutine (d));
			}
		}
	}

}
