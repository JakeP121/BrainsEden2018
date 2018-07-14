using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibrations : MonoBehaviour {

    private int playerName;

	// Use this for initialization
	void Start () {
        playerName = int.Parse(this.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
        nudge(controller.dpadValue());
        for (KeyCode i = 0; i <= KeyCode.Joystick8Button19; i++)
{
    if (Input.GetKey(i))
        Debug.Log(i);
}
    if (Input.GetAxisRaw("JoyConVertical") != 0)
        print(Input.GetAxisRaw("JoyConVertical"));

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
