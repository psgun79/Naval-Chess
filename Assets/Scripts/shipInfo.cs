using System;
using UnityEngine;
using UnityEngine.UI;

public class shipInfo : MonoBehaviour
{
    float a = Convert.ToSingle(227.0 / 255.0);
    float b = Convert.ToSingle(89.0 / 255.0);
    float c = Convert.ToSingle(89.0 / 255.0);
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
    public int attackCount = 0;
    public bool attackable = false;
    public bool selected = false;
    public bool scriptonly;
    public Sprite health_1;
    public Sprite health_2;
    boardSystem system_1;
    turnSystem system_2;

    void Start()
    {
        system_1 = GameObject.Find("Game Manager").GetComponent<boardSystem>();
        system_2 = GameObject.Find("Game Manager").GetComponent<turnSystem>();
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
        if (system_1.mode_attack && attackable && system_1.selectedInfo.type != 2 && system_1.selectedInfo.type != 4)
        {
            cover.GetComponent<Light>().intensity = 10f;
        }
    }

    public void HoverExit()
    {
        if (system_1.mode_attack && attackable && system_1.selectedInfo.type != 2 && system_1.selectedInfo.type != 4)
        {
            cover.GetComponent<Light>().intensity = 5f;
        }
    }

    public void OnClick()
    {
        if (!system_1.menu.activeSelf && type != -1 && system_2.turn == team)
        {
            system_1.UpdateMenu(this.transform.root.gameObject);
            system_1.menu.SetActive(true);
        }
        else if (attackable && system_1.selectedInfo.type != 2 && system_1.selectedInfo.type != 4) system_1.Attack(this.gameObject);
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
        if (type == -1) return;
        float ratio = Convert.ToSingle(currentHealth) / Convert.ToSingle(totalHealth);
        currentHealthBar.sizeDelta = new Vector2(currentHealthBar.sizeDelta.x, 0.5f * ratio);
        healthCanvas.gameObject.SetActive(true);
        if (currentHealth == 1) currentHealthBar.GetComponent<Image>().sprite = health_2;
        else if (ratio <= 0.5f) currentHealthBar.GetComponent<Image>().sprite = health_1;
        if (currentHealth <= 0) OnDeath();
    }

    public void OnDeath()
    {
        if (team == 0)
        {
            foreach (GameObject ship in system_1.ships_TOP)
            {
                if (GameObject.ReferenceEquals(ship, this.gameObject))
                {
                    system_1.ships_TOP.Remove(ship);
                    system_1.cnt_TOP--;
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject ship in system_1.ships_BOTTOM)
            {
                if (GameObject.ReferenceEquals(ship, this.gameObject))
                {
                    system_1.ships_BOTTOM.Remove(ship);
                    system_1.cnt_BOTTOM--;
                    break;
                }
            }
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (scriptonly) return;
        if (!system_1.menu.activeSelf || !system_1.mode_attack)
        {
            cover.GetComponent<Light>().enabled = false;
            attackable = false;
        }
        else if (!GameObject.ReferenceEquals(system_1.selectedInfo.gameObject, this.gameObject))
        {
            cover.GetComponent<Light>().enabled = false;
            selected = false;
        }
        if (attackable && system_1.mode_attack)
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
