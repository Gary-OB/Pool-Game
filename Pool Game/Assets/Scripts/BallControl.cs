using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    GameObject player;
    CodedSpherePhysics playerEngine;
    Camera main;
    bool jumping = false;

    public int playerHealth = 3;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEngine = player.GetComponent<CodedSpherePhysics>();
        main = player.GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= 1.5f) {
            jumping = false;
        }

        if (playerHealth <= 0) {
            Destroy(this.gameObject);          
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerEngine.velocity += main.transform.forward * 7.0f;
        }

        if (Input.GetMouseButtonDown(1) && !jumping)
        {
            jumping = true;
            playerEngine.velocity += main.transform.up * 7.0f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Red") playerHealth -= 1;
        if (collision.gameObject.tag == "Black") playerHealth = 0;
    }
}
