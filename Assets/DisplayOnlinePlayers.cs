using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOnlinePlayers : MonoBehaviour
{
    public Text onlineUsers;

    // Start is called before the first frame update
    async void Start()
    {
        onlineUsers.text = await Client.GetAllUsers();
    }

    // Update is called once per frame
    async void Update()
    {
        new WaitForSeconds(1);
        onlineUsers.text = await Client.GetAllUsers();
    }
}
