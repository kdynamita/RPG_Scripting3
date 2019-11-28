using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    public LevelManager lvlManager;
    public PlayerController player;
    public bool hasSavedOnce = false;
    public Item[] equip;

    // Start is called before the first frame update
    void Start()
    {
        lvlManager = GetComponent<LevelManager>();
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
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
        PlayerPrefs.SetFloat("RespawnPoint.x", lvlManager.respawnPoint.x);
        PlayerPrefs.SetFloat("RespawnPoint.y", lvlManager.respawnPoint.y);

        // - - - Saving Player Stats
        PlayerPrefs.SetInt("Level", player.stats.lvl);
        PlayerPrefs.SetInt("Exp", player.stats.exp);
        PlayerPrefs.SetInt("Dex", player.stats.dex);
        PlayerPrefs.SetInt("Def", player.stats.def);

        // - - - Saving General Game Stats
        PlayerPrefs.SetInt("Number of death(s)", StatsManager.instance.died);
        PlayerPrefs.SetFloat("Playtime", StatsManager.instance.playTime);

        // - - - Saving Inventory - - - 

        for (int i = 0; i < Inventory.instance.items.Count; i++) {


            if (Inventory.instance.items[i] != null) {
                PlayerPrefs.SetInt("Item Slot " + i, Inventory.instance.items[i].idItem);
            } 

            PlayerPrefs.SetInt("The size of the inventory", i);
            Debug.Log("Counting Qty of Inventory Items " + PlayerPrefs.GetInt("The size of the inventory"));
        }

        // - - - Saving Equipment - - - 
        for (int i= 0; i < EquipManager.instance.currentEquip.Length; i++) {
            if (EquipManager.instance.currentEquip[i] != null) {
                PlayerPrefs.SetString("Equipped: " + EquipManager.instance.currentEquip[i], EquipManager.instance.currentEquip[i].itemName);
            }
        }

    }


    public void LoadPrefs()
    {
        if (player != null) {
            Debug.Log(PlayerPrefs.GetInt("Has Saved Once"));

            if (PlayerPrefs.GetInt("Has Saved Once") == 1) {
                lvlManager.respawnPoint = new Vector2(PlayerPrefs.GetFloat("RespawnPoint.x"), PlayerPrefs.GetFloat("RespawnPoint.y"));
                player.transform.position = lvlManager.respawnPoint;


                    player.stats.lvl = PlayerPrefs.GetInt("Level");
                    player.stats.exp = PlayerPrefs.GetInt("Exp");
                    player.stats.dex = PlayerPrefs.GetInt("Dex");
                    player.stats.def = PlayerPrefs.GetInt("Def");

                // - - - - Loading the size of the inventory - - - - - 



                for (int i = 0; i < PlayerPrefs.GetInt("The size of the inventory"); i++) {
                    Debug.Log("Looping through i");
                    /*
                    for (int j = 0; j < equip.Length; i++) {
                        Debug.Log("Looping through j");
                       /*Debug.Log("Look through the " + equip.Length + " possible items");
                       if (PlayerPrefs.GetInt("Item Slot " + i) == equip[j].idItem) {
                            Debug.Log("Assigning item");
                            Inventory.instance.items.Add(equip[j]);
                       }
                    }*/
                }
                StatsManager.instance.died = PlayerPrefs.GetInt("Number of death(s)");
                StatsManager.instance.playTime = PlayerPrefs.GetFloat("Number of death(s)");
            }
        } 
    }

    public IEnumerator LoadPrefsCo() {
        yield return new WaitForSeconds(0.1f);
        lvlManager.respawnPoint = new Vector2(PlayerPrefs.GetFloat("RespawnPoint.x"), PlayerPrefs.GetFloat("RespawnPoint.y"));
        player.transform.position = lvlManager.respawnPoint;

        if (player != null) {
            player.stats.lvl = PlayerPrefs.GetInt("Level");
            player.stats.exp = PlayerPrefs.GetInt("Exp");
            player.stats.dex = PlayerPrefs.GetInt("Dex");
            player.stats.def = PlayerPrefs.GetInt("Def");
        }

        // - - - - Loading the size of the inventory - - - - - 



         for (int i = 0; i < PlayerPrefs.GetInt("The size of the inventory"); i++) {
            //Inventory.instance.items.Add(equip[i]);
            /*for (int j = 0; j < equip.Length; i++) {
                Debug.Log("Look through the " + equip.Length + " possible items");
               if (PlayerPrefs.GetInt("Item Slot " + i) == equip[j].idItem) {
                    Debug.Log("Assigning item");
                    Inventory.instance.items.Add(equip[j]);
               }
            }*/
        }

        StatsManager.instance.died = PlayerPrefs.GetInt("Number of death(s)");
        StatsManager.instance.playTime = PlayerPrefs.GetFloat("Number of death(s)");
    }
}
