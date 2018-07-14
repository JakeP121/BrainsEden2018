using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour {

    public Text text;

	public void SetWinner(string name)
    {
        text.text = name;
    }

    private void Awake()
    {
        Destroy(this.gameObject, 4);
    }

}
