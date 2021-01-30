using System;
using UnityEngine;

public class shipInfo : MonoBehaviour
{
    float a = Convert.ToSingle(227.0 / 255.0);
    float b = Convert.ToSingle(89.0 / 255.0);
    float c = Convert.ToSingle(89.0 / 255.0);
    float d = Convert.ToSingle(231.0 / 255.0);
    float e = Convert.ToSingle(19.0 / 255.0);
    float f = Convert.ToSingle(19.0 / 255.0);
    public int currentHealth;
    public int totalHealth;
    public int AP = 3;
    public int team;
    public int xPos;
    public int yPos;
    public int type;
    public int len;
    public Transform icon;
    public RectTransform currentHealthBar;
    public RectTransform totalHealthBar;
    public Canvas healthCanvas;
    public GameObject cover;
    public bool attackable = false;
    public bool selected = false;
    public bool scriptonly;
    boardSystem system;

    void Start()
    {
        system = GameObject.Find("Game Manager").GetComponent<boardSystem>();
    }

    public void transferInfo(shipInfo s)
    {
        type = s.type;
        team = s.team;
        if (team == 0)
        {
            xPos = s.xPos;
            if (s.xPos % 2 == 0) yPos = 6 - s.yPos; else yPos = 5 - s.yPos;
        }
        else
        {
            xPos = 32 - s.xPos;
            yPos = s.yPos;
        }
        Relocate();
    }

    public void HoverEnter()
    {
        if (system.mode_attack && attackable && system.selectedInfo.type != 2 && system.selectedInfo.type != 4)
        {
            cover.GetComponent<Light>().color = new Color(d, e, f);
        }
    }

    public void HoverExit()
    {
        if (system.mode_attack && attackable && system.selectedInfo.type != 2 && system.selectedInfo.type != 4)
        {
            cover.GetComponent<Light>().color = new Color(a, b, c);
        }
    }

    public void OnClick()
    {
        if (!system.menu.activeSelf)
        {
            system.UpdateMenu(this.transform.root.gameObject);
            system.menu.SetActive(true);
        }
        else if (attackable)
        {
            system.Attack(this.gameObject);
            OnDamage();
        }
    }

    public void OnMove(int xDel, int yDel)
    {
        xPos = xPos + xDel;
        yPos = yPos + yDel;
    }

    public void Relocate()
    {
        float x, z, k;
        if (type == 4) k = Convert.ToSingle(0.866);
        else if (type == 2 || type == 3) k = Convert.ToSingle(0.433);
        else k = 0;
        if (team == 0) k = k * (-1);
        x = Convert.ToSingle(xPos * 0.433 + k);
        z = Convert.ToSingle(yPos * 1.5);
        if (xPos % 2 == 1) z = z + Convert.ToSingle(0.75);
        this.transform.position = new Vector3(x, 0, z);
    }

    public void OnDamage()
    {
        currentHealth = currentHealth - 1;
        currentHealthBar.sizeDelta
        = new Vector2(currentHealthBar.sizeDelta.x, 0.5f * (Convert.ToSingle(currentHealth) / Convert.ToSingle(totalHealth)));
        healthCanvas.gameObject.SetActive(true);
        if (currentHealth <= 0) OnDeath();
    }

    public void OnDeath()
    {
        if (team == 0)
        {
            foreach (GameObject ship in system.ships_TOP)
            {
                if (GameObject.ReferenceEquals(ship, this.gameObject))
                {
                    system.ships_TOP.Remove(ship);
                    system.cnt_TOP--;
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject ship in system.ships_BOTTOM)
            {
                if (GameObject.ReferenceEquals(ship, this.gameObject))
                {
                    system.ships_BOTTOM.Remove(ship);
                    system.cnt_BOTTOM--;
                    break;
                }
            }
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (scriptonly) return;
        if (!system.menu.activeSelf || !system.mode_attack)
        {
            cover.GetComponent<Light>().enabled = false;
            attackable = false;
        }
        else if (!GameObject.ReferenceEquals(system.selectedInfo.gameObject, this.gameObject))
        {
            cover.GetComponent<Light>().enabled = false;
            selected = false;
        }
        if (attackable && system.mode_attack)
        {
            cover.GetComponent<Light>().enabled = true;
            cover.GetComponent<Light>().color = new Color(a, b, c);
        }
        else if (selected)
        {
            cover.GetComponent<Light>().enabled = true;
            cover.GetComponent<Light>().color = new Color(1, 1, 1);
        }
    }
}
