using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameComponents : MonoBehaviour
{
    public static bool playable;

    public static int numKeys = 5;
    public static int keysPressed = 0;
    public static int currentRound = 1;

    public Player p1 = new Player("Alice");
    public Player p2 = new Player("Bobby");

    public GameProperties gameProps;

    // Start is called before the first frame update
    void Start()
    {
        gameProps = gameObject.AddComponent<GameProperties>();
        Debug.Log("[GameComp] The game has started. Players: " + p1.name + " vs " + p2.name);
        Debug.Log("[GameComp] Number of rounds: " + GameProperties.numRounds);
        Debug.Log("[GameComp] Difficulty: " + (GameProperties.isHardMode ? "Hard" : "Easy"));

        updateNumKeys();
        playable = true;

        Debug.Log("[GameComp] Playable: " + playable);
    }

    // Update is called once per frame
    void Update()
    {
        if (playable && numKeys == 0)
        {
            // currentRound++;
            playable = false;
            Debug.Log("[GameComp] Playable: " + playable);
        }
        else if (!playable && numKeys > 0)
        {
            playable = true;
        }
    }

    private void FixedUpdate()
    {

    }

    private void updateNumKeys()
    {
        if (currentRound > 6)
            numKeys = 10;
        else if (currentRound == 1)
            numKeys = 5;
        else
            numKeys = currentRound + 3;
    }

    public class Player
    {
        public string name;
        public int avatarID;
        public bool isTurn;

        public Player()
        {
            name = "Bob";
            avatarID = 0;
            isTurn = false;
        }

        public Player(string name)
        {
            this.name = name;
            avatarID = 0;
            isTurn = false;
        }
    }
}
