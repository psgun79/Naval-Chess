using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas startingScreen;
    public Canvas teamUpScreen;
    public Canvas HUD;
    public Camera mainCamera;
    public GameObject gameboard;

    public void gameStart()
    {
        startingScreen.gameObject.SetActive(false);
        teamUpScreen.gameObject.SetActive(true);
    }

    public void gameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    /*
    public Transform prefab;

    public void spawn() //임시 함수
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    */
}
