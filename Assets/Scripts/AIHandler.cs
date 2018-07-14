using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {

    public bool useAI = false; // Should this player be controlled by AI or the joycon?

	// Use this for initialization
	void Start () {
        if (useAI)
            GetComponent<AIPlayer>().enabled = true;
        else
            GetComponent<Vibrations>().enabled = true;
    }
}
