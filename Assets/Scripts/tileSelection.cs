using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class tileSelection : MonoBehaviour
{
    public int xPos;
    public int yPos;
    float a = Convert.ToSingle(160.0 / 255.0);
        float b = Convert.ToSingle(205.0 / 255.0);
        float c = Convert.ToSingle(199.0 / 255.0);
    public teamDeploy system;

    public void HoverEnter()
    {
        if (system.selected != -1) this.gameObject.GetComponent<Image>().color = new Color(a, b, c);
    }

    public void HoverExit()
    {
        this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public void Click()
    {
        int i, len, origin;
        if (system.lock_table[xPos, yPos, 0] != -1)
        {
            if (system.selected == -1)
            {
                // 유닛 회수
                // #1. lock_table 첫번째 값(lock: 0~2) 및 두번째 값(type: 0~4)을 확인해서 앞뒤로 lock, type 초기화
                if (system.lock_table[xPos, yPos, 1] <= 1) len = 1;
                else if (system.lock_table[xPos, yPos, 1] <= 3) len = 2;
                else len = 3;
                origin = xPos - system.lock_table[xPos, yPos, 0] * 2;

                for (i = xPos - system.lock_table[xPos, yPos, 0] * 2;
                i <= xPos + 2 * (len - system.lock_table[xPos, yPos, 0] - 1);
                i = i + 2)
                {
                    system.lock_table[i, yPos, 0] = -1;
                    system.lock_table[i, yPos, 1] = -1;
                }

                // #2. 뱃머리 타일의 좌표를 계산해서 teamDeploy에서 struct 리스트를 iterate, 리스트에서 찾은 배 정보 삭제
                system.Remove(origin, yPos);

                // #3. 화면에 떠 있는 배 그림 삭제
            }
            else return;
        }
        else {
            // 유닛 배치
            // #1. 유닛이 들어갈 수 있는지 (이미 배가 있는지 또는 공간이 충분한지) 확인
            if (system.lock_table[xPos, yPos, 1] <= 1) len = 1;
            else if (system.lock_table[xPos, yPos, 1] <= 3) len = 2;
            else len = 3;
            // TODO: 유닛이 들어갈수 있는지 확인하는 것부터 시작해서 Click() 함수 완성시키기

            // #2. cnt 감소, ships에서 해당 배 수량 감소, ships_deployed에 추가, selected -1로 수정 (Add 함수)
            system.Add(xPos, yPos);

            // #3. 배 그림 삽입
            // #4. lock_table 값 수정 (type에 따라 len 구하고 lock 0부터 아래 타일까지 전부 적용)
        }
    }

    void Update()
    {
        if (system.selected == -1) this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }
}
