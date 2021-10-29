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
        
        EnvLoader.Load();
        Debug.Log(Client.username);
        Debug.Log(Client.uid);
        Client.CheckAlive();
        Client.Login();
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
