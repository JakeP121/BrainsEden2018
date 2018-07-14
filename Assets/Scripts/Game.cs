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

    public bool showingScore = false;

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
        for (int i = 0; i < players.Count; i++)
            players[i].draw();

        StartCoroutine(wait(secondsBeforeFiring));

        // Roll for duds
        for (int i = 0; i < players.Count; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);

            if (rand <= gunAccuracy)
                players[i].shoot(true);
            else
                players[i].shoot(false);
        }

        List<Player> livingPlayers = getLivingPlayers();

        for (int i = 0; i < livingPlayers.Count; i++)
        {
            int potDifference = Random.Range(-(pot / 100), pot / 100);

            livingPlayers[i].points += (pot / livingPlayers.Count) + potDifference;
            livingPlayers[i].pot = (pot / livingPlayers.Count) + potDifference;
        }

        if (debugging)
        {
            Debug.Log("Round " + round + " over!");
            logCurrentLeaderboard();
        }

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
                tempSplash.GetComponent<ScoreScreen>().displayResults(int.Parse(livingPlayers[0].transform.name), livingPlayers[0].GetComponent<Player>().pot);
                break;
            case 2:
                tempSplash.GetComponent<ScoreScreen>().displayResults(int.Parse(livingPlayers[0].transform.name), livingPlayers[0].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[1].transform.name), livingPlayers[1].GetComponent<Player>().pot);
                break;
            case 3:
                tempSplash.GetComponent<ScoreScreen>().displayResults(int.Parse(livingPlayers[0].transform.name), livingPlayers[0].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[1].transform.name), livingPlayers[1].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[2].transform.name), livingPlayers[2].GetComponent<Player>().pot);
                break;
            case 4:
                tempSplash.GetComponent<ScoreScreen>().displayResults(int.Parse(livingPlayers[0].transform.name), livingPlayers[0].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[1].transform.name), livingPlayers[1].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[2].transform.name), livingPlayers[2].GetComponent<Player>().pot,
                    int.Parse(livingPlayers[3].transform.name), livingPlayers[3].GetComponent<Player>().pot);
                break;


        }

        tempSplash.GetComponent<ScoreScreen>().gameController = this;

        round++;
 //       wait(4);
        roundStarted = false;
        showingScore = true;
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
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].targetSet == false)
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
