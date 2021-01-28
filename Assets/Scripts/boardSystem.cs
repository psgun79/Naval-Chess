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
    List<GameObject> ships_TOP = new List<GameObject>();
    List<GameObject> ships_BOTTOM = new List<GameObject>();
    public Transform selectedShip;
    public GameObject menu;
    int cnt_TOP = 0;
    int cnt_BOTTOM = 0;
    public bool gameStarted = false;

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
                // TODO: prefab 내부 모든 child에 대해서 태그를 Player로 붙임 (if 문 위아래로 둘다 작성)
                // 그 후 update 함수에서 updatemenu에 들어가는 것을 hit 대신 제일 위의 parent로 들어가게 함 (스크립트가 프리팹 제일 상위에 있으므로)
                // updatemenu에서는 shipInfo 내의 정보 및 적 배 리스트를 가지고 연산하게 될 예정
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

    public void UpdateMenu(GameObject s)
    {
        Debug.Log(s.name);
    }

    void Update()
    {
        if (gameStarted && Input.GetMouseButton(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                selectedShip = hit.transform;
                if (selectedShip.tag == "Player")
                {
                    UpdateMenu(hit.transform.gameObject);
                    menu.SetActive(true);
                }
            }
            else menu.SetActive(false);
        }
    }
}
