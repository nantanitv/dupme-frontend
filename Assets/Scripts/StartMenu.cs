using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    private void Start()
    {

    }
    
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

    public void GoToCreateRoom()
    { 
        SceneManager.LoadScene("CreateRoomMenu");
    }

    public void GoToJoinRoom()
    {
        SceneManager.LoadScene("JoinRoomMenu");
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void GoToGame()
    {
        if (GameProperties.isHardMode) SceneManager.LoadScene("HardMode");
        else SceneManager.LoadScene("EasyMode");
    }
}
