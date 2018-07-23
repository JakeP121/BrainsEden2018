using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public bool useJoyCon = false; // Joycon control
    public bool useXbox = false; // Xbox control
    public bool useAI = false; // AI decides who to target
    public bool useEditor = false; // Manually pick targets from editor

	// Use this for initialization
	void Start () {

        if (useJoyCon)
            GetComponent<Vibrations>().enabled = true;
        else if (useXbox)
            gameObject.AddComponent<XboxControllerHandler>();
        else if (useAI)
            gameObject.AddComponent<AIPlayer>();
        else if (useEditor)
            gameObject.AddComponent<EditorPlayer>();
        else
            Debug.LogWarning("No control method entered for player " + gameObject.name);
    }
}
