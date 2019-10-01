using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Yellow") Destroy(collision.gameObject);
    }
}
