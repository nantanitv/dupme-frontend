using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public Text playerOneText;
    public Text playerTwoText;
    public Text numRoundText;
    public Text keysLeftText;
    public Text timeText;
    public Text noteText;
    public static string toShow;


    // Initialize variable states
    void Start()
    {
        pauseMenuUI.SetActive(false);
        playerOneText.text = GameComponents.me.name + " (You): 0";
        playerTwoText.text = GameComponents.them.name + ": 0";
    }

    // Update is called once per frame
    void Update()
    {
        noteText.text = toShow;
        playerOneText.text = $"{GameComponents.me.name} (You): {GameComponents.me.score}";
        playerTwoText.text = $"{GameComponents.them.name}: {GameComponents.them.score}";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Results");
        }

        if (GameComponents.currentRound.ToString() != numRoundText.text)
            numRoundText.text = GameComponents.currentRound.ToString();

        if (GameComponents.numKeys.ToString() != keysLeftText.text)
            keysLeftText.text = GameComponents.numKeys.ToString();

        if (GameComponents.timeIsRunning)
        {
            string timeLeft = ((int)(GameComponents.timeLimit+1)).ToString();
            if (int.Parse(timeLeft) <= 20) timeText.text = timeLeft;
            else timeText.text = "0";
        }
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
