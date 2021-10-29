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

    private static string AUTH_TOKEN_;

    private async void Start()
    {
        EnvLoader.Load();
        Debug.Log(username);
        Debug.Log(uid);
        await CheckAlive();
    }

    public void setPlayerName(InputField inputName)
    {
        GameComponents.me.name = inputName.text;
    }

    public async void initGameAsync()
    { 
        await Login();
        await CreateUser(GameComponents.me.name);
    }

    async public static Task CheckAlive()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync(URL_DEV_ + "check-alive/");
    }

    async public static Task Login()
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
        AUTH_TOKEN_ = token;
        Debug.Log("[Client] Added: " + AUTH_TOKEN_);
    }

    async public static Task CreateUser(string name)
    {
        string url = URL_DEV_ + "create-user/";
        using var client = new HttpClient();

        HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AUTH_TOKEN_);

        /*var payload = new Dictionary<string, string>()
        {
            { "username", name }
        };


        var jsonPayload = new JSONObject(payload);
        string ct = jsonPayload.Print();
        Debug.Log("[JSON] " + ct);
        httpRequest.Content = new StringContent(ct, System.Text.Encoding.UTF8, "application/json");*/

        string payload = "{ \"username\": \"" + name + "\" }";
        Debug.Log(payload);
        httpRequest.Content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        // httpRequest.Content = new FormUrlEncodedContent(payload);
        var response = await client.SendAsync(httpRequest);
        Debug.Log("[CreateUser] Status " + response.StatusCode);

        Dictionary<string, string> content = ContentToDictAsync(response.Content);
        Debug.Log("[CreateUser] Status: " + content["status"]);
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
