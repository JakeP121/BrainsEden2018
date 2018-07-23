using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Game : MonoBehaviour {

    public List<Player> players = new List<Player>(); // All players in the game

    public int totalRounds = 5; // Rounds to play before the game ends.
    public int round = 1; // The current round.

    public float gunAccuracy = 0.5f; // 1 = 100% hit rate, 0 = 0% hit rate

    public int pot = 500; // The prize for winning a round

    public float secondsBeforeFiring = 3.0f; // The number of seconds between the players drawing and firing

    private bool debugging = false;

    public GameObject splash_Score;
    public GameObject splash_Win;
    public GameObject splash_Knockout;
    public GameObject splash_Split;

    private bool[] hasDied = new bool[4];

    public enum State { ROUND_STARTING, WAITING_FOR_INPUT, FIRING, PURGATORY, DYING, SHOWING_SCORE, ROUND_ENDED, GAMEOVER };
    public State gameState;

    public float purgatorySeconds = 3.0f;
    private float purgatoryTimer = 0.0f;

    public bool showingScore = false;
    public bool showingSplash = false;

    public bool roundStarted;

    List<Player> livingPlayers = new List<Player>(4);

    public AudioClip bang;

    private void LateUpdate()
    {
        if (gameState == State.GAMEOVER)
            return;

        if (round > totalRounds)
        {
            showEndScreen();
            gameState = State.GAMEOVER;
        }

        if (gameState == State.ROUND_STARTING)
            startRound();

        if (gameState == State.WAITING_FOR_INPUT && playerTargetsSet())
            gameState = State.FIRING;

        if (gameState == State.PURGATORY)
        {
            if (purgatoryTimer > purgatorySeconds)
            {
                gameState = State.DYING;
                monkeysDie();
                purgatoryTimer = 0.0f;
            }
            else
                purgatoryTimer += Time.deltaTime;
        }

        if (gameState == State.FIRING)
            monkeysFire();

    }

    private void startRound()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].reset();
        }
            //        livingPlayers.Clear();
        //        livingPlayers.Clear();
        livingPlayers = new List<Player>();
        gameState = State.WAITING_FOR_INPUT;
    }

    /// <summary>
    /// Plays a round
    /// </summary>
    private void monkeysFire()
    {
        Debug.Log("Start round!");

        livingPlayers = getLivingPlayers();

        for (int i = 0; i < livingPlayers.Count; i++)
            players[i].draw();

        // Roll for duds
        for (int i = 0; i < livingPlayers.Count; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);

            if (rand <= gunAccuracy)
                livingPlayers[i].shoot(true);
            else
                livingPlayers[i].shoot(false);
        }

        gameState = State.PURGATORY;
    }

    private void monkeysDie()
    {
        Debug.Log("All shots fired!");
        List<Player> livingPlayers = getLivingPlayers();
        GameObject SFX = GameObject.Find("SFX").gameObject;
        SFX.GetComponent<AudioSource>().PlayOneShot(bang, 1);

        foreach (Player p in players)
        {
            if (!livingPlayers.Contains(p))
            {
                if (p.GetComponent<AnimController>().currentStance != "Death")
                    p.GetComponent<AnimController>().DeathStance();
            }
            else
            {
                if (livingPlayers.Count <= 2)
                    p.GetComponent<AnimController>().Dance();
                else
                    p.GetComponent<AnimController>().NormalStance();
            }
        }

        // New 1-2 winners (3-4 surviving replays)
        if (livingPlayers.Count == 1)
        {
            livingPlayers[0].points += pot;
            livingPlayers[0].pot = pot;
 //           wait(int.Parse(livingPlayers[0].transform.name));
            showSplash(int.Parse(livingPlayers[0].transform.name));
        }
        else if (livingPlayers.Count == 2)
        {
            int potDifference = Random.Range(-(pot / 100), pot / 100);

            livingPlayers[0].points += (pot / 2) + potDifference;
            livingPlayers[0].pot = (pot / 2) + potDifference;

            livingPlayers[1].points += (pot / 2) - potDifference;
            livingPlayers[1].pot = (pot / 2) - potDifference;
//            wait(int.Parse(livingPlayers[0].transform.name), int.Parse(livingPlayers[1].transform.name));
            showSplash(int.Parse(livingPlayers[0].transform.name), int.Parse(livingPlayers[1].transform.name));
        }
        else if (livingPlayers.Count >= 3)
        {
            for (int i = 0; i < livingPlayers.Count; i++)
                livingPlayers[i].reset();

            gameState = State.WAITING_FOR_INPUT;
            return;
        }
        else
        {
 //           wait();
            showSplash();
        }


        if (debugging)
        {
            Debug.Log("Round " + round + " over!");
            logCurrentLeaderboard();
        }
     
  //      round++;
 //       wait(4);
        roundStarted = false;
    }
    IEnumerator wait (int p1 = 0, int p2 = 0)
    {

        yield return new WaitForSeconds(5);
        showSplash(p1, p2);
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
        

        round++;
        //       wait(4);
        gameState = State.SHOWING_SCORE;
    }

    public void startNextRound()
    {
        gameState = State.ROUND_STARTING;
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
