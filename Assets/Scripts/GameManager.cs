using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas startingScreen;
    public Canvas teamUpScreen;
    public Canvas deployScreen;
    public Canvas HUD;
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

    public void gameStart()
    {
        deployScreen.gameObject.SetActive(false);
        gameboard.SetActive(true);
        HUD.gameObject.SetActive(true);
        this.gameObject.GetComponent<boardSystem>().PutPieces(this.gameObject.GetComponent<teamDeploy>().deployment_TOP);
        this.gameObject.GetComponent<boardSystem>().PutPieces(this.gameObject.GetComponent<teamDeploy>().deployment_BOTTOM);
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
