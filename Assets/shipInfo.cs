using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipInfo : MonoBehaviour
{
    public int currentHealth = 2;
    public int totalHealth = 2;
    public int team = 1;
    public RectTransform currentHealthBar;
    public RectTransform totalHealthBar;

    public void Damaged()
    {
        // RectTransform.sizeDelta = new Vector2(원래 x방향 길이 * (현재 체력 / 최대 체력) , 원래 y방향 길이);
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
