using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System;
using UnityEngine;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft;

public class Client
{
    public static string url_dev = "https://dupme-backend-staging.herokuapp.com/";
    public static string url = "https://dupme-backend.herokuapp.com/";

    public static string username = Environment.GetEnvironmentVariable("DUPME_AUTH_USERNAME");
    public static string uid = Environment.GetEnvironmentVariable("DUPME_AUTH_UID");

    async public static void CheckAlive()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync(url_dev + "check-alive/");
        Debug.Log(content);
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
        Debug.Log(new FormUrlEncodedContent(authData));
        var token = await client.PostAsync(url_dev + "login/", postData);
        Debug.Log(token.StatusCode);
        var response = ReadContentAsync(token.Content);
    }

    private static string ReadContentAsync(HttpContent content)
    {
        return content.ReadAsStringAsync().Result;
    }
}
