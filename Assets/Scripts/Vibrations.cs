using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrations : MonoBehaviour {

    private int playerName;
    private List<Joycon> joycons;
    private Player player;
    public int jc_ind = 0;

    private Joycon current;

    private bool playerActive = true;

	// Use this for initialization
	void Start () {
        joycons = JoyconManager.Instance.j;
        player = this.GetComponent<Player>();

        if (joycons.Count < jc_ind+1){
            Destroy(gameObject);
		}
        playerName = int.Parse(this.gameObject.name) - 1;
        current = joycons[playerName];
	}
	
	// Update is called once per frame
	void Update () {
        Nudge(SelectNudge());
        Target();
        WhoAmI();
    }

    void WhoAmI()
    {
        if(current.GetButtonDown(Joycon.Button.MINUS) || current.GetButtonDown(Joycon.Button.PLUS))
        {
            print("Player" + (playerName + 1));
            StartCoroutine(slowVibrations());
        }
    }

    IEnumerator slowVibrations ()
    {
        for(int i = 0; i < playerName + 1; i++)
        {
            current.SetRumble(160, 160, 0.6f, 100);
            yield return new WaitForSeconds(0.35f); 
            print(i);
        }

    }

    void Target()
    {
        int playerToShoot = 0;

        //Different statements for each type of joycon
        if (!current.isLeft)
        {
            if(current.GetButtonDown(Joycon.Button.DPAD_LEFT))
				playerToShoot = 1;
            if(current.GetButtonDown(Joycon.Button.DPAD_UP))
				playerToShoot = 2;
            if(current.GetButtonDown(Joycon.Button.DPAD_RIGHT))
				playerToShoot = 3;
            if(current.GetButtonDown(Joycon.Button.DPAD_DOWN))
				playerToShoot = 4;
        }

        else
        {
            if(current.GetButtonDown(Joycon.Button.DPAD_RIGHT))
				playerToShoot = 1; 
            if(current.GetButtonDown(Joycon.Button.DPAD_DOWN))
				playerToShoot = 2;
            if(current.GetButtonDown(Joycon.Button.DPAD_LEFT))
				playerToShoot = 3;
            if(current.GetButtonDown(Joycon.Button.DPAD_UP))
				playerToShoot = 4;
        }

        if (playerToShoot != 0)
        {
            player.setTarget(GameObject.Find(playerToShoot.ToString()).GetComponent<Player>());
            player.targetSet = true;
            print("you shot" + playerToShoot.ToString());
        }

    }

    int SelectNudge()
    {
        Vector2 stickSelection;

        //Gets the floats for the stick axis
        float [] input = current.GetStick();

        //Moves axis data into a vec2
        stickSelection.y = input[0];
        stickSelection.x = input[1];

        //Calculates which direction the joystick is going
        //and returns a number to represent direction
        if (!current.isLeft)
        {
            if(stickSelection.y > 0.5) //Player 3
                return 3;
            if(stickSelection.y < -0.5) //Player 1
                return 1;
            if(stickSelection.x > 0.5) //Player 2
                return 2;    
            if(stickSelection.x < -0.5) //Player 4
                return 4;
        }
        else
        {
            if(stickSelection.y > 0.5) //Player 1
                return 1;
            if(stickSelection.y < -0.5) //Player 4
                return 4;
            if(stickSelection.x > 0.5) //Player 2
                return 3;    
            if(stickSelection.x < -0.5) //Player 4
                return 2;
        }

        
        return 0;
    }

    void Nudge (int nudgePlayer){

        //Switch to rumble the selected controller
        switch (nudgePlayer)
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
