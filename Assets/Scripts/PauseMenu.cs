using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public Text playerOneText;
    public Text playerTwoText;
    public Text numRoundText;
    public Text keysLeftText;

    // Initialize variable states
    void Start()
    {
        pauseMenuUI.SetActive(false);
        playerOneText.text = GameComponents.me.name + " (You): ";
        playerTwoText.text = GameComponents.them.name + ": ";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }

        if (GameComponents.currentRound.ToString() != numRoundText.text)
            numRoundText.text = GameComponents.currentRound.ToString();

        if (GameComponents.numKeys.ToString() != keysLeftText.text)
            keysLeftText.text = GameComponents.numKeys.ToString();
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        IsPaused = true;
    }
}
