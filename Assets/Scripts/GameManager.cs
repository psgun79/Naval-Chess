using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera canvasCamera;
    public Camera boardCamera;
    public Material inGameSkybox;
    public Canvas infoScreen;
    public Canvas startingScreen;
    public Canvas teamUpScreen;
    public Canvas deployScreen;
    public Canvas HUD;
    public GameObject gameboard;
    public List<AudioSource> musicList;
    public int trackNum;

    public void openHowToPlay()
    {
        startingScreen.gameObject.SetActive(false);
        infoScreen.gameObject.SetActive(true);
    }

    public void closeHowToPlay()
    {
        infoScreen.gameObject.SetActive(false);
        startingScreen.gameObject.SetActive(true);
    }
    
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

    public void gameStart()
    {
        deployScreen.gameObject.SetActive(false);
        canvasCamera.gameObject.SetActive(false);
        boardCamera.gameObject.AddComponent<Skybox>().material = inGameSkybox;
        gameboard.SetActive(true);
        musicList[0].Stop();
        trackNum = Random.Range(1, 5);
        musicList[trackNum].Play();
        HUD.gameObject.SetActive(true);
        this.gameObject.GetComponent<boardSystem>().PutPieces(this.gameObject.GetComponent<teamDeploy>().deployment_TOP);
        this.gameObject.GetComponent<boardSystem>().PutPieces(this.gameObject.GetComponent<teamDeploy>().deployment_BOTTOM);
    }

    public void SceneReload()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
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
