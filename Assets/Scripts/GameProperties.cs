using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProperties : MonoBehaviour
{
    public static bool isHardMode = false;
    public static int numRounds = 2;

    // Set number of rounds (2*turns) in Create Menu
    public void SetNumRounds(InputField newNumRounds)
    {
        int rounds = int.Parse(newNumRounds.text);
        numRounds = rounds >= 2 ? rounds : 2;
        Debug.Log(numRounds);
    }

    // Toggle Hard/Easy mode in Create Menu
    public void ToggleHardMode()
    {
        isHardMode = !isHardMode;
        Debug.Log("Hard Mode: " + isHardMode);
    }
}
