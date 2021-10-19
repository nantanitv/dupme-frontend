using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Adios Dupme
    public static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToCreateLobby()
    {
        SceneManager.LoadScene("CreateRoomMenu");
    }

    public void GoToJoinLobby()
    {
        SceneManager.LoadScene("JoinRoomMenu");
    }
}
