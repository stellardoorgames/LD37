using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public GameObject lavaDeathEffect;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            //Animator anim = GetComponent<Animator> ();
            //anim.Play ("Death");

            Debug.Log("Lava"); //TODO: Make this particular to inherited classes.  For now there's only lava.
            Instantiate(lavaDeathEffect, transform.position, Quaternion.identity);

            other.GetComponent<EnemyController>().Death();

        }
    }

}
