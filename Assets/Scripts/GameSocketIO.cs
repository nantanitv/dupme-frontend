using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;

public class GameSocketIO : MonoBehaviour
{
    public SocketIOClient.SocketIO so;

    async void Awake()
    {
        so = new SocketIOClient.SocketIO(Client.URL_DEV_);
        so.OnConnected += (sender, args) =>
        {
            Debug.Log("Connected");
        };

        so.On("server-event", MessageHandler =>
        {
            GameComponents.EndGame();
        });

        so.On("room-event", (e) =>
        {

        });

        await so.ConnectAsync();
        await so.EmitAsync("message", "deez nutz");
    }
}
