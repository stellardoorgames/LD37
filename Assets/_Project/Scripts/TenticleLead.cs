using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleLead : MonoBehaviour {

	public float speed = 1f;

	public TenticleController tenticleController;

	public bool isActive = false;

	EnemyController enemy;
	EnemyController grabbedEnemy;


	void Start () 
	{

	}
	
	void Update () {

		//Vector2 p = -(pos - Input.mousePosition) * 0.005f;
		//pos = Input.mousePosition;

		if (isActive)
		{
			float horizontal = Input.GetAxis ("Horizontal" );
			float vertical = Input.GetAxis ("Vertical");
			
			transform.Translate (horizontal * speed, 0f, vertical * speed);
			//transform.Translate (p.x, 0f, p.y);

			if (grabbedEnemy != null)
			{
				if (Vector3.Distance(transform.position, enemy.transform.position) > 1)
				{
					grabbedEnemy.Released ();
					grabbedEnemy = null;
				}
			}

			if (Input.GetButtonDown ("Grab"))
			{
				if (grabbedEnemy != null)
				{
					grabbedEnemy.Released ();
					grabbedEnemy = null;
				}
				else
				{
					if (enemy != null)
					{
						if (Vector3.Distance(transform.position, enemy.transform.position) < 1)
						enemy.Grabbed (transform);
						grabbedEnemy = enemy;
						//enemy = null
					}
				}
			}
		}
	}

	public void Activate(bool active)
	{
		isActive = active;

		if (active)
			gameObject.tag = "CameraFollow";
		else
			gameObject.tag = "Untagged";
	}

	void OnTriggerEnter(Collider other)
	{
		Obstacle obstacle = other.GetComponent<Obstacle> ();

		if (obstacle != null)
		{
			Debug.Log ("Trigger");

			tenticleController.Collide (obstacle.gameObject);

		}

		EnemyController ec = other.GetComponent<EnemyController> ();

		if (ec != null)
		{
			enemy = ec;
		}
	}

}
