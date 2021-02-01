using UnityEngine;
using System;

public class gameTileSelection : MonoBehaviour
{
    public Material normal;
    public Material highlight_off;
    public Material highlight_on;
    public MeshRenderer cover;
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
        if (system.mode_move && movable) highlightOn();
    }

    public void HoverExit()
    {
        if (system.mode_move && movable) highlightOff();
    }

    public void Click()
    {
        if (system.mode_move && movable) system.Move(xPos, yPos);
    }

    public void highlightOn()
    {
        Material[] materials = cover.materials;
        materials[0] = highlight_on;
        cover.materials = materials;
    }

    public void highlightOff()
    {
        Material[] materials = cover.materials;
        materials[0] = highlight_off;
        cover.materials = materials;
    }

    public void highlightDisabled()
    {
        Material[] materials = cover.materials;
        materials[0] = normal;
        cover.materials = materials;
    }
}
