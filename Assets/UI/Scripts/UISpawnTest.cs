using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawnTest : MonoBehaviour {

    public GameObject splash_Win;
    private string split_01 = "Player 1";
    private string split_02 = "Player 56";

    private void Awake()
    {
        GameObject tempSplash = Instantiate(splash_Win);
        //tempSplash.GetComponent<SplashScript>().SetWinner("Player 3 wins!");
        tempSplash.GetComponent<SplashScript>().SetWinner( split_01 + "\n             " + split_02);
        GameObject canvas = GameObject.Find("Canvas");
        tempSplash.transform.SetParent(canvas.transform, false);
    }

}
