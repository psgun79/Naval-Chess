using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpdate : MonoBehaviour
{
    public teamDeploy system;
    public int type;
    bool locked = false;
    Color highlight;
    public Color normal;
    Color disabled = new Color(Convert.ToSingle(144.0 / 255.0), Convert.ToSingle(144.0 / 255.0), Convert.ToSingle(144.0 / 255.0), Convert.ToSingle(144.0 / 255.0));

    void Start()
    {
        highlight = new Color(normal.r + (1 - normal.r) / 2, normal.g + (1 - normal.g) / 2, normal.b + (1 - normal.b) / 2);
    }

    public void HoverEnter()
    {
        if (!locked && system.selected != type) this.gameObject.GetComponent<Image>().color = highlight;
    }

    public void HoverExit()
    {
        if (!locked && system.selected != type) this.gameObject.GetComponent<Image>().color = normal;
    }

    public void LightOn()
    {
        this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void LightOff()
    {
        locked = false;
        this.gameObject.GetComponent<Image>().color = normal;
    }

    public void Lock()
    {
        locked = true;
        this.gameObject.GetComponent<Image>().color = disabled;
    }
}
