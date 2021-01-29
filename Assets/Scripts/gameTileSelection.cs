using UnityEngine;
using System;

public class gameTileSelection : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public bool movable;
    public boardSystem system;

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
            // move pending 단계에서만 강조 표시
        }
    }

    public void HoverExit()
    {
        if (system.mode_move && movable)
        {
            // move pending 단계에서만 강조 표시 제거
        }
    }

    public void Click()
    {
        if (system.mode_move && movable)
        {
            system.Move(xPos, yPos);
        }
    }

    void Update()
    {
        if (!system.mode_move)
        {
            // 강조 표시 제거
        }
    }
}
