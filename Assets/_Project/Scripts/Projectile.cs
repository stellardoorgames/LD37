using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform target;
    public string targetTag;
    public float speed = 1.0f;
    // public GameObject hitEffect;             // add an explosion effect here
    
	void Start () {
		
	}
	
	void Update () {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            Debug.LogError("Hit Target!");
        // } else if (other.tag == "Wall") {    // stub for future wall collision code, if needed
        } else {
            Debug.LogError("Hit something else");
        }
        // Instantiate (hitEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
