using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.yellow;
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0.0f, 0.0f, 2.0f);
	}
}
