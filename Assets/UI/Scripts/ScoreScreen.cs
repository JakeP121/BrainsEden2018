using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

    private Text text_01;
    private Text text_02;
    private Text text_03;
    private Text text_04;

    public GameObject panel_01;
    public GameObject panel_02;
    public GameObject panel_03;
    public GameObject panel_04;

    private GameObject banana_01;
    private GameObject banana_02;
    private GameObject banana_03;
    private GameObject banana_04;

    int[] winningPlayers = new int[] { 0, 0, 0, 0 };

    private void Awake()
    {
        //testBanan = this.gameObject.transform.GetChild(0).gameObject;
        banana_01 = panel_01.gameObject.transform.GetChild(2).gameObject;
        banana_02 = panel_02.gameObject.transform.GetChild(2).gameObject;
        banana_03 = panel_03.gameObject.transform.GetChild(2).gameObject;
        banana_04 = panel_04.gameObject.transform.GetChild(2).gameObject;

        text_01 = panel_01.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        text_02 = panel_02.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        text_03 = panel_03.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        text_04 = panel_04.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();

        displayResults(0, 48, 2, 52);
    }

    public void displayResults(int first_player = -1, int first_score = 0, int second_player = -1, int second_score = 0)
    {
        switch(first_player)
        {
        default:
            break;
        case 0:
            banana_01.GetComponent<Image>().enabled = true;
                text_01.enabled = true;
                text_01.text = "+" + first_score.ToString();
            break;
        case 1:
            banana_02.GetComponent<Image>().enabled = true;
                text_02.enabled = true;
                text_02.text = "+" + first_score.ToString();
                break;
        case 2:
            banana_03.GetComponent<Image>().enabled = true;
                text_03.enabled = true;
                text_03.text = "+" + first_score.ToString();
                break;
        case 3:
            banana_04.GetComponent<Image>().enabled = true;
                text_04.enabled = true;
                text_04.text = "+" + first_score.ToString();
                break;
        }

        switch (second_player)
        {
            default:
                break;
            case 0:
                banana_01.GetComponent<Image>().enabled = true;
                text_01.enabled = true;
                text_01.text = "+" + second_score.ToString();
                break;
            case 1:
                banana_02.GetComponent<Image>().enabled = true;
                text_02.enabled = true;
                text_02.text = "+" + second_score.ToString();
                break;
            case 2:
                banana_03.GetComponent<Image>().enabled = true;
                text_03.enabled = true;
                text_03.text = "+" + second_score.ToString();
                break;
            case 3:
                banana_04.GetComponent<Image>().enabled = true;
                text_04.enabled = true;
                text_04.text = "+" + second_score.ToString();
                break;
        }
    }

}
