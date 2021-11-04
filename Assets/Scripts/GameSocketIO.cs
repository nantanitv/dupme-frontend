using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using SocketIOClient.JsonSerializer;
using Newtonsoft.Json.Linq;

public class GameSocketIO : MonoBehaviour
{
    public static SocketIOClient.SocketIO so;
    public GameObject socketHolder;

    async void Awake()
    {
        DontDestroyOnLoad(socketHolder);
        so = new SocketIOClient.SocketIO(Client.URL_DEV_);

        so.OnConnected += (sender, args) =>
        {
            Debug.Log("Connected");
        };

        so.On("server-event", MessageHandler =>
        {
            GameComponents.EndGame();
        });

        so.On("message", response =>
        {
            Debug.Log(response);

            string content = response.GetValue<string>();
            // Debug.Log(content);

            if (content.Equals("ENDSEQ")) ReceiveEndSequence();
            else if (content.StartsWith("SCORE")) ReceiveScore(content);
            else if (NotesReceiver.NoteIsValid(content)) ReceiveNote(content);
            // else if (content.Equals("START1")) GameComponents.meGoesFirst = true;
            // else if (content.Equals("START0")) GameComponents.meGoesFirst = false;
        });

        await so.ConnectAsync();
        await so.EmitAsync("message", "deez nutz");
    }

    async public static void EmitJoinRoom()
    {
        string joinReq = $"{{\"room_id\": \"{GameProperties.roomId}\"}}";
        JObject reqJson = JObject.Parse(joinReq);
        await so.EmitAsync($"{GameProperties.roomId}/room-event", reqJson);
    }

    public static void EmitLeaveRoom()
    {
        string joinReq = $"{{\"room_id\": \"{GameProperties.roomId}\"}}";
        JObject reqJson = JObject.Parse(joinReq);
        so.EmitAsync("leave-room", new JSONObject($"{{\"room_id\": {GameProperties.roomId}"));
    }

    public static void EmitNote(string noteName)
    {
        so.EmitAsync("message", noteName);
    }

    public static void EmitEndSequence()
    {
        so.EmitAsync("message", "ENDSEQ");
    }

    public static void ReceiveNote(string noteName)
    {
        Debug.Log($"Received {noteName}");
        // var notePlayer = new GameObject().AddComponent<ObjectClicker>();
        // notePlayer.OnNotePlay(noteName);
        NotesReceiver.InputNote(noteName, false);
    }

    public static void EmitScore(int score)
    {
        so.EmitAsync("message", $"SCORE{score}");
    }

    public static void ReceiveScore(string score)
    {
        int realScore = int.Parse(score.Replace("SCORE", ""));
        Debug.Log($"Received Score: {realScore}");
        GameComponents.them.score += realScore;
        GameComponents.switchState = true;
    }

    // Only sent from first player to replying player
    public static void ReceiveEndSequence()
    {
        GameComponents.switchState = true;
    }
}
