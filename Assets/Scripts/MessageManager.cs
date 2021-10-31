using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public string username;

    public int maxMessages = 25;

    public GameObject chatPanel, textObject;

    public InputField chatbox;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    void Start()
    {
        
    }


    void Update()
    {
        if(chatbox.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(username + ": " + chatbox.text);
                chatbox.text = "";
            }
        } else
        {
            if(!chatbox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatbox.ActivateInputField();
            }
        }

        if(!chatbox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToChat("Space bar pressed");

            }
        }

        
    }

    public void SendMessageToChat(string text)
    {
        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;


        messageList.Add(newMessage);
    }
}



[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
