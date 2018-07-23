using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Player : MonoBehaviour
{

    public bool targetSet = false;
    public bool isAlive = true;

    public int points = 0;
    public int pot = 0;

    private Player target;

    private bool debugging = false;

    /// <summary>
    /// Plays animation to show who the player aimed at
    /// </summary>
    public void draw()
    {
        // Not implemented
    }

    /// <summary>
    /// The player shoots their banana
    /// </summary>
    /// <param name="loaded">Should the bullet be blank?</param>
    public void shoot(bool loaded)
    {
        GetComponent<AnimController>().AftershotStance(target.gameObject);

        if (!loaded)
        {

            if (debugging)
            {
                int playerName = int.Parse(this.gameObject.name);
                Debug.Log("Player " + playerName + " was unsuccessful");
            }

        }
        else
        {
            if (gameObject == target.gameObject)
                GetComponent<AnimController>().DeathStance();

            if (debugging)
            {
                int playerName = int.Parse(this.gameObject.name);
                Debug.Log("Player " + playerName + " was successful");
            }

            
            target.die();
        }

        if (target.GetComponent<InputHandler>() && target.GetComponent<InputHandler>().useAI)
            target.GetComponent<AIPlayer>().beAimedAt(this); // Show AI that this player tried to shoot them.

        target = null;
        targetSet = false;
    }

    public void die()
    {
        if (!isAlive)
            return;

        isAlive = false;

        if (debugging)
        {
            int playerName = int.Parse(this.gameObject.name) - 1;

            Debug.Log("Player " + playerName + " died");
        }

    }


    /// <summary>
    /// Resets the player (to be used at start of the round)
    /// </summary>
    public void reset()
    {
        isAlive = true;
        targetSet = false;
        target = null;

        GetComponent<AnimController>().NormalStance();
        transform.Find("Banana").GetComponent<MeshRenderer>().enabled = false;
        pot = 0;
    }

    /// <summary>
    /// Gets the button input to set a player target
    /// </summary>
    public void setTarget(Player targetedPlayer)
    {
        // button input to decide target
        target = targetedPlayer;
        targetSet = true;
        transform.Find("Banana").GetComponent<MeshRenderer>().enabled = true;
    }
}
