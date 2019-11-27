using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{
    public List<GameObject> unit;
    public List<Equip> weapon;
    public List<Equip> shield;
    public List<Item> item;

    private List<GameObject> statsManagerList;

    // Start is called before the first frame update
    void Start()
    {
        // - - - - - Shortcut for full singleton call - - - - - 
        statsManagerList = StatsManager.instance.unit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AssignEquips();
    }

    void AssignEquips()
    {
        unit = statsManagerList;
        for (int i = 0; i < unit.Count; i++) {

            if (unit[i] != null) {
                if (unit[i].name == "Knight") {
                    unit[i].GetComponent<Enemy>().weapon = weapon[1];
                    unit[i].GetComponent<Enemy>().shield = shield[1];
                    unit[i].GetComponent<Enemy>().item = item[0];

                } else if (unit[i].name == "Holy Knight") {
                    unit[i].GetComponent<Enemy>().weapon = weapon[2];
                    unit[i].GetComponent<Enemy>().shield = shield[2];
                    unit[i].GetComponent<Enemy>().item = item[1];
                } else {
                    unit[i].GetComponent<Enemy>().weapon = weapon[0];
                    unit[i].GetComponent<Enemy>().shield = shield[0];
                }
            }   else { return;  }
        }
    }
}
