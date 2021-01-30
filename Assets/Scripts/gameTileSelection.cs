using UnityEngine;
using System;

public class gameTileSelection : MonoBehaviour
{
    float a = 0;
    float b = 1f;
    float c = Convert.ToSingle(159.0 / 255.0);
    public GameObject cover;
    public int xPos;
    public int yPos;
    public bool movable = false;
    boardSystem system;

    void Start()
    {
        system = GameObject.Find("Game Manager").GetComponent<boardSystem>();
        yPos = Convert.ToInt32(this.gameObject.name.Substring(5)) - 1;
        xPos = Convert.ToInt32(this.transform.parent.gameObject.name.Substring(4)) - 1;
    }

    public void HoverEnter()
    {
        if (system.mode_move && movable)
        {
            cover.GetComponent<Light>().color = new Color(1, 1, 1);
        }
    }

    public void HoverExit()
    {
        if (system.mode_move && movable)
        {
            cover.GetComponent<Light>().color = new Color(a, b, c);
        }
    }

    public void Click()
    {
        if (system.mode_move && movable)
        {
            system.Move(xPos, yPos);
            cover.GetComponent<Light>().color = new Color(a, b, c);
        }
    }

    void Update()
    {
        if (!system.mode_move)
        {
            cover.GetComponent<Light>().enabled = false;
            movable = false;
        }
        else if (movable) cover.GetComponent<Light>().enabled = true;
    }
}
