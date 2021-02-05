using System;
using UnityEngine;
using UnityEngine.UI;

public class turnSystem : MonoBehaviour
{
    public GameManager gm;
    public boardSystem system;
    public Text team;
    public Text totalAP_text;
    public Image AP_image;
    public Camera canvasCamera;
    public Camera mainCamera;
    public Canvas gameOverCanvas;
    public GameObject win_TOP;
    public GameObject win_BOTTOM;
    public GameObject light_TOP;
    public GameObject light_BOTTOM;
    public GameObject base_TOP;
    public GameObject base_BOTTOM;
    public int totalAP = 4;
    public int turn = 0;

    public void SwitchTurn()
    {
        if (turn == 0) turn = 1; else turn = 0;
        system.CancelButtonClick();
        MonitorUpdate();
    }

    public void MonitorUpdate()
    {
        totalAP = 4;
        APUpdate();
        if (turn == 0)
        {
            team.text = "TOP";
            team.color = new Color(Convert.ToSingle(202.0 / 255.0), Convert.ToSingle(45.0 / 255.0), Convert.ToSingle(45.0 / 255.0));
            AP_image.color = new Color(Convert.ToSingle(202.0 / 255.0), Convert.ToSingle(45.0 / 255.0), Convert.ToSingle(45.0 / 255.0));
            light_TOP.SetActive(true);
            light_BOTTOM.SetActive(false);
            foreach (GameObject ship in system.ships_TOP)
            {
                ship.GetComponent<shipInfo>().attackCount = 0;
                ship.GetComponent<shipInfo>().AP = 3;
            }
        }
        else
        {
            team.text = "BOTTOM";
            team.color = new Color(0, Convert.ToSingle(29.0 / 255.0), Convert.ToSingle(144.0 / 255.0));
            AP_image.color = new Color(0, Convert.ToSingle(29.0 / 255.0), Convert.ToSingle(144.0 / 255.0));
            light_TOP.SetActive(false);
            light_BOTTOM.SetActive(true);
            foreach (GameObject ship in system.ships_BOTTOM)
            {
                ship.GetComponent<shipInfo>().attackCount = 0;
                ship.GetComponent<shipInfo>().AP = 3;
            }
        }
    }

    public void APUpdate(int reachedEnd = -1)
    {
        totalAP_text.text = totalAP.ToString();
        int winCondition = Winner(reachedEnd);
        if (winCondition != -1)
        {
            system.monitor.SetActive(false);
            system.menu.SetActive(false);
            mainCamera.gameObject.SetActive(false);
            canvasCamera.gameObject.SetActive(true);
            gameOverCanvas.gameObject.SetActive(true);
            gm.musicList[gm.trackNum].Stop();
            gm.musicList[0].Play();
            if (winCondition == 0) win_TOP.SetActive(true); else win_BOTTOM.SetActive(true);
        }
        else if (totalAP == 0) SwitchTurn();
    }

    public int Winner(int reachedEnd = -1)
    {
        if (base_TOP.GetComponent<shipInfo>().currentHealth == 0) return 1;
        else if (base_BOTTOM.GetComponent<shipInfo>().currentHealth == 0) return 0;
        else if (system.cnt_TOP == 0) return 1;
        else if (system.cnt_BOTTOM == 0) return 0;
        else return reachedEnd;
    }
}