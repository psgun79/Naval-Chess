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
    int team = 0;
    int cnt;
    public int[,] ships;
    public int[,,] lock_table = new int[9, 7, 2];
    public int selected = -1;
    public List<Transform> icons = new List<Transform>();
    public List<Text> texts = new List<Text>();
    public List<GameObject> arrows = new List<GameObject>();
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
            LightOn(selected);
        }
        else if (selected != type && ships[team, type] > 0)
        {
            LightOff(selected);
            selected = type;
            LightOn(selected);
        }
        else
        {
            if (selected != -1) LightOff(selected);
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
        var coordinates = Position(xPos, yPos, selected);
        s.GetComponent<shipInfo>().icon = Instantiate(icons[selected], board.transform, false);
        s.GetComponent<shipInfo>().icon.localPosition = new Vector3(coordinates.Item1, coordinates.Item2, 0);
        ships_deployed.Add(s);
        ships[team, selected]--;
        LightOff(selected);
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

    (float, float) Position(int xPos, int yPos, int type)
    {
        float x, y, k;
        if (type == 4) k = Convert.ToSingle(-49.5);
        else if (type == 2 || type == 3) k = Convert.ToSingle(-24.75);
        else k = 0;
        if (xPos % 2 == 0)
        {
            x = Convert.ToSingle((yPos * 90) - 129);
            y = Convert.ToSingle((xPos / 2 * 49.5) - 59 + k);
        }
        else
        {
            x = Convert.ToSingle((yPos * 90) - 84);
            y = Convert.ToSingle(((xPos - 1) / 2 * 49.5) - 34.25 + k);
        }
        return (x, y);
    }

    void LightOn(int type)
    {
        arrows[type].SetActive(true);
    }

    void LightOff(int type)
    {
        arrows[type].SetActive(false);
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
            team_text.GetComponent<Text>().text = "<color=#0000ffff>BOTTOM</color>";
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
