using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{

    private static Toolbox _instance;

    public static Toolbox GetInstance()
    {
        if (Toolbox._instance == null)
        {
            var goToolbox = new GameObject("Toolbox");
            //DontDestroyOnLoad(goToolbox);
            Toolbox._instance = goToolbox.AddComponent<Toolbox>();
        }
        return Toolbox._instance;
    }

    [SerializeField] private GameObject manager;
    [SerializeField] private StatsManager stats;
    [SerializeField] private GameworldManager gameManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipManager equip;
    [SerializeField] private EnemyInventory eInventory;
    [SerializeField] private SavePlayerPrefs savePrefs;


    void Awake()
    {
        if (Toolbox._instance != null)
        {
            Destroy(this.gameObject);
        }

        manager = GameObject.FindGameObjectWithTag("Manager");

        if (manager != null) { Debug.Log(manager); }
        gameManager = manager.GetComponent<GameworldManager>();
        inventory = manager.GetComponent<Inventory>();
        equip = manager.GetComponent<EquipManager>();
        eInventory = manager.GetComponent<EnemyInventory>();
        stats = manager.GetComponent<StatsManager>();

        if (manager == null) {
            var go = new GameObject("Manager");
            go.transform.parent = this.gameObject.transform;
            go.AddComponent<StatsManager>();
            go.AddComponent<GameworldManager>();
            go.AddComponent<Inventory>();
            go.AddComponent<EquipManager>();
            go.AddComponent<EnemyInventory>();
            go.AddComponent<SavePlayerPrefs>();
        }



    }

    public StatsManager GetStats()
    {
        return this.stats;
    }

    public GameworldManager GetManager()
    {
        return this.gameManager;
    }

    public Inventory GetInventory()
    {
        return this.inventory;
    }

    public EquipManager GetEquip()
    {
        return this.equip;
    }

    public EnemyInventory GeteInventory()
    {
        return this.eInventory;
    }





}