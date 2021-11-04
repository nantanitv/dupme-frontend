using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class DisplayOnlinePlayers : MonoBehaviour
{
    public Text onlineUsers;
    public Text status;

    // Start is called before the first frame update
    async void Start()
    {
        onlineUsers.text = "Please wait";
        string allUsers = await Client.GetAllUsers();
        JObject jo = JObject.Parse(allUsers);

        onlineUsers.text = "";
        status.text = "";
        foreach (var pair in jo)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();
            JObject p = JObject.Parse(pair.Value.ToString());
            foreach (var property in p)
            {
                Debug.Log($"{property.Key}: {property.Value}");
                user.Add(property.Key, property.Value.ToString());
            }
            onlineUsers.text += $"{user["username"]}:\n";
            status.text += $"{user["status"]}\n";
        }
        
    }

    // Update is called once per frame
    async void Update()
    {
        
        //new WaitForSeconds(1);
        //onlineUsers.text = await Client.GetAllUsers();
    }
}
