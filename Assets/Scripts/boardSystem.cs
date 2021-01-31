using System.Collections.Generic;
using UnityEngine;

public class boardSystem : MonoBehaviour
{
    public actionRangeCalc system;
    public List<GameObject> prefabs_TOP = new List<GameObject>();
    public List<GameObject> prefabs_BOTTOM = new List<GameObject>();
    public List<GameObject> ships_TOP = new List<GameObject>();
    public List<GameObject> ships_BOTTOM = new List<GameObject>();
    public List<GameObject> attackable = new List<GameObject>();
    public List<GameObject> movable = new List<GameObject>();
    public GameObject board;
    public GameObject menu;
    public GameObject attack_pending;
    public GameObject attack_unable;
    public GameObject move_pending;
    public GameObject move_unable;
    public shipInfo selectedInfo;
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
        target.GetComponent<shipInfo>().OnDamage();
        mode_attack = false;
        selectedInfo.selected = false;
        menu.SetActive(false);
    }

    public void MoveButtonClick()
    {
        attack_pending.SetActive(false);
        if (!mode_move)
        {
            mode_move = true;
            move_pending.SetActive(true);
            foreach (GameObject tile in movable) tile.GetComponent<gameTileSelection>().movable = true;
        }
        else
        {
            mode_move = false;
            move_pending.SetActive(false);
            foreach (GameObject tile in movable) tile.GetComponent<gameTileSelection>().movable = false;
        }
    }

    public void Move(int x, int y)
    {
        selectedInfo.xPos = x;
        selectedInfo.yPos = y;
        selectedInfo.Relocate();
        mode_move = false;
        selectedInfo.selected = false;
        menu.SetActive(false);
    }

    public void CancelButtonClick()
    {
        mainCamera.gameObject.GetComponent<cameraMovement>().sniping = false;
        mode_attack = false;
        mode_move = false;
        selectedInfo.selected = false;
        menu.SetActive(false);
    }

    public void UpdateMenu(GameObject s)
    {
        selectedInfo = s.GetComponent<shipInfo>();
        selectedInfo.selected = true;
        attackable.Clear();
        movable.Clear();
        mode_attack = false;
        mode_move = false;
        system.attackRangeCalculation();
        system.moveRangeCalculation();
        if (attackable.Count == 0) attack_unable.SetActive(true); else attack_unable.SetActive(false);
        if (movable.Count == 0) move_unable.SetActive(true); else move_unable.SetActive(false);
        attack_pending.SetActive(false);
        move_pending.SetActive(false);
        if (selectedInfo.type == 2 || selectedInfo.type == 4)
        {
            foreach (GameObject ship in attackable) ship.GetComponent<shipInfo>().attackable = true;
            mode_attack = true;
        }
    }
}
