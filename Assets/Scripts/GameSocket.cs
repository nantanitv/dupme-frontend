using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using SocketIO;
using System;

public class GameSocket : MonoBehaviour
{
    public static SocketIOComponent so;

    public class Room
    {
        string id;

    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        so = go.GetComponent<SocketIOComponent>();

        so.On("open", openConnection);
        so.On("close", closeConnection);
        so.On("note", rcvNote);
        so.On("res", rcvResults);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void openConnection(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Connection Opened");
    }

    void closeConnection(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Connection Closed");
    }

    void rcvNote(SocketIOEvent e)
    {
        var noteVal = e.data.ToDictionary()["note"];
        Debug.Log("[SocketIO] Note data received: " + noteVal);

        NotesReceiver.Receiver rcv = new NotesReceiver.Receiver();
        if (NotesReceiver.Receiver.noteIsValid(noteVal))
        {
            rcv.inputNote(noteVal, false);
        }
    }

    void rcvResults(SocketIOEvent e)
    {
        var replyNotesVal = e.data.ToDictionary()["notes"];
        int score = int.Parse(e.data.ToDictionary()["score"]);

        Debug.Log("[SocketIO] Score received: " + score);
    }

    public static void sendNote(string n)
    {
        Dictionary<string, string> eventData = new Dictionary<string, string>()
        {
            { "note", n }
        };
        JSONObject toSend = new JSONObject(eventData);
        // so.Emit("note", toSend);
        Debug.Log("[SocketIO] Emitted data: " + toSend.Print());
    }

    public static void sendScore(int score)
    {
        var eventData = new Dictionary<string, string>()
        {
            { "score", score.ToString() }
        };

        JSONObject toSend = new JSONObject(eventData);
        // so.Emit("res", toSend);
        Debug.Log("[SocketIO] Emitted score: " + toSend.Print());
    }

    /*
    public static void sendReplyNotes(List<string> notes)
    {
        var eventData = new Dictionary<string, string[]>()
        {
            { "notes", notes.ToArray() }
        };

        JSONObject toSend = JSONTemplates.TOJSON(eventData);
        so.Emit("res", toSend);
        Debug.Log("[SocketIO] Emitted reply: " + toSend.Print());
    }*/
}
