using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Client : MonoBehaviour
{
    class CreateRoomResponse
    {
        public string room_id { get; set; }
        public string host { get; set; }
        public IList<string> players { get; set; }
        public object settings { get; set; }
        public string status { get; set; }
        public string time_created { get; set; }
    }
    class RoomSettings
    {
        public int difficulty { get; set; }
        public int levels { get; set; }
        public int player_limit { get; set; }
        public int turns { get; set; }
    }

    #region Attributes
    public static string URL_DEV_ = "https://dupme-backend-staging.herokuapp.com/";
    public static string URL_ = "https://dupme-backend.herokuapp.com/";

    // public static string username = Environment.GetEnvironmentVariable("DUPME_AUTH_USERNAME");
    // public static string uid = Environment.GetEnvironmentVariable("DUPME_AUTH_UID");
    public static string username = "admin@dupme.dupme";
    public static string uid = "4yRoC62AeVUIPkTqdMewUfRr5JI2";

    public static string AUTH_TOKEN_;
    #endregion

    public void SetPlayerName(InputField inputName)
    {
        GameComponents.me.name = inputName.text;
    }

    public async void InitGameAsync()
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
        string url = URL_DEV_ + "user/" + GameComponents.me.uuid + "/logout";
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

    async public static Task<string> GetUserInfo(string uuid)
    {
        string url = URL_DEV_ + $"/user/{uuid}/status";
        return (await Post(url))["username"];
    }
    #endregion

    #region Room Requests
    async public static Task CreateRoom()
    {
        string url = $"{URL_DEV_}create-room?uuid={GameComponents.me.uuid}&difficulty={(GameProperties.isHardMode ? "1" : "0")}&turns={GameProperties.numRounds}";
        using var client = new HttpClient();
        Debug.Log($"[URL] {url}");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AUTH_TOKEN_);
        var response = await client.PostAsync(url, null);
        Debug.Log($"[POST] Status {response.StatusCode}");

        CreateRoomResponse room = JsonConvert.DeserializeObject<CreateRoomResponse>(response.Content.ReadAsStringAsync().Result);
        GameProperties.roomId = room.room_id;
        Debug.Log($"[CreateRoom] Room ID: {GameProperties.roomId}");

        var players = room.players;
        GameComponents.them.name = players[0].Equals(GameComponents.me.name) ? players[1] : players[2];
    }

    async public static Task JoinRoom()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/join?uuid=" + GameComponents.me.uuid;
        await Post(url);
        Debug.Log($"[JoinRoom]: Joined");
        await GetRoomInfo();
    }

    async public static Task GetRoomInfo()
    {
        string url = $"{URL_DEV_}room/{GameProperties.roomId}/status";
        using var client = new HttpClient();
        Debug.Log("[URL] " + url);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AUTH_TOKEN_);
        var response = await client.GetAsync(url);
        Debug.Log(response.Content.ReadAsStringAsync().Result);
        CreateRoomResponse room = JsonConvert.DeserializeObject<CreateRoomResponse>(response.Content.ReadAsStringAsync().Result);
        RoomSettings roomset = JsonConvert.DeserializeObject<RoomSettings>(room.settings.ToString());
        GameProperties.isHardMode = roomset.difficulty == 1;
        Debug.Log($"[RoomInfo] Hardmode: {GameProperties.isHardMode}");
        GameProperties.numRounds = roomset.turns;
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

    async public static Task ResetRoom(string toReset)
    {
        string url = URL_DEV_ + "room/" + toReset + "/close";
        await Post(url);
    }

    async public static Task ResetAllRooms()
    {
        string url = URL_DEV_ + "misc/reset";
        await Post(url);
    }
    #endregion

    #region Game Requests
    async public static Task StartGame()
    {
        string url = URL_DEV_ + "room/" + GameProperties.roomId + "/start";
        Dictionary<string, string> content = await Post(url);
        string goesFirst = content["starts_with"];
        GameComponents.meGoesFirst = goesFirst.Equals(GameComponents.me.uuid);
        Debug.Log($"Start: {goesFirst}");
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

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AUTH_TOKEN_);
        var response = await client.PostAsync(url, null);

        Dictionary<string, string> content = ContentToDictAsync(response.Content);
        Debug.Log("[POST] Status " + response.StatusCode);

        return content;
    }

    private static Dictionary<string, string> ContentToDictAsync(HttpContent content)
    {
        return JsonStringToDict(content.ReadAsStringAsync().Result);
    }

    private static Dictionary<string, string> JsonStringToDict(string json)
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
