using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardSystem : MonoBehaviour
{
    public List<GameObject> prefabs_TOP = new List<GameObject>();
    public GameObject prefab_0_TOP;
    public GameObject prefab_1_TOP;
    public GameObject prefab_2_TOP;
    public GameObject prefab_3_TOP;
    public GameObject prefab_4_TOP;
    public List<GameObject> prefabs_BOTTOM = new List<GameObject>();
    public GameObject prefab_0_BOTTOM;
    public GameObject prefab_1_BOTTOM;
    public GameObject prefab_2_BOTTOM;
    public GameObject prefab_3_BOTTOM;
    public GameObject prefab_4_BOTTOM;
    public List<GameObject> ships_TOP = new List<GameObject>();
    public List<GameObject> ships_BOTTOM = new List<GameObject>();
    public List<GameObject> attackable = new List<GameObject>();
    public List<GameObject> movable = new List<GameObject>();
    public Transform clickedTransform;
    public GameObject board;
    public GameObject menu;
    public GameObject attack_pending;
    public GameObject attack_unable;
    public GameObject cancel;
    public GameObject move_pending;
    public GameObject move_unable;
    shipInfo selectedInfo;
    int cnt_TOP = 0;
    int cnt_BOTTOM = 0;
    public bool gameStarted = false;
    public bool mode_attack = false;
    public bool mode_move = false;

    void Start()
    {
        prefabs_TOP.Add(prefab_0_TOP);
        prefabs_TOP.Add(prefab_1_TOP);
        prefabs_TOP.Add(prefab_2_TOP);
        prefabs_TOP.Add(prefab_3_TOP);
        prefabs_TOP.Add(prefab_4_TOP);
        prefabs_BOTTOM.Add(prefab_0_BOTTOM);
        prefabs_BOTTOM.Add(prefab_1_BOTTOM);
        prefabs_BOTTOM.Add(prefab_2_BOTTOM);
        prefabs_BOTTOM.Add(prefab_3_BOTTOM);
        prefabs_BOTTOM.Add(prefab_4_BOTTOM);
    }

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
        if (!mode_attack)
        {
            mode_attack = true;
            attack_pending.SetActive(true);
        }
        else
        {
            mode_attack = false;
            attack_pending.SetActive(false);
        }
    }

    public void Attack()
    {
        
    }

    public void MoveButtonClick()
    {
        attack_pending.SetActive(false);
        if (!mode_move)
        {
            mode_move = true;
            move_pending.SetActive(true);
            foreach (GameObject tile in movable)
            {
                tile.GetComponent<gameTileSelection>().movable = true;
            }
        }
        else
        {
            mode_move = false;
            move_pending.SetActive(false);
            foreach (GameObject tile in movable)
            {
                tile.GetComponent<gameTileSelection>().movable = false;
            }
        }
    }

    public void Move(int x, int y)
    {
        selectedInfo.xPos = x;
        selectedInfo.yPos = y;
        selectedInfo.Relocate();
        menu.SetActive(false);
    }

    public void CancelButtonClick()
    {
        selectedInfo = new shipInfo();
        mode_attack = false;
        mode_move = false;
        menu.SetActive(false);
    }

    bool OccupationCheck(int x, int y)
    {
        foreach (GameObject s in ships_TOP)
        {
            shipInfo info = s.GetComponent<shipInfo>();
            if (x == info.xPos && y == info.yPos) return false;
        }
        foreach (GameObject s in ships_BOTTOM)
        {
            shipInfo info = s.GetComponent<shipInfo>();
            if (x == info.xPos && y == info.yPos) return false;
        }
        return true;
    }

    public void UpdateMenu(GameObject s)
    {
        selectedInfo = s.GetComponent<shipInfo>();
        attackable.Clear();
        movable.Clear();
        List<GameObject> enemies = (selectedInfo.team == 0) ? ships_BOTTOM : ships_TOP;
        int k = (selectedInfo.team == 0) ? 2 : -2;
        switch (selectedInfo.type)
        {
            case 0:
                foreach (GameObject e in enemies)
                {
                    bool withinRange = false;
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = Mathf.Abs(x - selectedInfo.xPos);
                        switch (y_gap)
                        {
                            case 0:
                                if (x_gap <= 6) withinRange = true;
                                break;
                            case -1:
                                if (x_gap <= 5) withinRange = true;
                                break;
                            case 1:
                                if (x_gap <= 4) withinRange = true;
                                break;
                            default:
                                if (y_gap != -2) continue;
                                else if (x_gap == 1 || x_gap == 3) withinRange = true;
                                break;
                        }
                        if (withinRange) {
                            attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
            case 1:
                foreach (GameObject e in enemies)
                {
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int x_gap = Mathf.Abs(enemyInfo.xPos - selectedInfo.xPos);
                    int y_gap = Mathf.Abs(enemyInfo.yPos - selectedInfo.yPos);
                    if (y_gap != 0) continue;
                    else if (x_gap == 2 || x_gap == 4 || x_gap == 6) attackable.Add(e);
                }
                break;
            case 2:
                foreach (GameObject e in enemies)
                {
                    bool withinRange = false;
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = Mathf.Abs(x - selectedInfo.xPos);
                        switch (y_gap)
                        {
                            case 0:
                                if (x_gap <= 4) withinRange = true;
                                break;
                            case -1:
                                if (x_gap <= 3) withinRange = true;
                                break;
                            default:
                                if (y_gap != 1) continue;
                                else if (x_gap == 0 || x_gap == 2) withinRange = true;
                                break;
                        }
                        if (withinRange) {
                            attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
            case 3:
                foreach (GameObject e in enemies)
                {
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = Mathf.Abs(x - selectedInfo.xPos);
                        if ((y_gap == x / 2 || y_gap == -((x - 1) / 2) - 1) && Mathf.Abs(y_gap) <= 3)
                        {
                            attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
            default:
                foreach (GameObject e in enemies)
                {
                    bool withinRange = false;
                    shipInfo enemyInfo = e.GetComponent<shipInfo>();
                    int y = enemyInfo.yPos;
                    int y_gap = y - selectedInfo.yPos;
                    for (int i = 0; i < enemyInfo.len; i++)
                    {
                        int x = enemyInfo.xPos + k * i;
                        int x_gap = Mathf.Abs(x - selectedInfo.xPos);
                        switch (y_gap)
                        {
                            case 0:
                                if (-4 <= x_gap && x_gap <= 1) withinRange = true;
                                break;
                            case -1:
                                if (-3 <= x_gap && x_gap <= 2) withinRange = true;
                                break;
                            case 1:
                                if (-2 <= x_gap && x_gap <= 3) withinRange = true;
                                break;
                            default:
                                if (y_gap != -2) continue;
                                else if (x_gap == -1 || x_gap == 1 || x_gap == 3) withinRange = true;
                                break;
                        }
                        if (withinRange) {
                            attackable.Add(e);
                            break;
                        }
                    }
                }
                break;
        }
        k = (selectedInfo.team == 0) ? 1 : -1;
        Debug.Log(k);
        Transform option_1 = null;
        Transform option_2 = null;
        Transform option_3 = null;
        string x_plus_one = "Row_" + (selectedInfo.xPos + 1 + k).ToString();
        string x_plus_two = "Row_" + (selectedInfo.xPos + 1 + 2 * k).ToString();
        string x_plus_four = "Row_" + (selectedInfo.xPos + 1 + 4 * k).ToString();
        string y_minus_one = "Cell_" + (selectedInfo.yPos).ToString();
        string y_zero = "Cell_" + (selectedInfo.yPos + 1).ToString();
        string y_plus_one = "Cell_" + (selectedInfo.yPos + 2).ToString();
        switch (selectedInfo.type)
        {
            case 1:
                if (OccupationCheck(selectedInfo.xPos + k, selectedInfo.yPos - 1) && OccupationCheck(selectedInfo.xPos + 2 * k, selectedInfo.yPos - 1))
                    option_1 = board.transform.Find(x_plus_two).Find(y_minus_one);
                if (OccupationCheck(selectedInfo.xPos + 2 * k, selectedInfo.yPos) && OccupationCheck(selectedInfo.xPos + 4 * k, selectedInfo.yPos))
                    option_2 = board.transform.Find(x_plus_four).Find(y_zero);
                if (OccupationCheck(selectedInfo.xPos + k, selectedInfo.yPos) && OccupationCheck(selectedInfo.xPos + 2 * k, selectedInfo.yPos + 1))
                    option_3 = board.transform.Find(x_plus_two).Find(y_plus_one);
                break;
            default:
                if (OccupationCheck(selectedInfo.xPos + k, selectedInfo.yPos - 1)) option_1 = board.transform.Find(x_plus_one).Find(y_minus_one);
                if (OccupationCheck(selectedInfo.xPos + 2 * k, selectedInfo.yPos)) option_2 = board.transform.Find(x_plus_two).Find(y_zero);
                if (OccupationCheck(selectedInfo.xPos + k, selectedInfo.yPos)) option_3 = board.transform.Find(x_plus_one).Find(y_zero);
                break;
        }
        if (option_1 != null) movable.Add(option_1.gameObject);
        if (option_2 != null) movable.Add(option_2.gameObject);
        if (option_3 != null) movable.Add(option_3.gameObject);
        if (attackable.Count == 0) attack_unable.SetActive(true); else attack_unable.SetActive(false);
        if (movable.Count == 0) move_unable.SetActive(true); else move_unable.SetActive(false);
        attack_pending.SetActive(false);
        move_unable.SetActive(false);
    }

    void Update()
    {
        if (gameStarted && Input.GetMouseButton(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                clickedTransform = hit.transform;
                if (clickedTransform.root.tag == "Player")
                {
                    UpdateMenu(hit.transform.root.gameObject);
                    menu.SetActive(true);
                }
            }
            else menu.SetActive(false);
        }
    }
}
