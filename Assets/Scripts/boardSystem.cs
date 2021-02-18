using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boardSystem : MonoBehaviour
{
    public actionRangeCalc calc;
    public turnSystem system;
    public List<AudioSource> SFXList;
    public List<GameObject> prefabs_TOP = new List<GameObject>();
    public List<GameObject> prefabs_BOTTOM = new List<GameObject>();
    public List<GameObject> ships_TOP = new List<GameObject>();
    public List<GameObject> ships_BOTTOM = new List<GameObject>();
    public List<GameObject> attackable = new List<GameObject>();
    public List<GameObject> movable = new List<GameObject>();
    public GameObject monitor;
    public GameObject board;
    public GameObject menu;
    public GameObject shutdown_button;
    public GameObject shutdown_window;
    public GameObject attack_available;
    public GameObject attack_pending;
    public GameObject attack_unable;
    public GameObject move_available;
    public GameObject move_pending;
    public GameObject move_unable;
    public shipInfo selectedInfo;
    public Text selectedType;
    public Text selectedAP;
    public Camera mainCamera;
    public int cnt_TOP = 0;
    public int cnt_BOTTOM = 0;
    public bool mode_attack = false;
    public bool mode_move = false;

    public void PutPieces(List<GameObject> list)
    {
        foreach (GameObject s in list)
        {
            if (s.GetComponent<shipInfo>().team == 0)
            {
                GameObject ship = Instantiate(prefabs_TOP[s.GetComponent<shipInfo>().type]);
                ship.GetComponent<shipInfo>().transferInfo(s.GetComponent<shipInfo>());
                ships_TOP.Add(ship);
                cnt_TOP++;
                Destroy(s);
            }
            else
            {
                GameObject ship = Instantiate(prefabs_BOTTOM[s.GetComponent<shipInfo>().type]);
                ship.GetComponent<shipInfo>().transferInfo(s.GetComponent<shipInfo>());
                ships_BOTTOM.Add(ship);
                cnt_BOTTOM++;
                Destroy(s);
            }
        }
    }

    public void AttackButtonClick()
    {
        QuitWindowClose();
        move_pending.SetActive(false);
        if (selectedInfo.type == 2 || selectedInfo.type == 4)
        {
            foreach (GameObject ship in attackable) Attack(ship);
            return;
        }
        if (!mode_attack)
        {
            mode_attack = true;
            attack_pending.SetActive(true);
            foreach (GameObject ship in attackable) ship.GetComponent<shipInfo>().attackable = true;
            if (selectedInfo.type == 3) mainCamera.gameObject.GetComponent<cameraMovement>().sniping = true;
        }
        else
        {
            mode_attack = false;
            attack_pending.SetActive(false);
            foreach (GameObject ship in attackable) ship.GetComponent<shipInfo>().attackable = false;
            mainCamera.gameObject.GetComponent<cameraMovement>().sniping = false;
        }
    }

    public void Attack(GameObject target)
    {
        if (selectedInfo.type == 0 || selectedInfo.type == 1) SFXList[2].Play();
        else if (selectedInfo.type == 2 || selectedInfo.type == 4) SFXList[3].Play();
        else SFXList[4].Play();
        foreach (ParticleSystem p in selectedInfo.attackFXs) p.Play();
        mainCamera.gameObject.GetComponent<cameraMovement>().sniping = false;
        target.GetComponent<shipInfo>().OnDamage();
        selectedInfo.AP--;
        selectedInfo.attackCount++;
        mode_attack = false;
        selectedInfo.selected = false;
        menu.SetActive(false);
        system.totalAP--;
        system.APUpdate();
    }

    public void MoveButtonClick()
    {
        QuitWindowClose();
        attack_pending.SetActive(false);
        if (!mode_move)
        {
            mode_move = true;
            move_pending.SetActive(true);
            foreach (GameObject tile in movable)
            {
                tile.GetComponent<gameTileSelection>().movable = true;
                tile.GetComponent<gameTileSelection>().highlightOff();
            }
        }
        else
        {
            mode_move = false;
            move_pending.SetActive(false);
            foreach (GameObject tile in movable)
            {
                tile.GetComponent<gameTileSelection>().movable = false;
                tile.GetComponent<gameTileSelection>().highlightDisabled();
            }
        }
    }

    public void Move(int x, int y)
    {
        SFXList[5].Play();
        foreach (GameObject tile in movable)
        {
            tile.GetComponent<gameTileSelection>().movable = false;
            tile.GetComponent<gameTileSelection>().highlightDisabled();
        }
        selectedInfo.xPos = x;
        selectedInfo.yPos = y;
        int reachedEnd = -1;
        if (selectedInfo.team == 0 && x == 32) reachedEnd = 0;
        else if (selectedInfo.team == 1 && x == 0) reachedEnd = 1;
        selectedInfo.Relocate();
        selectedInfo.AP--;
        mode_move = false;
        selectedInfo.selected = false;
        menu.SetActive(false);
        system.totalAP--;
        system.APUpdate(reachedEnd);
    }

    public void CancelButtonClick()
    {
        QuitWindowClose();
        foreach (GameObject tile in movable)
        {
            tile.GetComponent<gameTileSelection>().movable = false;
            tile.GetComponent<gameTileSelection>().highlightDisabled();
        }
        mainCamera.gameObject.GetComponent<cameraMovement>().sniping = false;
        mode_attack = false;
        mode_move = false;
        if (selectedInfo != null) selectedInfo.selected = false;
        menu.SetActive(false);
    }

    public void QuitWindowOpen()
    {
        if (shutdown_button.activeSelf)
        {
            shutdown_button.SetActive(false);
            shutdown_window.SetActive(true);
        }
    }

    public void QuitWindowClose()
    {
        if (!shutdown_button.activeSelf)
        {
            shutdown_window.SetActive(false);
            shutdown_button.SetActive(true);
        }
    }

    public void UpdateMenu(GameObject s)
    {
        QuitWindowClose();
        selectedInfo = s.GetComponent<shipInfo>();
        selectedInfo.selected = true;
        switch (selectedInfo.type)
        {
            case 0: selectedType.text = "CORVETTE"; SFXList[0].Play(); break;
            case 1: selectedType.text = "FRIGATE"; SFXList[0].Play(); break;
            case 2: selectedType.text = "DESTROYER"; SFXList[1].Play(); break;
            case 3: selectedType.text = "CRUISER"; SFXList[1].Play(); break;
            default: selectedType.text = "BATTLESHIP"; SFXList[1].Play(); break;
        }
        selectedAP.text = selectedInfo.AP.ToString();
        attackable.Clear();
        movable.Clear();
        mode_attack = false;
        mode_move = false;
        calc.attackRangeCalculation();
        calc.moveRangeCalculation();
        if (attackable.Count == 0 || selectedInfo.AP == 0 || selectedInfo.attackCount > 0)
        {
            attack_available.SetActive(false);
            attack_unable.SetActive(true);
        }
        else 
        {
            attack_available.SetActive(true);
            attack_unable.SetActive(false);
            if (selectedInfo.type == 2 || selectedInfo.type == 4)
            {
                foreach (GameObject ship in attackable) ship.GetComponent<shipInfo>().attackable = true;
                mode_attack = true;
            }
        }
        if (movable.Count == 0 || selectedInfo.AP == 0)
        {
            move_available.SetActive(false);
            move_unable.SetActive(true);
        }
        else
        {
            move_available.SetActive(true);
            move_unable.SetActive(false);
        }
        attack_pending.SetActive(false);
        move_pending.SetActive(false);
    }
}
