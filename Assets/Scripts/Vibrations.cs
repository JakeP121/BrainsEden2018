using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations : MonoBehaviour {

    private int playerName;
    private ControllerButtons controller;
    private int nudge;
    private int target;

	// Use this for initialization
	void Start () {
        playerName = int.Parse(this.gameObject.name);
        controller = this.GetComponent<ControllerButtons>();
	}
	
	// Update is called once per frame
	void Update () {
        detect();
        if (selectionMade())
        {
            //pass to gameobject or game controller
        }
    }

    public void detect()
    {
         nudge = controller.dpadValue();
    }

    private bool selectionMade()
    {
        if (nudge != 0 && target != 0)
            return true;
        else
            return false;
    }
}
