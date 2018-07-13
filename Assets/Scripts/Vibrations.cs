using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations : MonoBehaviour {

    private int playerName;
    private ControllerButtons controller;

	// Use this for initialization
	void Start () {
        playerName = int.Parse(this.gameObject.name);
        controller = this.GetComponent<ControllerButtons>();
	}
	
	// Update is called once per frame
	void Update () {
        nudge(controller.dpadValue());
    }

    void nudge (int target){
        //make something vibrate
    }

}
