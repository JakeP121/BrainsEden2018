using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtons : MonoBehaviour {

    private bool up, down, left, right;

    public int dpadValue()
    {
        if (DpadUp())
            return 1;
        if (DpadRight())
            return 2;
        if (DpadDown())
            return 3;
        if (DpadLeft())
            return 4;
        return 0;
    }

    bool DpadUp()
    {
        if (Input.GetAxisRaw("DPadVertical") == 1 && !up)
        {
            StartCoroutine(resetUpBool(0.5f));
            up = true;
            return true;
        }
        return false;
    }
    bool DpadDown()
    {
        if (Input.GetAxisRaw("DPadVertical") == -1 && !down)
        {
            StartCoroutine(resetDownBool(0.5f));
            down = true;
            return true;
        }
        return false;
    }
    bool DpadLeft()
    {
        if (Input.GetAxisRaw("DPadHorizontal") == -1 && !left)
        {
            StartCoroutine(resetLeftBool(0.5f));
            left = true;
            return true;
        }
        return false;
    }
    bool DpadRight()
    {
        if (Input.GetAxisRaw("DPadHorizontal") == 1 && !right)
        {
            StartCoroutine(resetRightBool(0.5f));
            right = true;
            return true;
        }
        return false;
    }

    IEnumerator resetLeftBool(float seconds)
    {
        float ResumeTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < ResumeTime)
        {
            yield return null;
        }
        left = false;
    }
    IEnumerator resetRightBool(float seconds)
    {
        float ResumeTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < ResumeTime)
        {
            yield return null;
        }
        right = false;
    }
    IEnumerator resetUpBool(float seconds)
    {
        float ResumeTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < ResumeTime)
        {
            yield return null;
        }
        up = false;
    }
    IEnumerator resetDownBool(float seconds)
    {
        float ResumeTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < ResumeTime)
        {
            yield return null;
        }
        down = false;
    }
}
