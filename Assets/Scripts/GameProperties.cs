using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameSettings
{
    
}

public class GameProperties : MonoBehaviour
{
    public static bool isHardMode = false;
    public static int numTurns = 3;

    // Set number of turns in Create Menu
    public void SetNumTurns(InputField newNumTurns)
    {
        int turns = int.Parse(newNumTurns.text);
        numTurns = turns;
        Debug.Log(numTurns);
    }

    // Toggle Hard/Easy mode in Create Menu
    public void ToggleHardMode()
    {
        isHardMode = !isHardMode;
        Debug.Log("Hard Mode: " + isHardMode);
    }
}
