using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxControllerHandler : MonoBehaviour {

    private Player player;
    private int playerNum;

    private Dictionary<string, KeyCode> buttons = new Dictionary<string, KeyCode>();

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponent<Player>();
        playerNum = int.Parse(gameObject.name);

        setControls();
	}

    private void setControls()
    {
        switch (playerNum)
        {
            case (1):
                buttons.Add("A", KeyCode.Joystick1Button0);
                buttons.Add("B", KeyCode.Joystick1Button1);
                buttons.Add("X", KeyCode.Joystick1Button2);
                buttons.Add("Y", KeyCode.Joystick1Button3);
                buttons.Add("L_Thumbstick", KeyCode.Joystick1Button8);
                break;
            case (2):
                buttons.Add("A", KeyCode.Joystick2Button0);
                buttons.Add("B", KeyCode.Joystick2Button1);
                buttons.Add("X", KeyCode.Joystick2Button2);
                buttons.Add("Y", KeyCode.Joystick2Button3);
                buttons.Add("L_Thumbstick", KeyCode.Joystick2Button8);
                break;
            case (3):
                buttons.Add("A", KeyCode.Joystick3Button0);
                buttons.Add("B", KeyCode.Joystick3Button1);
                buttons.Add("X", KeyCode.Joystick3Button2);
                buttons.Add("Y", KeyCode.Joystick3Button3);
                buttons.Add("L_Thumbstick", KeyCode.Joystick3Button8);
                break;
            case (4):
                buttons.Add("A", KeyCode.Joystick4Button0);
                buttons.Add("B", KeyCode.Joystick4Button1);
                buttons.Add("X", KeyCode.Joystick4Button2);
                buttons.Add("Y", KeyCode.Joystick4Button3);
                buttons.Add("L_Thumbstick", KeyCode.Joystick4Button8);
                break;
            default:
                Debug.LogError("Invalid player num: " + playerNum);
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        getInput();
	}

    void getInput()
    {  
        if (Input.GetKeyDown(buttons["A"]))
            Debug.Log(playerNum + "A");

        if (Input.GetKeyDown(buttons["B"]))
            Debug.Log(playerNum + "B");

        if (Input.GetKeyDown(buttons["X"]))
            Debug.Log(playerNum + "X");

        if (Input.GetKeyDown(buttons["Y"]))
            Debug.Log(playerNum + "Y");

        if (Input.GetKeyDown(buttons["L_Thumbstick"]))
            StartCoroutine(whoAmI());
    }

    IEnumerator whoAmI()
    {
        for (int i = 0; i < playerNum; i++)
        {
            this.transform.Find("Spot Light").gameObject.SetActive(true);
            // rumble
            yield return new WaitForSeconds(0.20f);
            this.transform.Find("Spot Light").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
