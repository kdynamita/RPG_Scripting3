using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBattleManager : MonoBehaviour
{
    private BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        var go = new GameObject("BattleManager");
        this.battleManager = go.AddComponent<BattleManager>();

        Destroy(this.gameObject);
    }

}
