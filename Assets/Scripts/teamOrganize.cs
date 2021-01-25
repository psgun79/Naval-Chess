using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teamOrganize : MonoBehaviour
{
    public int team = 0;
    int cnt = 0; // 1개 이상 고르지 않으면 시작하지 못함
    int budget = 15;
    int money = 15;
    public int[,] ships = new int[,] {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};
    int[] limit = new int[] {7, 4, 4, 2, 1};
    int[] cost = new int[] {1, 2, 2, 3, 5};
    public Text count_0;
    public Text count_1;
    public Text count_2;
    public Text count_3;
    public Text count_4;
    public Text team_text;
    public Text money_text;

    void Start()
    {
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
        // 최대 제한을 넘기지 않으면서 돈이 부족하지 않을 때
        if (ships[team, type] + 1 <= limit[type] && money - cost[type] >= 0)
        {
            cnt++;
            ships[team, type]++;
            money = money - cost[type];
            UpdateCount();
        }
    }

    public void Decrement(int type)
    {
        // 배의 개수가 음수가 되지 않을 때
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
            // 배를 1개 이상 구매하지 않으면 시작할 수 없다는 메시지 출력
        }
        else if (team == 0)
        {
            money = budget;
            cnt = 0;
            team = 1;
            team_text.GetComponent<Text>().text = "<color=#0000ffff>BOTTOM</color>";
            UpdateCount();
        }
        else
        {
            // 게임 시작을 위해 GameManager로 권한 넘김
        }
    }
}
