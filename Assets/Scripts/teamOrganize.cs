using System;
using UnityEngine;
using UnityEngine.UI;

public class teamOrganize : MonoBehaviour
{
    public int team = 0;
    int cnt = 0;
    int budget = 15;
    public int money = 15;
    public int[,] ships = new int[,] {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};
    public int[] limit = new int[] {7, 4, 4, 2, 1};
    public int[] cost = new int[] {1, 2, 2, 3, 5};
    public Text count_0;
    public Text count_1;
    public Text count_2;
    public Text count_3;
    public Text count_4;
    public Text team_text;
    public Text money_text;
    public GameObject message;
    public GameManager system;

    void Start()
    {
        message.SetActive(false);
        UpdateCount();
    }

    void UpdateCount()
    {
        count_0.GetComponent<Text>().text = ships[team, 0].ToString();
        count_1.GetComponent<Text>().text = ships[team, 1].ToString();
        count_2.GetComponent<Text>().text = ships[team, 2].ToString();
        count_3.GetComponent<Text>().text = ships[team, 3].ToString();
        count_4.GetComponent<Text>().text = ships[team, 4].ToString();
        money_text.GetComponent<Text>().text = money.ToString();
    }

    public void Increment(int type)
    {
        if (ships[team, type] + 1 <= limit[type] && money - cost[type] >= 0)
        {
            if (message.activeSelf) message.SetActive(false);
            cnt++;
            ships[team, type]++;
            money = money - cost[type];
            UpdateCount();
        }
    }

    public void Decrement(int type)
    {
        if (ships[team, type] - 1 >= 0)
        {
            cnt--;
            ships[team, type]--;
            money = money + cost[type];
            UpdateCount();
        }
    }

    public void Finish()
    {
        if (cnt == 0)
        {
            message.SetActive(true);
        }
        else if (team == 0)
        {
            message.SetActive(false);
            money = budget;
            cnt = 0;
            team = 1;
            team_text.GetComponent<Text>().text = "BOTTOM";
            team_text.GetComponent<Text>().color = new Color(0, Convert.ToSingle(29.0 / 255.0), Convert.ToSingle(144.0 / 255.0));
            UpdateCount();
        }
        else
        {
            system.deployStart();
        }
    }
}
