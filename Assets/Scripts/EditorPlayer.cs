using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayer : MonoBehaviour {

    public bool[] targets = new bool[4]; // The targets to choose from

    private Player player; // This player
    private List<Player> allPlayers = new List<Player>(); // All players in scene (including this)

    // Use this for initialization
    void Start () {
        for (int i = 1; i <= 4; i++)
            allPlayers.Add(GameObject.Find(i.ToString()).GetComponent<Player>());

        this.player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == true)
            {
                this.player.setTarget(allPlayers[i]);
                targets[i] = false;
            }
        }
    }
}
