using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public List<Player> players = new List<Player>(); // All players in the game

    public int totalRounds = 5; // Rounds to play before the game ends.
    public int round = 1; // The current round.

    public float gunAccuracy = 0.5f; // 1 = 100% hit rate, 0 = 0% hit rate

    public int pot = 500; // The prize for winning a round

    public float secondsBeforeFiring = 3.0f; // The number of seconds between the players drawing and firing

    public bool roundStarted = false; // Has a current round started? 
    private bool waitingForInput = false; // Is the round waiting for player input? 

    private bool gameOver = false;

    private bool debugging = false;

    public GameObject splash_Score;
    public GameObject splash_Win;
    public GameObject splash_Knockout;
    public GameObject splash_Split;

    public bool showingScore = false;
    public bool showingSplash = false;

    private bool[] hasDied = new bool[4];

    List<Player> livingPlayers;

    private void LateUpdate()
    {
        if (gameOver)
            return;

        if (round > totalRounds)
        {
            showEndScreen();
            gameOver = true;
        }

        if (!roundStarted && !showingScore)
            startRound();

        if (playerTargetsSet())
            waitingForInput = false;

        if (roundStarted && !waitingForInput)
            playRound();

        //if (!showingSplash && !waitingForInput && !showingScore && !roundStarted)
        //{
        //    showScores(livingPlayers);
        //}
    }

    private void startRound()
    {
        for (int i = 0; i < players.Count; i++)
            players[i].reset();

        roundStarted = true;
        waitingForInput = true;
    }

    /// <summary>
    /// Plays a round
    /// </summary>
    private void playRound()
    {
        livingPlayers = getLivingPlayers();

        for (int i = 0; i < livingPlayers.Count; i++)
            players[i].draw();

        StartCoroutine(wait(secondsBeforeFiring));

        // Roll for duds
        for (int i = 0; i < livingPlayers.Count; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);

            if (rand <= gunAccuracy)
                livingPlayers[i].shoot(true);
            else
                livingPlayers[i].shoot(false);
        }

        livingPlayers = getLivingPlayers();

        // OG 1-3 winners
        /*
        for (int i = 0; i < livingPlayers.Count; i++)
        {
            int potDifference = Random.Range(-(pot / 100), pot / 100);

            livingPlayers[i].points += (pot / livingPlayers.Count) + potDifference;
            livingPlayers[i].pot = (pot / livingPlayers.Count) + potDifference;
        }
        */

        // New 1-2 winners (3-4 surviving replays)
        if (livingPlayers.Count == 1)
        {
            livingPlayers[0].points += pot;
            livingPlayers[0].pot = pot;
            showSplash(int.Parse(livingPlayers[0].transform.name));
        }
        else if (livingPlayers.Count == 2)
        {
            int potDifference = Random.Range(-(pot / 100), pot / 100);

            livingPlayers[0].points += (pot / 2) + potDifference;
            livingPlayers[0].pot = (pot / 2) + potDifference;
            livingPlayers[1].points += (pot / 2) - potDifference;
            livingPlayers[1].pot = (pot / 2) - potDifference;
            showSplash(int.Parse(livingPlayers[0].transform.name), int.Parse(livingPlayers[1].transform.name));
        }
        else if (livingPlayers.Count >= 3)
        {
            for (int i = 0; i < livingPlayers.Count; i++)
                livingPlayers[i].reset();

            waitingForInput = true;
            return;
        }
        else
        {
            showSplash();
        }


        if (debugging)
        {
            Debug.Log("Round " + round + " over!");
            logCurrentLeaderboard();
        }

        round++;
 //       wait(4);
        roundStarted = false;

        //showScores(livingPlayers);

    }

    private void showSplash(int p1 = 0, int p2 = 0)
    {
        showingSplash = true;
        showingScore = true;
        if(p2 == 0)
        {
                if (p1 != 0)
                {
                    GameObject tempSplash = Instantiate(splash_Win);
                    tempSplash.GetComponent<SplashScript>().SetWinner("Player " + p1 + " wins!");
                    tempSplash.GetComponent<SplashScript>().gameController = this;
                    GameObject canvas = GameObject.Find("Canvas");
                    tempSplash.transform.SetParent(canvas.transform, false);
                }
                else
                {
                    GameObject tempSplash = Instantiate(splash_Knockout);
                    GameObject canvas = GameObject.Find("Canvas");
                tempSplash.GetComponent<SplashScript>().gameController = this;
                tempSplash.transform.SetParent(canvas.transform, false);
                }
        }
        else
        {
            GameObject tempSplash = Instantiate(splash_Split);
            GameObject canvas = GameObject.Find("Canvas");
            tempSplash.GetComponent<SplashScript>().SetWinner("Player " + p1 + "\n             " + "Player " + p2);
            tempSplash.GetComponent<SplashScript>().gameController = this;
            tempSplash.transform.SetParent(canvas.transform, false);
        }
    }

    public void showScores()
    {
        showingScore = true;

        GameObject tempSplash = Instantiate(splash_Score);
        GameObject canvas = GameObject.Find("Canvas");
        tempSplash.transform.SetParent(canvas.transform, false);
        //       Debug.Log("Living players = " + livingPlayers.Count);
        switch (livingPlayers.Count)
        {
            case 0:
                tempSplash.GetComponent<ScoreScreen>().displayResults();
                break;
            case 1:
                tempSplash.GetComponent<ScoreScreen>().displayResults((int.Parse(livingPlayers[0].transform.name)) - 1, livingPlayers[0].GetComponent<Player>().pot);
                break;
            case 2:
                tempSplash.GetComponent<ScoreScreen>().displayResults((int.Parse(livingPlayers[0].transform.name)) - 1, livingPlayers[0].GetComponent<Player>().pot,
                    int.Parse((livingPlayers[1].transform.name)) - 1, livingPlayers[1].GetComponent<Player>().pot);
                break;
            case 3:
                tempSplash.GetComponent<ScoreScreen>().displayResults((int.Parse(livingPlayers[0].transform.name)) - 1, livingPlayers[0].GetComponent<Player>().pot,
                    int.Parse((livingPlayers[1].transform.name)) - 1, livingPlayers[1].GetComponent<Player>().pot,
                    int.Parse((livingPlayers[2].transform.name)) - 1, livingPlayers[2].GetComponent<Player>().pot);
                break;
            case 4:
                tempSplash.GetComponent<ScoreScreen>().displayResults((int.Parse(livingPlayers[0].transform.name)) - 1, livingPlayers[0].GetComponent<Player>().pot,
                    (int.Parse(livingPlayers[1].transform.name)) - 1, livingPlayers[1].GetComponent<Player>().pot,
                    (int.Parse(livingPlayers[2].transform.name)) - 1, livingPlayers[2].GetComponent<Player>().pot,
                    (int.Parse(livingPlayers[3].transform.name)) - 1, livingPlayers[3].GetComponent<Player>().pot);
                break;


        }

        tempSplash.GetComponent<ScoreScreen>().gameController = this;
        tempSplash.GetComponent<ScoreScreen>().displayTotals(players[0].points, players[1].points, players[2].points, players[3].points);

    }

    /// <summary>
    /// Waits boi
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    /// <summary>
    /// Checks if all players have confirmed a target
    /// </summary>
    /// <returns>True if all players have a target, else false.</returns>
    private bool playerTargetsSet()
    {
        List<Player> livingPlayers = getLivingPlayers();

        for (int i = 0; i < livingPlayers.Count; i++)
        {
            if (livingPlayers[i].targetSet == false)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets a list of living players.
    /// </summary>
    /// <returns></returns>
    private List<Player> getLivingPlayers()
    {
        List<Player> livingPlayers = new List<Player>();

        // See who's still alive
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].isAlive)
                livingPlayers.Add(players[i]);
        }

        return livingPlayers;
    }

    private void showEndScreen()
    {
        // Calculate sorted leaderboard
        List<Player> leaderboard = players;

        for (int i = 0; i < leaderboard.Count; i++)
        {
            for (int j = 0; j < leaderboard.Count - i - 1; j++)
            {
                if (leaderboard[j].points < leaderboard[j + 1].points)
                {
                    Player swap = leaderboard[j];
                    leaderboard[j] = leaderboard[j + 1];
                    leaderboard[j + 1] = swap;
                }
            }
        }
        
        // Visual shit

    }

    private void logCurrentLeaderboard()
    {
        // Calculate sorted leaderboard
        List<Player> leaderboard = players;

        for (int i = 0; i < leaderboard.Count; i++)
        {
            for (int j = 0; j < leaderboard.Count - i - 1; j++)
            {
                if (leaderboard[j].points < leaderboard[j + 1].points)
                {
                    Player swap = leaderboard[j];
                    leaderboard[j] = leaderboard[j + 1];
                    leaderboard[j + 1] = swap;
                }
            }
        }

        for (int i = 0; i < leaderboard.Count; i++)
        {
            int playerName = int.Parse(leaderboard[i].name);
            Debug.Log("Player " + playerName + ": " + leaderboard[i].points);
        }
    }
}
