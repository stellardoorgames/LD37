using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOneShot : MonoBehaviour {

    public float removeAfterSeconds = 1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(RemoveSelf(removeAfterSeconds));
	}

    public IEnumerator RemoveSelf(float duration) {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

}
