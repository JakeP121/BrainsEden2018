using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawnTest : MonoBehaviour {

    public GameObject splash_Win;

    private void Awake()
    {
        GameObject tempSplash = Instantiate(splash_Win);
        tempSplash.GetComponent<SplashScript>().SetWinner("Player 3 wins!");
        GameObject canvas = GameObject.Find("Canvas");
        tempSplash.transform.SetParent(canvas.transform, false);
    }

}
