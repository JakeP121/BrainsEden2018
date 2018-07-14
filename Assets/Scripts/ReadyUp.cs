using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUp : MonoBehaviour {

    public Sprite unconfirmed;
    public Sprite confirmed;

    //private void Awake()
    //{
    //    Confirm();
    //}

    public void Confirm()
    {
        transform.GetComponent<Image>().sprite = confirmed;
    }
}
