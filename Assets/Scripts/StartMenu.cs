using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    private void Start()
    {

    }

    #region Load Scenes
    public static void GoToStartMenu()
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

    public void GoToAdminPanel()
    {
        SceneManager.LoadScene("AdminMenu");
    }

    async public static void GoToGame()
    {
        await Client.StartGame();
        if (GameProperties.isHardMode) SceneManager.LoadScene("HardMode");
        else SceneManager.LoadScene("EasyMode");
    }
    #endregion

    async public static void QuitGame()
    {
        Debug.Log("Quit");
        await Client.LogOut();
        Application.Quit();
    }

    async public static void QuitToMainMenu()
    {
        await Client.KickUser();
        GoToStartMenu();
    }

    async public static void BackFromLobby()
    {
        await Client.CloseRoom();
        GoToStartMenu();
    }

    public async void CreateRoom()
    {
        await Client.CreateRoom();
    }

    public async void JoinRoom(InputField roomIdInput)
    {
        string input = roomIdInput.text;
        if (input.Equals("ADMIN")) GoToAdminPanel();
        else
        {
            Debug.Log("Before lobby");
            GoToLobby();
            GameProperties.roomId = input;
            Debug.Log("Before JoinRoom");
            await Client.JoinRoom();
            
        }
    }

    public async void ResetRoom(InputField roomToReset)
    {
        await Client.ResetRoom(roomToReset.text);
    }

    public async void ResetAllRoom()
    {
        await Client.ResetAllRooms();
    }
}
