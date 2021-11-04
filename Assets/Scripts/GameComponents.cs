using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameComponents : MonoBehaviour
{
    #region Player
    public class Player
    {
        public string name;
        public string uuid;
        public int score = 0;
        public int avatarID;
        public bool isTurn = false;

        public Player(string id)
        {
            name = "Bob";
            isTurn = false;
            uuid = id;
        }

        public Player(string id, string name)
        {
            new Player(id);
            this.name = name;
        }
    }
    #endregion

    #region Attibutes
    public static bool mePlayable;
    public static bool meGoesFirst;
    public static float timeLimit;
    public static bool timeIsRunning;
    public static bool switchState = false;

    public static int numKeys;
    public static int currentRound = 0;

    public static Player me = new Player("", "Alice");
    public static Player them = new Player("", "Bobby");

    public Text roundsText;
    public Text numKeysText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (meGoesFirst) StartCoroutine(PlayFirst());
        else StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        roundsText.text = $"Round {currentRound}/{GameProperties.numRounds}";
        numKeysText.text = $"Keys Left: {numKeys}";
    }

    private void FixedUpdate()
    {
        if (currentRound > GameProperties.numRounds)
        {
            Debug.Log("[GameComp] Game Ends");
            Debug.Log($"Scores: {me.name} = {me.score} vs {them.name} = {them.score}");
            EndMyTurn();
        }
    }

    #region Game Managers
    public static void StartGame()
    {
        if (meGoesFirst)
        {
            me.isTurn = true;
            mePlayable = true;
        }

        Debug.Log($"[GameComp] The game has started. Players: {me.name} vs {them.name}");
        Debug.Log($"[GameComp] Number of rounds: {GameProperties.numRounds}");
        Debug.Log($"[GameComp] Difficulty: {(GameProperties.isHardMode ? "Hard" : "Easy")}");
        Debug.Log($"[GameComp] Playable: {mePlayable}");
    }

    public static void EndGame()
    {
        numKeys = 0;
        me.isTurn = false;
        mePlayable = false;
        SceneManager.LoadScene("Results");
    }
    #endregion

    #region Turn/Round Managers
    IEnumerator PlayFirst()
    {
        NewRound();
        StartMyTurn();

        while (timeIsRunning && mePlayable)
        {
            if (timeLimit > 0) timeLimit -= Time.deltaTime;
            else
            {
                EndMyTurn();
                Debug.Log("[Time] Time's Up");
            }
            if (numKeys == 0) EndMyTurn();
            yield return null;
        }
        GameSocketIO.EmitEndSequence();
        Debug.Log("[PlayFirst] Done");
        StartCoroutine(Wait());
    }

    IEnumerator PlayLater()
    {
        StartMyTurn();

        while (timeIsRunning && mePlayable)
        {
            if (timeLimit > 0) timeLimit -= Time.deltaTime;
            else
            {
                EndMyTurn();
                Debug.Log("[Time] Time's Up");
            }
            if (numKeys == 0) EndMyTurn();
            yield return null;
        }

        Debug.Log("[PlayLater] Done");
        int score = NotesReceiver.CalculateScore();
        them.score += score;
        GameSocketIO.EmitScore(score);

        NewRound();
        if(currentRound < GameProperties.numRounds)
        {
            StartCoroutine(PlayFirst());
        }
        else
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(SwitchingState);
        switchState = false;

        if (meGoesFirst)
        {
            NewRound();
            if (currentRound < GameProperties.numRounds)
            {
                StartCoroutine(Wait());
            }
            // need to check
        }
        else
        {
            StartCoroutine(PlayLater());
        }
    }

    private bool SwitchingState()
    {
        return switchState;
    }

    public static void NewRound()
    {
        ++currentRound;
        Debug.Log($"[GameComp] Current Round: {currentRound}/{GameProperties.numRounds}");
        UpdateNumKeys();
        Debug.Log($"[GameComp] Playable Keys: {numKeys}");
        NotesReceiver.ResetSequences();
        meGoesFirst = !meGoesFirst;
    }

    // Make client playable
    public void StartMyTurn()
    {
        mePlayable = true;
        NewTimer();
    }

    private static void EndMyTurn()
    {
        timeIsRunning = false;
        mePlayable = false;
        Debug.Log("[GameComp] Turn Ended");
        //GameSocket.SendScore(99);
    }
    #endregion

    #region Utilities
    private static void UpdateNumKeys()
    {
        if (currentRound > 6) numKeys = 10;
        else if (currentRound == 1) numKeys = 5;
        else numKeys = currentRound + 4;

        Debug.Log("[UpdateNumKeys] " + numKeys);
    }

    private static void NewTimer()
    {
        timeLimit = meGoesFirst ? 10 : 20;
        timeIsRunning = true;
        Debug.Log("[GameComp] Updated time limit: " + timeLimit);
    }
    #endregion
}
