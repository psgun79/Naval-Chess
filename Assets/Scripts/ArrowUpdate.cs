using UnityEngine;
using UnityEngine.UI;

public class ArrowUpdate : MonoBehaviour
{
    public teamOrganize system;
    public int type;
    int cost;
    int count;
    int limit;
    bool increment;

    void Start()
    {
        cost = system.cost[type];
        limit = system.limit[type];
        increment = (this.gameObject.name == "Up") ? true : false;
    }

    void Update()
    {
        count = system.ships[system.team, type];
        if (increment && (system.ships[system.team, type] + 1 > limit || system.money - cost < 0))
        {
            this.GetComponent<Button>().interactable = false;
        }
        else if (!increment && system.ships[system.team, type] - 1 < 0)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else this.GetComponent<Button>().interactable = true;
    }
}
