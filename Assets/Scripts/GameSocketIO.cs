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


        /*  message: msg, note, endseq, score
         *  score:  "S{score}S", e.g. "S3S"
         *  endseq: "ENDSQ"
         */
        so.On("message", response =>
        {
            Debug.Log(response);
            string content = response.GetValue<string>();
            Debug.Log(content);

            if (NotesReceiver.NoteIsValid(content)) ReceiveNote(content);
            else if (content.StartsWith("S") && content.EndsWith("S")) ReceiveScore(content);
            else if (content.Equals("ENDSQ")) ReceiveEndSequence();
        });

        await so.ConnectAsync();
        await so.EmitAsync("message", "deez nutz");
    }

    public static void SendNote(string noteName)
    {
        so.EmitAsync("message", noteName);
    }

    public static void SendEndSequence()
    {
        so.EmitAsync("message", "ENDSEQ");
    }

    public static void ReceiveNote(string noteName)
    {
        var notePlayer = new ObjectClicker();
        notePlayer.onNotePlay(noteName);
        if (!GameComponents.meGoesFirst) NotesReceiver.InputNote(noteName, false);
    }

    public static void SendScore(int score)
    {
        so.EmitAsync("message", $"S{score}S");
    }

    public static void ReceiveScore(string score)
    {
        int realScore = int.Parse(score.Replace("S", ""));
        GameComponents.them.score += realScore;
        GameComponents.switchState = true;
    }

    // Only sent from first player to replying player
    public static void ReceiveEndSequence()
    {
        GameComponents.switchState = true;
    }
}
