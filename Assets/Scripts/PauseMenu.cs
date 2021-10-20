using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;

    // Initialize variable states
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
    }
    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        IsPaused = true;
    }

    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
