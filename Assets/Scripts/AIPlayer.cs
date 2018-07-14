using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour {

    public List<Player> allPlayers;

    private Player player;
    
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        
		if (!player.targetSet)
        {
            int rand = Random.Range(0, allPlayers.Count);

            player.setTarget(allPlayers[rand]);

            int playername = int.Parse(this.gameObject.name) - 1;
            int targetname = int.Parse(allPlayers[rand].gameObject.name) - 1;

            Debug.Log("Player " + playername + " targeted player " + targetname);
        }
        
	}
}
