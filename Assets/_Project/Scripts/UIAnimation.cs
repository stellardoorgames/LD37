using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour {

    [SerializeField]
    private GameObject rotateObject;

    [SerializeField]
    private int spinSpeed;

	void Start () {
        
    }

  
    void Update () {
        rotateObject.transform.Rotate(0,0,6.0f * spinSpeed * Time.deltaTime);
    }
}
