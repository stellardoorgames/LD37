using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : EnemyController {

	public float attackInterval = 1;

	bool isAttacking;

    public Transform castPoint;
    public Projectile fireballProjectile;
    public Transform target;

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

        if (other.tag == targetTag)
        {
            target = other.transform;
            Debug.Log("Attack");

            if (!isAttacking) {
                StartCoroutine(AttackCoroutine());
            }
            isAttacking = true;
            Debug.LogWarning("STARTED A COROUTINE");
            
		}

	}

	public override void Update ()
	{
		base.Update ();

		//Collider[] colliders = Physics.OverlapSphere (transform.position, .5f);
		//bool isTentacle = false;
  //      foreach (Collider c in colliders) {
  //          if (c.tag == "Tentacle") {
  //              isTentacle = true;
  //              //target = c.transform;
  //          }
  //      }
		//isAttacking = isTentacle;
	}

	IEnumerator AttackCoroutine()
	{
		isAttacking = true;

		anim.SetBool ("isAttacking", true);

		agent.Stop ();

        while (isAttacking) { 
		//{
			anim.SetTrigger ("Attack");
            Projectile fireball = Instantiate(fireballProjectile, castPoint.position, Quaternion.identity);

            fireball.targetTag = "Tentacle"; //TODO: This needs to be made modular
            fireball.target = target;
            fireball.speed = 1f;
			
			//float nextAttackTime = Time.time + attackInterval;

            //while (Time.time < nextAttackTime)
            //{
            //    yield return null;
            //}

            yield return new WaitForSeconds(attackInterval);

        }

		agent.Resume ();

		anim.SetBool ("isAttacking", false);
	}
}
