using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

    private Text text_01;
    private Text text_02;
    private Text text_03;
    private Text text_04;

    private Text text_05;
    private Text text_06;
    private Text text_07;
    private Text text_08;

    public GameObject panel_01;
    public GameObject panel_02;
    public GameObject panel_03;
    public GameObject panel_04;

    private GameObject banana_01;
    private GameObject banana_02;
    private GameObject banana_03;
    private GameObject banana_04;

    public Game gameController;

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

        text_05 = panel_01.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        text_06 = panel_02.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        text_07 = panel_03.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        text_08 = panel_04.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();

        //   displayResults(0, 48, 2, 52);

        Destroy(this.gameObject, 4);
    }

    public void displayResults(int first_player = -1, int first_score = 0, int second_player = -1, int second_score = 0,
                                int third_player = -1, int third_score = 0, int fourth_player = -1, int fourth_score = 0)
    {
        Debug.Log("Displaying results!");
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

        switch (third_player)
        {
            default:
                break;
            case 0:
                banana_01.GetComponent<Image>().enabled = true;
                text_01.enabled = true;
                text_01.text = "+" + third_score.ToString();
                break;
            case 1:
                banana_02.GetComponent<Image>().enabled = true;
                text_02.enabled = true;
                text_02.text = "+" + third_score.ToString();
                break;
            case 2:
                banana_03.GetComponent<Image>().enabled = true;
                text_03.enabled = true;
                text_03.text = "+" + third_score.ToString();
                break;
            case 3:
                banana_04.GetComponent<Image>().enabled = true;
                text_04.enabled = true;
                text_04.text = "+" + third_score.ToString();
                break;
        }

        switch (fourth_player)
        {
            default:
                break;
            case 0:
                banana_01.GetComponent<Image>().enabled = true;
                text_01.enabled = true;
                text_01.text = "+" + fourth_score.ToString();
                break;
            case 1:
                banana_02.GetComponent<Image>().enabled = true;
                text_02.enabled = true;
                text_02.text = "+" + fourth_score.ToString();
                break;
            case 2:
                banana_03.GetComponent<Image>().enabled = true;
                text_03.enabled = true;
                text_03.text = "+" + fourth_score.ToString();
                break;
            case 3:
                banana_04.GetComponent<Image>().enabled = true;
                text_04.enabled = true;
                text_04.text = "+" + fourth_score.ToString();
                break;
        }

    }

    public void displayTotals(int p1, int p2, int p3, int p4)
    {
        text_05.text = p1.ToString();
        text_06.text = p2.ToString();
        text_07.text = p3.ToString();
        text_08.text = p4.ToString();
    }

    private void OnDestroy()
    {
        //       gameController.roundStarted = false;
        gameController.startNextRound();
        Debug.Log("Score screen destroyed.");
    }

}
