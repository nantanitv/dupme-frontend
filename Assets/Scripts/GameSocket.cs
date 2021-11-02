using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;
using System.Net.Http;

public class GameSocket : MonoBehaviour
{
    public static SocketIOComponent so;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        so = go.GetComponent<SocketIOComponent>();
        so.url = Client.URL_DEV_;
        so.SetHeader("Bearer", Client.AUTH_TOKEN_);

        so.On("open", OpenConnection);
        so.On("close", CloseConnection);
        so.On("note", ReceiveNote);
        so.On("results", ReceiveResults);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenConnection(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Connection Opened");
    }

    void CloseConnection(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Connection Closed");
    }

    void ReceiveNote(SocketIOEvent e)
    {
        var noteVal = e.data.ToDictionary()["note"];
        Debug.Log("[SocketIO] Note data received: " + noteVal);

        if (NotesReceiver.NoteIsValid(noteVal))
        {
            NotesReceiver.InputNote(noteVal, false);
        }
    }

    void ReceiveResults(SocketIOEvent e)
    {
        // var replyNotesVal = e.data.ToDictionary()["notes"];
        int score = int.Parse(e.data.ToDictionary()["score"]);

        Debug.Log("[SocketIO] Score received: " + score);
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
    }
}
