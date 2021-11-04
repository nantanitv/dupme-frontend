using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class LobbyInfo : MonoBehaviour
{
    public Text roomCode;
    private bool readyToLoad = false;

    void LoadGame()
    {
        if (GameProperties.isHardMode) SceneManager.LoadScene("HardMode");
        else SceneManager.LoadScene("EasyMode");
    }

    void Start()
    {
        readyToLoad = false;
        roomCode.text = "ROOM CODE: ";

        GameSocketIO.so.On($"{GameProperties.roomId}/room-event", response =>
        {
            Debug.Log(response);

            var content = response.GetValue();
            Debug.Log(content.ToString());

            JObject contentObj = JObject.Parse(response.GetValue<string>());
            var theirUuid = contentObj["data"]["uuid"];
            GameComponents.them.uuid = theirUuid.ToString();
            var theirName = Client.GetUserInfo(GameComponents.them.uuid);
            GameComponents.them.name = theirName.Result;

            if (content.ToString().Contains("start_game"))
            {
                string startsWith = contentObj["data"]["starts_with"].ToString();
                Debug.Log(startsWith);
                GameComponents.meGoesFirst = startsWith.Equals(GameComponents.me.uuid);
                Debug.Log("Going to game");
                Debug.Log($"hardmode {GameProperties.isHardMode}");
                readyToLoad = true;
            }
        });

        GameSocketIO.so.On($"{GameProperties.roomId}/message", response =>
        {
            string content = response.GetValue<string>();
            Debug.Log($"Room message: {content}");
            if (NotesReceiver.NoteIsValid(content)) GameSocketIO.ReceiveNote(content);
            else if (content.StartsWith("S") && content.EndsWith("S")) GameSocketIO.ReceiveScore(content);
            else if (content.Equals("ENDSQ")) GameSocketIO.ReceiveEndSequence();
        });
    }

    void Update()
    {
        if (roomCode.text.Equals("ROOM CODE: ") && GameProperties.roomId.Length==4)
            roomCode.text += GameProperties.roomId;

        if (readyToLoad) LoadGame();
    }
}
