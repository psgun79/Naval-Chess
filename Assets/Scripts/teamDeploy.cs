using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class teamDeploy : MonoBehaviour
{
    /*public struct deployInfo // 이후 게임 시작할 때 유닛 배치에 활용할 예정
    {
        public int xPos;
        public int yPos;
        public int type;
        public Transform icon;
    }*/
    public List<GameObject> ships_deployed = new List<GameObject>();
    public List<GameObject> deployment_TOP;
    public List<GameObject> deployment_BOTTOM;
    public GameObject image_0;
    public GameObject image_1;
    public GameObject image_2;
    public GameObject image_3;
    public GameObject image_4;
    public GameObject arrow_0;
    public GameObject arrow_1;
    public GameObject arrow_2;
    public GameObject arrow_3;
    public GameObject arrow_4;
    public Text count_0;
    public Text count_1;
    public Text count_2;
    public Text count_3;
    public Text count_4;
    public Text team_text;
    int team = 0;
    int cnt;
    public int[,] ships; // 일회용 리스트
    public int[,,] lock_table = new int[9, 7, 2]; // 가로, 세로, (lock 0~2 , type 0~4)
    public int selected = -1;
    public List<Transform> icons = new List<Transform>();
    public Transform icon_0;
    public Transform icon_1;
    public Transform icon_2;
    public Transform icon_3;
    public Transform icon_4;
    public GameObject scriptOnly;
    public GameObject message;
    public GameObject board;
    public GameManager system;

    void Start()
    {
        icons.Add(icon_0);
        icons.Add(icon_1);
        icons.Add(icon_2);
        icons.Add(icon_3);
        icons.Add(icon_4);
        message.SetActive(false);
        arrow_0.SetActive(false);
        arrow_1.SetActive(false);
        arrow_2.SetActive(false);
        arrow_3.SetActive(false);
        arrow_4.SetActive(false);
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
        count_0.GetComponent<Text>().text = ships[team, 0].ToString();
        count_1.GetComponent<Text>().text = ships[team, 1].ToString();
        count_2.GetComponent<Text>().text = ships[team, 2].ToString();
        count_3.GetComponent<Text>().text = ships[team, 3].ToString();
        count_4.GetComponent<Text>().text = ships[team, 4].ToString();
        int tmp = 0;
        for (int i = 0; i < ships.GetLength(1); i++)
        {
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
            LightOff(selected);
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
        switch (type)
        {
            case 0:
                arrow_0.SetActive(true);
                break;
            case 1:
                arrow_1.SetActive(true);
                break;
            case 2:
                arrow_2.SetActive(true);
                break;
            case 3:
                arrow_3.SetActive(true);
                break;
            default:
                arrow_4.SetActive(true);
                break;
        }
    }

    void LightOff(int type)
    {
        switch (type)
        {
            case 0:
                arrow_0.SetActive(false);
                break;
            case 1:
                arrow_1.SetActive(false);
                break;
            case 2:
                arrow_2.SetActive(false);
                break;
            case 3:
                arrow_3.SetActive(false);
                break;
            default:
                arrow_4.SetActive(false);
                break;
        }
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
