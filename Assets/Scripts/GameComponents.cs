using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

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
    private static float timeLimit;
    private static bool timeIsRunning;

    public static int numKeys;
    public static int currentRound = 0;

    public static Player me = new Player("", "Alice");
    public static Player them = new Player("", "Bobby");
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meGoesFirst = true;
        StartCoroutine(PlayFirst());
        
    }

    // Update is called once per frame
    void Update()
    {

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
        NewRound();

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

        Debug.Log("[PlayFirst] Done");
    }

    IEnumerator PlayLater()
    {
        yield return null;
    }

    IEnumerator WaitFirst()
    {
        
        yield return null;
    }

    IEnumerator WaitLater()
    {

        yield return null;
    }

    public static void NewRound()
    {
        ++currentRound;
        Debug.Log($"[GameComp] Current Round: {currentRound}/{GameProperties.numRounds}");
        UpdateNumKeys();
        Debug.Log($"[GameComp] Playable Keys: {numKeys}");
        NotesReceiver.ResetSequences();
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
