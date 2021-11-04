using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;
using Newtonsoft.Json;

public class GameSocket : MonoBehaviour
{
    /*public static SocketIOComponent so;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        so = go.GetComponent<SocketIOComponent>();
        so.url = Client.URL_DEV_;
        so.SetHeader("Bearer", Client.AUTH_TOKEN_);

        so.On("server-reset", ServerResetHandler => {
            Debug.Log("[SERVER] Server Reset");
            StartMenu.GoToStartMenu();
        });

        so.On("room-event", RoomEventHandler);
        so.On("message", MessageHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RoomEventHandler(SocketIOEvent e)
    {
        Debug.Log("[RoomEventHandler] Got a room event");
        string eventName = e.data.ToDictionary()["event"];
        if (eventName.Equals("start_game")) GameComponents.StartGame();
        else if (eventName.Equals("room_closed") || eventName.Equals("end_game")) GameComponents.EndGame();
    }

    void MessageHandler(SocketIOEvent e)
    {
        Debug.Log("[MessageHandler] Got a message");
        if (e.data.ToDictionary()["type"].Equals("note")) ReceiveNote(e);
        else if (e.data.ToDictionary()["type"].Equals("score")) ReceiveResults(e);
    }

    void ReceiveNote(SocketIOEvent e)
    {
        var noteVal = e.data.ToDictionary()["data"];
        Debug.Log("[SocketIO] Note data received: " + noteVal);

        if (NotesReceiver.NoteIsValid(noteVal)) NotesReceiver.InputNote(noteVal, false);
        else if (noteVal.Equals("your_turn")) GameComponents.me.isTurn = true;
    }

    void ReceiveResults(SocketIOEvent e)
    {
        // var replyNotesVal = e.data.ToDictionary()["notes"];
        int score = int.Parse(e.data.ToDictionary()["data"]);

        Debug.Log("[SocketIO] Score received: " + score);
        GameComponents.them.score += score;
        GameComponents.me.isTurn = true;
    }

    public static void SendNote(string n)
    {
        Dictionary<string, string> eventData = new Dictionary<string, string>()
        {
            { "note", n }
        };

        JSONObject toSend = new JSONObject(eventData);
        so.Emit("note", toSend);
        Debug.Log("[SocketIO] Emitted data: " + toSend.Print());
    }

    public static void SendScore(int score)
    {
        Dictionary<string, string> eventData = new Dictionary<string, string>()
        {
            { "score", score.ToString() }
        };

        JSONObject toSend = new JSONObject(eventData);
        so.Emit("result", toSend);
        Debug.Log("[SocketIO] Emitted score: " + toSend.Print());
    }*/
}
