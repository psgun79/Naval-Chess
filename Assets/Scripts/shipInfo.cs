using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipInfo : MonoBehaviour
{
    public int currentHealth = 2;
    public int totalHealth = 2;
    public int team = 1;
    public int xPos = -1;
    public int yPos = -1;
    public int type = 0;
    public RectTransform currentHealthBar;
    public RectTransform totalHealthBar;

    public void OnSpawn(int x, int y)
    {
        xPos = x;
        yPos = y;
        Relocate();
    }

    public void OnMove(int xDel, int yDel)
    {
        xPos = xPos + xDel;
        yPos = yPos + yDel;
    }

    public void Relocate()
    {
        this.transform.position = new Vector3();
        // xPos, yPos로 몇 번째 row/column 인지 계산하고 그에 해당하는 타일 위치 찾아냄
    }

    public void OnShoot(int amount)
    {
        currentHealth = currentHealth - amount;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
        currentHealthBar.sizeDelta = new Vector2(); // new Vector2(원래 x방향 길이 * (현재 체력 / 최대 체력) , 원래 y방향 길이);
    }

    public void OnDeath()
    {
        // 인스턴스 삭제
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
