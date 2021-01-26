using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas startingScreen;
    public Canvas teamUpScreen;
    public Canvas deployScreen;
    public Canvas HUD;
    public Camera mainCamera;
    public GameObject gameboard;

    public void teamUpStart()
    {
        startingScreen.gameObject.SetActive(false);
        teamUpScreen.gameObject.SetActive(true);
    }

    public void deployStart()
    {
        teamUpScreen.gameObject.SetActive(false);
        deployScreen.gameObject.SetActive(true);
        this.gameObject.GetComponent<teamDeploy>().ships = this.gameObject.GetComponent<teamOrganize>().ships;
        this.gameObject.GetComponent<teamDeploy>().UpdateCount();
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
