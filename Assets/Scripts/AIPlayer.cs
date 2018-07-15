using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour {

    public List<Player> allPlayers;

    private Dictionary<Player, float> distrust = new Dictionary<Player, float>();

    private Player player;

    private bool debugging = false;

    public bool[] targets = new bool[4];


	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();

        for (int i = 0; i < allPlayers.Count; i++)
        {
            distrust.Add(allPlayers[i], 100.0f);
        }
        distrust[this.player] = 25.0f;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (!this.player.targetSet)
                pickTarget();
        */

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == true)
            {
                this.player.setTarget(allPlayers[i]);
                targets[i] = false;
            }
        }
	}

    /// <summary>
    /// Uses the trust system to pick a target
    /// </summary>
    private void pickTarget()
    {
        float totalTrust = 0.0f;
        for (int i = 0; i < allPlayers.Count; i++)
            totalTrust += distrust[allPlayers[i]];

        float rand = Random.Range(0.0f, totalTrust);

        float currentTrust = 0.0f;
        int j = 0;

        if (debugging)
        {
            int playername = int.Parse(this.gameObject.name) - 1;
            Debug.Log("Player " + playername + "'s distrusts");

            for (int i = 0; i < allPlayers.Count; i++)
            {
                int targetname = int.Parse(allPlayers[i].gameObject.name) - 1;

                Debug.Log(targetname + ": " + distrust[allPlayers[i]]);
            }
        }

        while (j < 4)
        {
            if (rand <= distrust[allPlayers[j]] + currentTrust)
            {
                player.setTarget(allPlayers[j]);

                if (debugging)
                {
                    int playername = int.Parse(this.gameObject.name) - 1;
                    int targetname = int.Parse(allPlayers[j].gameObject.name) - 1;

                    Debug.Log("Player " + playername + " targeted player " + targetname);
                }

                return;
            }
            else
            {
                currentTrust += distrust[allPlayers[j]];
                j++;
            }
        }


    }

    /// <summary>
    /// Called by a player/bot when it aims at this bot. Affects trust.
    /// </summary>
    /// <param name="shooter">The player aiming at this bot</param>
    public void beAimedAt(Player shooter)
    {
        distrust[shooter] += distrust[shooter] / 2;
    }
}
