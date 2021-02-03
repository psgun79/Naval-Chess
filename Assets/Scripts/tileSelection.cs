using System;
using UnityEngine;
using UnityEngine.UI;

public class tileSelection : MonoBehaviour
{
    public int xPos;
    public int yPos;
    Color highlight = new Color(0, Convert.ToSingle(200.0 / 255.0), 1);
    public teamDeploy system;

    void Start()
    {
        yPos = Convert.ToInt32(this.gameObject.name.Substring(5)) - 1;
        xPos = Convert.ToInt32(this.transform.parent.gameObject.name.Substring(4)) - 1;
    }

    public void HoverEnter()
    {
        if (system.selected != -1) this.gameObject.GetComponent<Image>().color = highlight;
    }

    public void HoverExit()
    {
        this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void Click()
    {
        int i, len, origin, l, t;
        if (system.lock_table[xPos, yPos, 0] != -1)
        {
            if (system.selected == -1)
            {
                l = system.lock_table[xPos, yPos, 0];
                t = system.lock_table[xPos, yPos, 1];
                if (t <= 1) len = 1; else if (t <= 3) len = 2; else len = 3;
                origin = xPos + l * 2;
                for (i = xPos + l * 2; i >= xPos - 2 * (len - l - 1); i = i - 2)
                {
                    system.lock_table[i, yPos, 0] = -1;
                    system.lock_table[i, yPos, 1] = -1;
                }
                system.Remove(origin, yPos, 0);
            }
            else return;
        }
        else
        {
            if (system.selected == -1) return;
            else if (system.selected <= 1) len = 1;
            else if (system.selected <= 3) len = 2;
            else len = 3;
            if (xPos - (len - 1) * 2 >= 0)
            {
                for (i = xPos; i >= xPos - (len - 1) * 2; i = i - 2)
                {
                    if (system.lock_table[i, yPos, 0] != -1) return;
                    else if (i == 0 && yPos == 3) return;
                }
                system.Add(xPos, yPos);
                origin = 0;
                for (i = xPos; i >= xPos - (len - 1) * 2; i = i - 2)
                {
                    system.lock_table[i, yPos, 0] = origin++;
                    system.lock_table[i, yPos, 1] = system.selected;
                }
                system.selected = -1;
            }
            else return;
        }
    }

    void Update()
    {
        if (system.selected == -1) this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }
}
