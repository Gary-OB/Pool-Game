using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Text healthBar;
    public Text ballsLeft;
    public BallControl player;

    GameObject[] yellowBalls;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        yellowBalls = GameObject.FindGameObjectsWithTag("Yellow");

        healthBar.text = "HP: " + player.playerHealth.ToString();
        ballsLeft.text = "Target: " + yellowBalls.Length.ToString();

        if (yellowBalls.Length == 0) {
            ballsLeft.text = "Winner!";
        }
	}
}
