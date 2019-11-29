using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    public PlayerController player;
    public bool hasSavedOnce = false;
    public Item[] equip;
    public Equip[] equipment;

    private StatsManager stats;
    private GameworldManager manager;
    private Inventory inventory;
    private EquipManager equipManager;
    private EnemyInventory eInventory;
    private SavePlayerPrefs savePrefs;

    // Start is called before the first frame update
    void Start()
    {
        stats = Toolbox.GetInstance().GetStats().GetComponent<StatsManager>();

        manager = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>();

        inventory = Toolbox.GetInstance().GetInventory().GetComponent<Inventory>();

        equipManager = Toolbox.GetInstance().GetEquip().GetComponent<EquipManager>();
        eInventory = Toolbox.GetInstance().GeteInventory().GetComponent<EnemyInventory>();

        LoadPrefs();
    }


    // Update is called once per frame
    void Update()
    {
       if (player == null) {
            player = stats.player.GetComponent<PlayerController>();
        }
    }

    public void Save()
    {
        hasSavedOnce = true;

        if (hasSavedOnce) {
            PlayerPrefs.SetInt("Has Saved Once", 1);
        }

        // - - - Saving respawn point coordinates
        PlayerPrefs.SetFloat("RespawnPoint.x", manager.respawnPoint.x);
        PlayerPrefs.SetFloat("RespawnPoint.y", manager.respawnPoint.y);

        // - - - Saving Player Stats
        PlayerPrefs.SetInt("Level", player.stats.lvl);
        PlayerPrefs.SetInt("Exp", player.stats.exp);
        PlayerPrefs.SetInt("NextLvl", player.stats.lvlUp);
        PlayerPrefs.SetInt("Dex", player.stats.dex);
        PlayerPrefs.SetInt("Def", player.stats.def);
        PlayerPrefs.SetInt("MaxHP", player.stats.maxHp);
        PlayerPrefs.SetInt("HP", player.stats.hp);


        // - - - Saving General Game Stats
        PlayerPrefs.SetInt("Number of death(s)", stats.died);
        PlayerPrefs.SetFloat("Playtime", stats.playTime);

        // - - - Saving Inventory - - - 

        if (inventory.items.Count > 0) {
            for (int i = 0; i < inventory.items.Count; i++) {
                if (inventory.items[i] != null) {
                    PlayerPrefs.SetInt("Item Slot " + i, inventory.items[i].idItem);
                }

                PlayerPrefs.SetInt("The size of the inventory", i + 1);

            }
        } else if (inventory.items.Count <= 0) {

            PlayerPrefs.SetInt("The size of the inventory", 0);
        }
        

        // - - - Saving Equipment - - - 
        for (int i= 0; i < equipManager.currentEquip.Length; i++) {
            if (equipManager.currentEquip[i] != null) {
                PlayerPrefs.SetInt("Equipment Worn " + i, equipManager.currentEquip[i].idItem);
            }
        }

        if (equipManager.currentEquip[0] != null) {
            PlayerPrefs.SetInt("Weapon Equipped ", equipManager.currentEquip[0].idItem);
        }
        if (equipManager.currentEquip[1] != null) {
            PlayerPrefs.SetInt("Shield Equipped ", equipManager.currentEquip[1].idItem);
        }

    }


    public void LoadPrefs() {
        if (player != null) {

            if (PlayerPrefs.GetInt("Has Saved Once") == 1) {
                manager.respawnPoint = new Vector2(PlayerPrefs.GetFloat("RespawnPoint.x"), PlayerPrefs.GetFloat("RespawnPoint.y"));
                player.transform.position = manager.respawnPoint;


               
                player.stats.lvl = PlayerPrefs.GetInt("Level");
                player.stats.exp = PlayerPrefs.GetInt("NextLvl");
                player.stats.hp = PlayerPrefs.GetInt("MaxHP");
                player.stats.hp = PlayerPrefs.GetInt("HP");
                player.stats.dex = PlayerPrefs.GetInt("Dex");
                player.stats.def = PlayerPrefs.GetInt("Def");
                player.stats.exp = PlayerPrefs.GetInt("Exp");





                // - - - - Loading the size of the inventory - - - - - 

                for (int i = 0; i < PlayerPrefs.GetInt("The size of the inventory"); i++) {


                    switch(PlayerPrefs.GetInt("Item Slot " + i)) {
                        case 7:
                            inventory.items.Add(equip[7]);
                            break;

                        case 6:
                            inventory.items.Add(equip[6]);
                            break;

                        case 5:
                            inventory.items.Add(equip[5]);
                            break;

                        case 4:
                            inventory.items.Add(equip[4]);
                            break;

                        case 3:
                            inventory.items.Add(equip[3]);
                            break;

                        case 2:
                            inventory.items.Add(equip[2]);
                            break;

                        case 1:
                            inventory.items.Add(equip[1]);
                            break;

                        case 0:
                            inventory.items.Add(equip[0]);
                            break;

                        default:
                            inventory.items.Add(equip[0]);
                            break;
                    }
                }

                equipManager.currentEquip[0] = equipment[PlayerPrefs.GetInt("Weapon Equipped ")];
                equipManager.currentEquip[1] = equipment[PlayerPrefs.GetInt("Shield Equipped ")];

                stats.died = PlayerPrefs.GetInt("Number of death(s)");
                stats.playTime = PlayerPrefs.GetFloat("Number of death(s)");
            }
        }
        StartCoroutine(EnableLvlUp());
    }

    public IEnumerator EnableLvlUp()
    {
        yield return new WaitForSeconds(5f);
        stats.isLoadingPrefs = false;
    }

}
