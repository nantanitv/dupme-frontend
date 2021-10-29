using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameComponents : MonoBehaviour
{
    #region Class Player
    public class Player
    {
        public string name;
        public int avatarID;
        public bool isTurn;
        public string playerID;

        public Player(string id)
        {
            name = "Bob";
            avatarID = 0;
            isTurn = false;
            playerID = id;
        }

        public Player(string id, string name)
        {
            this.name = name;
            avatarID = 0;
            isTurn = false;
            playerID = id;
        }
    }
    #endregion

    #region Attibutes
    public static bool playable;
    public static bool goFirst;

    public static int numKeys;
    public static int currentRound = 0;

    public static NotesReceiver.Receiver rcv = new NotesReceiver.Receiver();

    public Player me = new Player("Alice");
    public Player them = new Player("Bobby");

    public GameProperties gameProps;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameProps = gameObject.AddComponent<GameProperties>();
        newRound();

        Debug.Log("[GameComp] The game has started. Players: " + me.name + " vs " + them.name);
        Debug.Log("[GameComp] Number of rounds: " + GameProperties.numRounds);
        Debug.Log("[GameComp] Difficulty: " + (GameProperties.isHardMode ? "Hard" : "Easy"));
        Debug.Log("[GameComp] Playable: " + playable);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                newRound();
            }
            if (numKeys > 0)
            {
                playable = true;
                Debug.Log("[GameComp] Playable: " + playable);
            }
        }
    }

    private void FixedUpdate()
    {
        if (playable && numKeys == 0)
        {
            playable = false;
            Debug.Log("[GameComp] Playable: " + playable);
            GameSocket.sendScore(99);

        }

        if (currentRound > GameProperties.numRounds)
        {
            Debug.Log("[GameComp] Game Ends");
            StartMenu endGame = new StartMenu();
            endGame.GoToStartMenu();
        }
    }

    private void updateNumKeys()
    {
        if (currentRound > 6) numKeys = 10;
        else if (currentRound == 1) numKeys = 5;
        else numKeys = currentRound + 4;

        Debug.Log("[UpdateNumKeys] " + numKeys);
    }

    public void newRound()
    {
        ++currentRound;
        Debug.Log("[GameComp] Current Round: " + currentRound + "/" + GameProperties.numRounds);
        playable = true;
        updateNumKeys();
        Debug.Log("[GameComp] Playable Keys: " + numKeys);
    }

    public void updateTurn()
    {
        
    }
}
