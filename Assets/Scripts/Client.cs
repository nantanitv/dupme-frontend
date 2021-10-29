using System.Collections;
using System.Collections.Generic;
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

    private static string AUTH_TOKEN_= "";

    private void Start()
    {
        
    }

    private void setPlayerName(InputField inputName)
    { 
        GameComponents.me.name = inputName.ToString();
    }

    private void initGame()
    {
        string name = GameComponents.me.name;
        CreateUser(name);
        Login();
    }

    async public static void CheckAlive()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync(URL_DEV_ + "check-alive/");
    }

    async public static void Login()
    {
        using var client = new HttpClient();
        var authData = new Dictionary<string, string>()
        {
            { "username", username },
            { "uid", uid }
        };

        var postData = new FormUrlEncodedContent(authData);

        string url = URL_DEV_ + "login/";
        var response = await client.PostAsync(url, postData);

        Debug.Log("[Client] Status " + response.StatusCode);
        Dictionary<string, string> content = ContentToDictAsync(response.Content);
        string token = content["token"];
        Debug.Log("[Client] Token: " + token);
        AUTH_TOKEN_ = token;
    }

    async public static void CreateUser(string name)
    {
        using var client = new HttpClient();
        if (AUTH_TOKEN_.Equals("")) Login();
        client.DefaultRequestHeaders.Add("token", AUTH_TOKEN_);
        string url = URL_DEV_ + "/create-user/";
        
        var payload = new Dictionary<string, string>()
        {
            { "username", name }
        };

        var postData = new FormUrlEncodedContent(payload);
        var response = await client.PostAsync(url, postData);
        Dictionary<string, string> content = ContentToDictAsync(response.Content);
        foreach (var key in content.Keys)
        {
            Debug.Log("[CreateUser] " + key + ": " + content[key]);
        }
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
}
