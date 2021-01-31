using System.Collections.Generic;
using UnityEngine;

public class actionRangeCalc : MonoBehaviour
{
    public boardSystem system;

    public void attackRangeCalculation()
    {
        List<GameObject> enemies = (system.selectedInfo.team == 0) ? system.ships_BOTTOM : system.ships_TOP;
        int k = (system.selectedInfo.team == 0) ? 2 : -2;
        bool even = system.selectedInfo.xPos % 2 == 0;
        switch (system.selectedInfo.type)
        {
            case 0:
                foreach (GameObject e in enemies)
                {
                    bool withinRange = false;
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - system.selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = x - system.selectedInfo.xPos;
                        switch (y_gap)
                        {
                            case 0:
                                if (Mathf.Abs(x_gap) <= 6) withinRange = true;
                                break;
                            case -1:
                                if (even && Mathf.Abs(x_gap) <= 5) withinRange = true;
                                else if (!even && Mathf.Abs(x_gap) <= 4) withinRange = true;
                                break;
                            case 1:
                                if (even && Mathf.Abs(x_gap) <= 4) withinRange = true;
                                else if (!even && Mathf.Abs(x_gap) <= 5) withinRange = true;
                                break;
                            default:
                                if (even && y_gap != -2) continue;
                                else if (!even && y_gap != 2) continue;
                                else if (Mathf.Abs(x_gap) == 1 || Mathf.Abs(x_gap) == 3) withinRange = true;
                                break;
                        }
                        if (withinRange) {
                            system.attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
            case 1:
                foreach (GameObject e in enemies)
                {
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int x_gap = Mathf.Abs(enemyInfo.xPos - system.selectedInfo.xPos);
                    int y_gap = Mathf.Abs(enemyInfo.yPos - system.selectedInfo.yPos);
                    if (y_gap != 0) continue;
                    else if (x_gap == 2 || x_gap == 4 || x_gap == 6) system.attackable.Add(e);
                }
                break;
            case 2:
                List<GameObject> temp_2;
                for (int i = 0; i < 2; i++)
                {
                    int k_2 = (i == 0) ? -2 : 2;
                    temp_2 = (i == 0) ? system.ships_TOP : system.ships_BOTTOM;
                    foreach (GameObject e in temp_2)
                    {
                        if (GameObject.ReferenceEquals(system.selectedInfo.gameObject, e)) continue;
                        bool withinRange = false;
                        shipInfo targetInfo = e.GetComponent<shipInfo>();
                        int y = targetInfo.yPos;
                        int y_gap = y - system.selectedInfo.yPos;
                        for (int j = 0; j < targetInfo.len; j++)
                        {
                            int x = targetInfo.xPos + k_2 * j;
                            int x_gap = x - system.selectedInfo.xPos;
                            switch (y_gap)
                            {
                                case 0:
                                    if (Mathf.Abs(x_gap) <= 4) withinRange = true;
                                    break;
                                case -1:
                                    if (even && Mathf.Abs(x_gap) <= 3) withinRange = true;
                                    else if (!even && (x_gap == 0 || Mathf.Abs(x_gap) == 2)) withinRange = true;
                                    break;
                                default:
                                    if (even && y_gap != -1) continue;
                                    else if (!even && y_gap == 1 && Mathf.Abs(x_gap) <= 3) withinRange = true;
                                    break;
                            }
                            if (withinRange) {
                                system.attackable.Add(e);
                                break;
                            }
                        }
                    }
                }
                break;
            case 3:
                foreach (GameObject e in enemies)
                {
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - system.selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = Mathf.Abs(x - system.selectedInfo.xPos);
                        if (even && (y_gap == x_gap / 2 || y_gap == -((x_gap - 1) / 2) - 1) && x_gap <= 7)
                        {
                            system.attackable.Add(e);
                            break;
                        }
                        else if (!even && (y_gap == -(x_gap / 2) || y_gap == (x_gap + 1) / 2) && x_gap <= 7)
                        {
                            system.attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
            default:
                List<GameObject> temp_4;
                for (int i = 0; i < 2; i++)
                {
                    int k_4 = (i == 0) ? -2 : 2;
                    temp_4 = (i == 0) ? system.ships_TOP : system.ships_BOTTOM;
                    foreach (GameObject e in temp_4)
                    {
                        if (GameObject.ReferenceEquals(system.selectedInfo.gameObject, e)) continue;
                        bool withinRange = false;
                        shipInfo targetInfo = e.GetComponent<shipInfo>();
                        int y = targetInfo.yPos;
                        int y_gap = y - system.selectedInfo.yPos;
                        for (int j = 0; j < targetInfo.len; j++)
                        {
                            int x = targetInfo.xPos + k_4 * j;
                            int x_gap = x - system.selectedInfo.xPos;
                            switch (y_gap)
                            {
                                case 0:
                                    if (-4 <= x_gap && x_gap <= 1) withinRange = true;
                                    break;
                                case -1:
                                    if (even && -3 <= x_gap && x_gap <= 2) withinRange = true;
                                    else if (!even && -2 <= x_gap && x_gap <= 3) withinRange = true;
                                    break;
                                case 1:
                                    if (even && -2 <= x_gap && x_gap <= 3) withinRange = true;
                                    else if (!even && -3 <= x_gap && x_gap <= 2) withinRange = true;
                                    break;
                                default:
                                    if (even && y_gap != -2) continue;
                                    else if (!even && y_gap != 2) continue;
                                    else if (x_gap == -1 || x_gap == 1 || x_gap == 3) withinRange = true;
                                    break;
                            }
                            if (withinRange) {
                                system.attackable.Add(e);
                                break;
                            }
                        }
                    }
                }
                break;
        }
    }

    public void moveRangeCalculation()
    {
        int k = (system.selectedInfo.team == 0) ? 1 : -1;
        bool even = system.selectedInfo.xPos % 2 == 0;
        Transform option_1 = null;
        Transform option_2 = null;
        Transform option_3 = null;
        string x_plus_one = "Row_" + (system.selectedInfo.xPos + 1 + k).ToString();
        string x_plus_two = "Row_" + (system.selectedInfo.xPos + 1 + 2 * k).ToString();
        string x_plus_four = "Row_" + (system.selectedInfo.xPos + 1 + 4 * k).ToString();
        string y_minus_one = "Cell_" + (system.selectedInfo.yPos).ToString();
        string y_zero = "Cell_" + (system.selectedInfo.yPos + 1).ToString();
        string y_plus_one = "Cell_" + (system.selectedInfo.yPos + 2).ToString();
        switch (system.selectedInfo.type)
        {
            case 1:
                if (even)
                {
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos - 1)
                    && OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos - 1))
                        option_1 = system.board.transform.Find(x_plus_two).Find(y_minus_one);
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos)
                    && OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos + 1))
                        option_3 = system.board.transform.Find(x_plus_two).Find(y_plus_one);
                }
                else
                {
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos)
                    && OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos - 1))
                        option_1 = system.board.transform.Find(x_plus_two).Find(y_minus_one);
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos + 1)
                    && OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos + 1))
                        option_3 = system.board.transform.Find(x_plus_two).Find(y_plus_one);
                }
                if (OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos, true)
                && OccupationCheck(system.selectedInfo.xPos + 4 * k, system.selectedInfo.yPos, true))
                    option_2 = system.board.transform.Find(x_plus_four).Find(y_zero);
                break;
            default:
                if (even)
                {
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos - 1)) option_1 = system.board.transform.Find(x_plus_one).Find(y_minus_one);
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos)) option_3 = system.board.transform.Find(x_plus_one).Find(y_zero);
                }
                else
                {
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos)) option_1 = system.board.transform.Find(x_plus_one).Find(y_zero);
                    if (OccupationCheck(system.selectedInfo.xPos + k, system.selectedInfo.yPos + 1)) option_3 = system.board.transform.Find(x_plus_one).Find(y_plus_one);
                }
                if (OccupationCheck(system.selectedInfo.xPos + 2 * k, system.selectedInfo.yPos, true)) option_2 = system.board.transform.Find(x_plus_two).Find(y_zero);
                break;
        }
        if (option_1 != null) system.movable.Add(option_1.gameObject);
        if (option_2 != null) system.movable.Add(option_2.gameObject);
        if (option_3 != null) system.movable.Add(option_3.gameObject);
    }

    bool OccupationCheck(int x, int y, bool straight = false)
    {
        if ((x == 0 || x == 32) && y == 3) return false;
        int len = system.selectedInfo.len;
        int k = (system.selectedInfo.team == 0) ? -2 : 2; 
        foreach (GameObject s in system.ships_TOP)
        {
            shipInfo info = s.GetComponent<shipInfo>();
            for (int i = 0; i < info.len; i++)
            {
                if (x == info.xPos - i * 2 && y == info.yPos) return false;
            }
            if (!straight)
            {
                for (int i = 0; i < len; i++)
                {
                    if (x + i * k == info.xPos && y == info.yPos) return false;
                }
            }

        }
        foreach (GameObject s in system.ships_BOTTOM)
        {
            shipInfo info = s.GetComponent<shipInfo>();
            for (int i = 0; i < info.len; i++)
            {
                if (x == info.xPos + i * 2 && y == info.yPos) return false;
            }
            if (!straight)
            {
                for (int i = 0; i < len; i++)
                {
                    if (x + i * k == info.xPos && y == info.yPos) return false;
                }
            }
        }
        return true;
    }
}
