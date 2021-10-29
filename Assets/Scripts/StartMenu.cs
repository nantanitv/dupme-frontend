using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public static string url_dev = "dupme-backend-staging.heroku.app";
    public static string url = "dupme-backend.heroku.app";
    
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

    async public void GoToCreateRoom()
    {
        /*using var client = new HttpClient();
        var response = await client.PostAsync(url, data);
        string result = response.Content.ReadAsStringAsync().Result;*/
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
