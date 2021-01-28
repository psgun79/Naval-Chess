using System;
using UnityEngine;

public class shipInfo : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;
    public int AP = 3;
    public int team;
    public int xPos;
    public int yPos;
    public int type;
    public Transform icon;
    public RectTransform currentHealthBar;
    public RectTransform totalHealthBar;

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
        int tmp;
        if (type == 0) tmp = 2;
        else if (type == 2) tmp = 4;
        else if (type == 4) tmp = 5;
        else tmp = 3;
        currentHealth = tmp;
        totalHealth = tmp;
        Relocate();
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
