using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public static string URL_DEV_ = "https://dupme-backend-staging.herokuapp.com/";
    public static string URL_ = "https://dupme-backend.herokuapp.com/";

    public static string username = Environment.GetEnvironmentVariable("DUPME_AUTH_USERNAME");
    public static string uid = Environment.GetEnvironmentVariable("DUPME_AUTH_UID");

    public static string AUTH_TOKEN_;

    private void Start()
    {
        
    }

    public void setPlayerName(InputField inputName)
    {
        GameComponents.me.name = inputName.text;
    }

    public async void initGameAsync()
    {
        EnvLoader.Load();
        Debug.Log("[Username] " + username);
        await Login();
        await CreateUser(GameComponents.me.name);
    }

    async public static Task Login()
    {
        using var client = new HttpClient();
        var authData = new Dictionary<string, string>()
        {
            { "username", username },
            { "uid", uid }
        };

        var payload = new FormUrlEncodedContent(authData);

        string url = URL_DEV_ + "login/";
        var response = await client.PostAsync(url, payload);

        Debug.Log("[Client] Status " + response.StatusCode);
        Dictionary<string, string> content = ContentToDictAsync(response.Content);

        AUTH_TOKEN_ = content["token"];
        Debug.Log("[Client] Added: " + AUTH_TOKEN_);
    }

    async public static Task LogOut()
    {
        string url = URL_DEV_ + "user/" + GameComponents.me.uuid + "/logout/";
        await Post(url);
    }
    
    #region User Requests
    // THANKS PUTTER
    async public static Task CreateUser(string name)
    {
        string url = URL_DEV_ + "create-user?username=" + name;
        Dictionary<string, string> content = await Post(url);
        GameComponents.me.uuid = content["uuid"];
    }

    async public static Task ChangeName(string name)
    {
        string url = URL_DEV_ + "user/" + GameComponents.me.uuid + "/change?to=" + name;
        await Post(url);
    }
    #endregion

    #region Room Requests
    async public static Task CreateRoom()
    {
        string url = 
            URL_DEV_ + "create-room?uuid=" + GameComponents.me.uuid + 
            "&difficulty=" + (GameProperties.isHardMode ? "1" : "0") + 
            "&turns=" + GameProperties.numRounds;
        Dictionary<string, string> content = await Post(url);
        GameProperties.roomId = content["room_id"];
        Debug.Log("[CreateRoom] Room ID: " + GameProperties.roomId);
    }

    async public static Task JoinRoom()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/join?uuid=" + GameComponents.me.uuid;
        await Post(url);
    }

    async public static Task KickUser()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/kick";
        await Post(url);
    }

    async public static Task CloseRoom()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/close";
        await Post(url);
    }
    #endregion

    #region Game Requests
    async public static Task StartGame()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/start";
        await Post(url);
    }

    async public static Task EndGame()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/end";
        await Post(url);
    }
    #endregion

    #region Utility functions
    async static Task<Dictionary<string, string>> Post(string url)
    {
        using var client = new HttpClient();
        Debug.Log("[URL] " + url);

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AUTH_TOKEN_);
        var response = await client.PostAsync(url, null);

        Dictionary<string, string> content = ContentToDictAsync(response.Content);
        Debug.Log("[POST] Status " + response.StatusCode);

        return content;
    }

    private static Dictionary<string, string> ContentToDictAsync(HttpContent content)
    {
        return jsonStringToDict(content.ReadAsStringAsync().Result);
    }

    private static Dictionary<string, string> jsonStringToDict(string json)
    {
        JSONObject js = new JSONObject(json);
        Dictionary<string, string> jsDict = js.ToDictionary();
        return jsDict;
    }

    async public static Task CheckAlive()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync(URL_DEV_ + "check-alive/");
    }
    #endregion
}
