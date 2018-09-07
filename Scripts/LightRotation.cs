using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour {

    Light lightOptions;
    public float speedRotation;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lightOptions = GetComponent<Light>();
        lightOptions.transform.RotateAround(transform.position, Vector3.right, speedRotation);
    }
}
