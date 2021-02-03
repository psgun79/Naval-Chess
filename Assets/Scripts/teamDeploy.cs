using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class teamDeploy : MonoBehaviour
{
    public List<GameObject> ships_deployed = new List<GameObject>();
    public List<GameObject> deployment_TOP;
    public List<GameObject> deployment_BOTTOM;
    public Text team_text;
    public int team = 0;
    int cnt;
    public int[,] ships;
    public int[,,] lock_table = new int[9, 7, 2];
    public int selected = -1;
    public List<Transform> icons = new List<Transform>();
    public List<Text> texts = new List<Text>();
    public List<GameObject> buttons = new List<GameObject>();
    public GameObject scriptOnly;
    public GameObject message;
    public GameObject board;
    public GameManager system;

    void Start()
    {
        message.SetActive(false);
        int i, j, k;
        for (i = 0; i < lock_table.GetLength(0); i++)
        {
            for (j = 0; j < lock_table.GetLength(1); j++)
            {
                for (k = 0; k < lock_table.GetLength(2); k++)
                {
                    lock_table[i, j, k] = -1;
                }
            }
        }
    }

    public void UpdateCount()
    {
        int tmp = 0;
        for (int i = 0; i < ships.GetLength(1); i++)
        {
            if (ships[team, i] == 0) buttons[i].GetComponent<ButtonUpdate>().Lock();
            else buttons[i].GetComponent<ButtonUpdate>().LightOff();
            texts[i].GetComponent<Text>().text = ships[team, i].ToString();
            tmp += ships[team, i];
        }
        cnt = tmp;
    }

    public void unitSelect(int type)
    {
        if (selected == -1 && ships[team, type] > 0)
        {
            selected = type;
            buttons[type].GetComponent<ButtonUpdate>().LightOn();
        }
        else if (selected != type && ships[team, type] > 0)
        {
            buttons[selected].GetComponent<ButtonUpdate>().LightOff();
            selected = type;
            buttons[selected].GetComponent<ButtonUpdate>().LightOn();
        }
        else
        {
            if (selected != -1) buttons[type].GetComponent<ButtonUpdate>().LightOff();
            selected = -1;
        }
    }

    public void Add(int xPos, int yPos)
    {
        message.SetActive(false);
        cnt--;
        GameObject s = Instantiate(scriptOnly);
        s.GetComponent<shipInfo>().xPos = xPos;
        s.GetComponent<shipInfo>().yPos = yPos;
        s.GetComponent<shipInfo>().type = selected;
        s.GetComponent<shipInfo>().team = team;
        s.GetComponent<shipInfo>().icon = Instantiate(icons[selected], board.transform, false);
        s.GetComponent<shipInfo>().icon.localPosition = Position(xPos, yPos, selected);
        ships_deployed.Add(s);
        ships[team, selected]--;
        UpdateCount();
    }

    public void Remove(int xPos, int yPos, int mode)
    {
        if (mode == 0)
        {
            foreach (GameObject s in ships_deployed)
            {
                if (s.GetComponent<shipInfo>().xPos == xPos && s.GetComponent<shipInfo>().yPos == yPos)
                {
                    ships[team, s.GetComponent<shipInfo>().type]++;
                    Destroy(s.GetComponent<shipInfo>().icon.gameObject);
                    cnt++;
                    ships_deployed.Remove(s);
                    Destroy(s);
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject s in ships_deployed)
            {
                Destroy(s.GetComponent<shipInfo>().icon.gameObject);
            }
        }
        UpdateCount();
    }

    Vector3 Position(int xPos, int yPos, int type)
    {
        float x, y, k;
        if (type == 4) k = Convert.ToSingle(-35.2);
        else if (type == 2 || type == 3) k = Convert.ToSingle(-17.6);
        else k = 0;
        if (xPos % 2 == 0)
        {
            x = Convert.ToSingle((yPos * 67.9) - 203.7);
            y = Convert.ToSingle((xPos / 2 * 35.2) - 120 + k);
        }
        else
        {
            x = Convert.ToSingle((yPos * 67.9) - 169.8);
            y = Convert.ToSingle(((xPos - 1) / 2 * 35.2) - 102.4 + k);
        }
        Vector3 cell_pos = new Vector3();
        cell_pos.x = x;
        cell_pos.y = y;
        cell_pos.z = 0;
        return cell_pos;
    }

    public void Finish()
    {
        if (cnt != 0)
        {
            message.SetActive(true);
        }
        else if (team == 0)
        {
            message.SetActive(false);
            team++;
            team_text.GetComponent<Text>().text = "BOTTOM";
            team_text.GetComponent<Text>().color = new Color(0, Convert.ToSingle(29.0 / 255.0), Convert.ToSingle(144.0 / 255.0));
            deployment_TOP = ships_deployed.ToList();
            Remove(0, 0, 1);
            ships_deployed = new List<GameObject>();
            int i, j, k;
            for (i = 0; i < lock_table.GetLength(0); i++)
            {
                for (j = 0; j < lock_table.GetLength(1); j++)
                {
                    for (k = 0; k < lock_table.GetLength(2); k++)
                    {
                        lock_table[i, j, k] = -1;
                    }
                }
            }
            UpdateCount();
        }
        else
        {
            deployment_BOTTOM = ships_deployed.ToList();
            Remove(0, 0, 1);
            system.gameStart();
        }
    }
}
