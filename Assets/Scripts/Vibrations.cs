using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibrations : MonoBehaviour {

    private int playerName = 0;
    private List<Joycon> joycons;

    public int jc_ind = 0;

	// Use this for initialization
	void Start () {
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
        nudge(SelectNudge());
    }

    int SelectNudge()
    {
        Vector2 stickSelection;
        Joycon current = joycons[playerName];
        int selection = 0;

        float [] input = current.GetStick();

        stickSelection.y = input[0];
        stickSelection.x = input[1];

        if(stickSelection.y > 0.5)
            return 1;
        if(stickSelection.y < -0.5)
            return 2;
        if(stickSelection.x > 0.5)
            return 3;    
        if(stickSelection.x < -0.5)
            return 4;
        
        return 0;

    }

    void nudge (int target){
        switch (target)
        {
            case 1:
                {
                    joycons[0].SetRumble(160, 320, 0.6f, 200);
                    break;
                }
            case 2:
                {
                    joycons[1].SetRumble(160, 320, 0.6f, 200);
                    break;
                }
            case 3:
                {
                    joycons[2].SetRumble(160, 320, 0.6f, 200);
                    break;
                }
            case 4:
                {
                    joycons[3].SetRumble(160, 320, 0.6f, 200);
                    break;
                }
            default:
                {
                    break;
                }
        }

    }
}
