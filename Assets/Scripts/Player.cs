using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// <param name="blank">Should the bullet be blank?</param>
    public void shoot(bool blank)
    {
        if (blank == true)
        {
            //successful shoot animation start

            if (debugging)
            {
                int playerName = int.Parse(this.gameObject.name) - 1;
                Debug.Log("Player " + playerName + " was successful");
            }

            target.die();
        }
        else
        {
            // failed shoot animation start

            if (debugging)
            {
                int playerName = int.Parse(this.gameObject.name) - 1;
                Debug.Log("Player " + playerName + " was unsuccessful");
            }
        }

        if (target.GetComponent<AIHandler>() && target.GetComponent<AIHandler>().useAI)
            target.GetComponent<AIPlayer>().beAimedAt(this); // Show AI that this player tried to shoot them.

        target = null;
        targetSet = false;
    }

    public void die()
    {
        // Not implemented

        if (!isAlive)
            return;

        isAlive = false;

        if (debugging)
        {
            int playerName = int.Parse(this.gameObject.name) - 1;

            Debug.Log("Player " + playerName + " died");
        }

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }


    /// <summary>
    /// Resets the player (to be used at start of the round)
    /// </summary>
    public void reset()
    {
        isAlive = true;
        targetSet = false;
        target = null;
    }

    /// <summary>
    /// Gets the button input to set a player target
    /// </summary>
    public void setTarget(Player targetedPlayer)
    {
        // button input to decide target
        target = targetedPlayer;
        targetSet = true;
    }
}
