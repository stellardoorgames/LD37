using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	
	public Character.DeathType hazardType;

	public GameObject effectPrefab;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
			Instantiate(effectPrefab, other.transform.position, Quaternion.identity);

		/*Character c = other.GetComponent<Character>();

		if (c.vulnerabilities.Contains(hazardType))
		{
			Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
			
		}*/

	}

}
