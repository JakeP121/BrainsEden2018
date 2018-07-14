using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool targetSet = false;
    public bool isAlive = true;

    public int points = 0;

    private Player target;

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
        // Not implemented

        if (blank == true)
        {
            //successful shoot animation start
            target.die();
        }
        else
            // failed shoot animation start

        target = null;
        targetSet = false;
    }

    public void die()
    {
        // Not implemented

        isAlive = false;
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
    private void setTarget()
    {
        // button input to decide target
        targetSet = true;
    }

    
}
