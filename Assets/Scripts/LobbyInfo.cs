using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInfo : MonoBehaviour
{
    public Text roomCode;

    // Start is called before the first frame update
    void Start()
    {
        roomCode.text = "ROOM CODE: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (roomCode.text.Equals("ROOM CODE: ") && GameProperties.roomId.Length==4)
            roomCode.text += GameProperties.roomId;
    }
}
