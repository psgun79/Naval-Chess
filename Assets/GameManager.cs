using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas startingScreen;
    public Canvas HUD;
    public Camera mainCamera;
    public GameObject gameboard;

    public void gameStart()
    {
        startingScreen.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        HUD.gameObject.SetActive(true);
        gameboard.SetActive(true);
    }

    public void gameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
