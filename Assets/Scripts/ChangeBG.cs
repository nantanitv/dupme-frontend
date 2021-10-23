using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour
{
    public Renderer top;
    public Renderer bottom;
    public Renderer left;
    public Renderer right;
    public Renderer ahead;
    public Renderer behind;

    private void changeBGcolor(Color color)
    {
        top.material.color = color;
        bottom.material.color = color;
        left.material.color = color;
        right.material.color = color;
        ahead.material.color = color;
        behind.material.color = color;
    }

    public void BGRed()
    {
        changeBGcolor(Color.red);
    }
    public void BGOrange()
    {
        changeBGcolor(new Color(1f, 0.5f, 0f, 1f));
    }
    public void BGYellow()
    {
        changeBGcolor(Color.yellow);
    }
    public void BGGreen()
    {
        changeBGcolor(Color.green);
    }
    public void BGWhite()
    {
        changeBGcolor(Color.white);
    }
    public void BGBlack()
    {
        changeBGcolor(Color.black);
    }
    public void BGBlue()
    {
        changeBGcolor(Color.blue);
    }
    public void BGPink()
    {
        changeBGcolor(new Color(0.88235f, 0.50588f, 0.8745f, 1f));
    }
}
