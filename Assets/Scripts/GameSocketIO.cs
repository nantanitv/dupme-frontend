using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using SocketIOClient.JsonSerializer;
using Newtonsoft.Json;

public class GameSocketIO : MonoBehaviour
{
    public static SocketIOClient.SocketIO so;

    async void Awake()
    {
        so = new SocketIOClient.SocketIO(Client.URL_DEV_);
        var jsonSerializer = new JsonSerializer();
        
        so.OnConnected += (sender, args) =>
        {
            Debug.Log("Connected");
        };

        so.On("server-event", MessageHandler =>
        {
            GameComponents.EndGame();
        });

        so.On("room-event", response =>
        {
            Debug.Log(response);
            // string ev = response.GetValue<string>();
            // if (ev.Equals("start_game")) GameComponents.StartGame();
        });

        so.On("message", response =>
        {
            Debug.Log(response);
            string content = response.GetValue<string>();
            Debug.Log(content);
        });

        await so.ConnectAsync();
        await so.EmitAsync("message", "deez nutz");
    }
}
