using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    public PlayerController player;
    public bool hasSavedOnce = false;
    public Item[] equip;
    public Equip[] equipment;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.001f);
        LoadPrefs();
    }

    // Update is called once per frame
    void Update()
    {
       if (player == null) {
            player = StatsManager.instance.player.GetComponent<PlayerController>();
        }
    }

    public void Save()
    {
        hasSavedOnce = true;

        if (hasSavedOnce) {
            PlayerPrefs.SetInt("Has Saved Once", 1);
        }

        // - - - Saving respawn point coordinates
        PlayerPrefs.SetFloat("RespawnPoint.x", GameworldManager.instance.respawnPoint.x);
        PlayerPrefs.SetFloat("RespawnPoint.y", GameworldManager.instance.respawnPoint.y);

        // - - - Saving Player Stats
        PlayerPrefs.SetInt("Level", player.stats.lvl);
        PlayerPrefs.SetInt("Exp", player.stats.exp);
        PlayerPrefs.SetInt("NextLvl", player.stats.lvlUp);
        PlayerPrefs.SetInt("Dex", player.stats.dex);
        PlayerPrefs.SetInt("Def", player.stats.def);

        // - - - Saving General Game Stats
        PlayerPrefs.SetInt("Number of death(s)", StatsManager.instance.died);
        PlayerPrefs.SetFloat("Playtime", StatsManager.instance.playTime);

        // - - - Saving Inventory - - - 

        if (Inventory.instance.items.Count > 0) {
            for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i] != null) {
                    PlayerPrefs.SetInt("Item Slot " + i, Inventory.instance.items[i].idItem);
                }

                PlayerPrefs.SetInt("The size of the inventory", i + 1);

            }
        } else if (Inventory.instance.items.Count <= 0) {

            PlayerPrefs.SetInt("The size of the inventory", 0);
        }
        

        // - - - Saving Equipment - - - 
        for (int i= 0; i < EquipManager.instance.currentEquip.Length; i++) {
            if (EquipManager.instance.currentEquip[i] != null) {
                PlayerPrefs.SetInt("Equipment Worn " + i, EquipManager.instance.currentEquip[i].idItem);
            }
        }

        if (EquipManager.instance.currentEquip[0] != null) {
            PlayerPrefs.SetInt("Weapon Equipped ", EquipManager.instance.currentEquip[0].idItem);
        }
        if (EquipManager.instance.currentEquip[1] != null) {
            PlayerPrefs.SetInt("Shield Equipped ", EquipManager.instance.currentEquip[1].idItem);
        }

    }


    public void LoadPrefs() {
        if (player != null) {

            if (PlayerPrefs.GetInt("Has Saved Once") == 1) {
                GameworldManager.instance.respawnPoint = new Vector2(PlayerPrefs.GetFloat("RespawnPoint.x"), PlayerPrefs.GetFloat("RespawnPoint.y"));
                player.transform.position = GameworldManager.instance.respawnPoint;


               
                    player.stats.lvl = PlayerPrefs.GetInt("Level");
                    player.stats.exp = PlayerPrefs.GetInt("NextLvl");
                    player.stats.exp = PlayerPrefs.GetInt("Exp");
                    //player.stats.dex = PlayerPrefs.GetInt("Dex");
                    //player.stats.def = PlayerPrefs.GetInt("Def");

                // - - - - Loading the size of the inventory - - - - - 

                for (int i = 0; i < PlayerPrefs.GetInt("The size of the inventory"); i++) {


                    switch(PlayerPrefs.GetInt("Item Slot " + i)) {
                        case 7:
                            Inventory.instance.items.Add(equip[7]);
                            break;

                        case 6:
                            Inventory.instance.items.Add(equip[6]);
                            break;

                        case 5:
                            Inventory.instance.items.Add(equip[5]);
                            break;

                        case 4:
                            Inventory.instance.items.Add(equip[4]);
                            break;

                        case 3:
                            Inventory.instance.items.Add(equip[3]);
                            break;

                        case 2:
                            Inventory.instance.items.Add(equip[2]);
                            break;

                        case 1:
                            Inventory.instance.items.Add(equip[1]);
                            break;

                        case 0:
                            Inventory.instance.items.Add(equip[0]);
                            break;

                        default:
                            Inventory.instance.items.Add(equip[0]);
                            break;
                    }
                }

                EquipManager.instance.currentEquip[0] = equipment[PlayerPrefs.GetInt("Weapon Equipped ")];
                EquipManager.instance.currentEquip[1] = equipment[PlayerPrefs.GetInt("Shield Equipped ")];

                StatsManager.instance.died = PlayerPrefs.GetInt("Number of death(s)");
                StatsManager.instance.playTime = PlayerPrefs.GetFloat("Number of death(s)");
            }
        }
        StartCoroutine(EnableLvlUp());
    }

    public IEnumerator EnableLvlUp()
    {
        yield return new WaitForSeconds(5f);
        StatsManager.instance.isLoadingPrefs = false;
    }

}
