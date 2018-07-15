using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject barrel;
	public Scene main;
	private List<Joycon> joycons;
	private int idx = 0;
	public int jc_ind = 0;


	// Use this for initialization
	void Start () {
		joycons = JoyconManager.Instance.j;

        if (joycons.Count < jc_ind+1){
            Destroy(gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(transform.position, Vector3.up, 0.1f);
		startGame();
		updateCanvas();
		print(idx);
	}

	void startGame()
	{
			Joycon j;
			if(Input.anyKeyDown && idx <= 5)
				idx++;
			
			for (int i = 0; i < joycons.Count; i++)
			{
				j = joycons [i];
				if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.DPAD_UP))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.DPAD_LEFT))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.HOME))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.MINUS))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.PLUS))
					idx++;
				else if (j.GetButtonDown(Joycon.Button.CAPTURE))
					idx++;
			}
	}

	void updateCanvas()
	{
		switch(idx)
		{
			case 1:
			{
				transform.Find("Canvas").Find("Logo").gameObject.SetActive(false);
				transform.Find("Canvas").Find("Text").gameObject.SetActive(false);
				transform.Find("Canvas").Find("1").gameObject.SetActive(true);
				break;
			}
			case 2:
			{
				transform.Find("Canvas").Find("1").gameObject.SetActive(false);
				transform.Find("Canvas").Find("2").gameObject.SetActive(true);
				break;
			}
			case 3:
			{
				transform.Find("Canvas").Find("2").gameObject.SetActive(false);
				transform.Find("Canvas").Find("3").gameObject.SetActive(true);
				break;
			}
			case 4:
			{
				transform.Find("Canvas").Find("3").gameObject.SetActive(false);
				transform.Find("Canvas").Find("4").gameObject.SetActive(true);
				break;
			}
			case 5:
			{
				transform.Find("Canvas").Find("4").gameObject.SetActive(false);
				transform.Find("Canvas").Find("5").gameObject.SetActive(true);
				break;
			}
			case 6:
			{
				SceneManager.LoadScene(1);
				break;
			}
		}
	}
}
