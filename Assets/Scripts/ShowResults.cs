using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{
    public Text winner;
    public Text result;

    // Start is called before the first frame update
    void Start()
    {
        if (GameComponents.me.score > GameComponents.them.score) winner.text += GameComponents.me.name;
        else if (GameComponents.me.score < GameComponents.them.score) winner.text += GameComponents.them.name;
        else winner.text = "DRAW!";

        result.text = $"{GameComponents.me.name} {GameComponents.me.score} : {GameComponents.them.name} {GameComponents.them.score}";
    }

    // Update is called once per frame
    void Update()
    {
        if (winner.text.Equals("WINNER: "))
        {
            if (GameComponents.me.score > GameComponents.them.score) winner.text += GameComponents.me.name;
            else if (GameComponents.me.score < GameComponents.them.score) winner.text += GameComponents.them.name;
            else winner.text = "DRAW!";
        }
        if (result.text.Equals(":"))
        {
            result.text = $"{GameComponents.me.name} {GameComponents.me.score} : {GameComponents.them.name} {GameComponents.them.score}";
        }
    }

}
