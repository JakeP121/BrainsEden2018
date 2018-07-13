using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

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
        switch (target)
        {
            case 1:
                {
                    print("Hit");
                    GamePad.SetVibration(PlayerIndex.One, 1, 1);
                    StartCoroutine(StopVibration(PlayerIndex.One));
                    
                    break;
                }
            case 2:
                {
                    GamePad.SetVibration(PlayerIndex.Two, 1, 1);
                    StartCoroutine(StopVibration(PlayerIndex.Two));
                    break;
                }
            default:
                {
                    break;
                }
        }

    }

    IEnumerator StopVibration (PlayerIndex player)
    {
        print("Waiting");
        yield return new WaitForSeconds(1);
        GamePad.SetVibration(player, 0, 0);

    }

}
