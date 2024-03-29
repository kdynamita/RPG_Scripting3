﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public EquipManager equip;
    public GameworldManager manager;

    public GameObject player;
    public PlayerController playerController;

    public List<GameObject> unit;
    public Text hudHp;
    public Image eWpn;
    public Image eShield;

    public int died;
    public float playTime;
    public int totalKills;

    public bool isLoadingPrefs = true;

    void Start()
    {
        //StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1f);
        equip = Toolbox.GetInstance().GetEquip().GetComponent<EquipManager>() ;
    }

    void Update()
    {
        FindTargets();
        UpdateStats();
    }


    // - - - - - Function to check all units currently present & active in the game - - - - - 
    void FindTargets()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }

        if (unit.Count <= 0) {
            unit.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        }

        for (int i=0; i<unit.Count; i++) {
            if (unit[i] == null) {
                unit.RemoveAt(i);
            }
        }
    }

    #region - - - - - Check Stats & Level Up Functions - - - - - 
    public void UpdateStats()
    {
        playTime += Time.deltaTime;

        #region - - - - - Updating Player Stats/Equips- - - - - 
        // - - - - - Run only if there is a player character available - - - - - 
        if (player != null) {

            #region - - - - - Update Exp & Equip Icons - - - - 

            // - - - - Check if there's a weapon equipped - - - - - 
            // - - - If there is one, then assign the icon - - -
            if (eWpn.sprite != null) {
                eWpn.sprite = equip.currentEquip[0].icon;
            }

            // - - - - Check if there's a shield equipped - - - - - 
            // - - - If there is one, then assign the icon - - -
            if (eShield.sprite != null) {
                eShield.sprite = equip.currentEquip[1].icon;
            }

            #endregion

            #region  - - - - - HUD Text for hp / maxHp - - - - - 

            if (playerController.stats.hp < 0) {
                playerController.stats.hp = 0;
                hudHp.text = "HP: " + playerController.stats.hp + " / " + playerController.stats.maxHp;
            } 
            
            else if (playerController.stats.hp > playerController.stats.maxHp) {
                playerController.stats.hp = playerController.stats.maxHp;
                hudHp.text = "HP: " + playerController.stats.hp + " / " + playerController.stats.maxHp;
            } 
            
            else {
                hudHp.text = "HP: " + playerController.stats.hp + " / " + playerController.stats.maxHp;
            }
            #endregion

            #region - - - - - Exp & Level Up (Player & Enemy) - - - - -
            // - - - - - If Exp requirement is met, level up character - - - - - 
            if (playerController.stats.exp >= playerController.stats.lvlUp) {
                LevelUp();
            }


            if (playerController.stats.hp >= playerController.stats.maxHp) {
                playerController.stats.hp = playerController.stats.maxHp;
            }

            // - - - - - If the player is dead, award exp to enemy who did it - - - - 
            if (playerController.pState == state.dead) {
                if (!playerController.slayer.GetComponent<Enemy>().hasLeveled) {
                    playerController.slayer.GetComponent<Enemy>().hasLeveled = true;
                    playerController.slayer.GetComponent<Enemy>().stats.exp += playerController.stats.lvl;
                    playerController.slayer.GetComponent<SpriteRenderer>().color = Color.blue;
                    died += 1;

                } else {
                    return;
                }
            }
            #endregion

        }

        #endregion

        #region - - - - - Checking & Updating Enemy - - - - - 
        // - - - - - Check if there's any enemy on the map - - - - - 
        if (unit.Count > 0) {
            // - - - - - Loop through all the enemies - - - -  
            for (int i=0; i<unit.Count; i++) {

                if (unit[i] == null) {
                    unit.Remove(unit[i]);
                    i = 0;
                }
            } manager.unit = this.unit;
        } else { return; }
        #endregion
    }


    // - - - - - Function to assign unit index to each enemy, as a way to identify them throughout the playhthrough
    void AssignUnitIndex()
    {
        // - - - - Assign an index to every enemy for enemy promotion mechanic
        for (int i = 0; i < unit.Count; i++) {
            unit[i].GetComponent<Enemy>().unitIndex = i;
        }
    }

    // - - - - - Level Up Function that adds:
    // - - - - - 1 Level, Doubles Exp Requirements, 5 HP & Max H, 1 Dex & 1 Def
    void LevelUp()
    {
        if (isLoadingPrefs) {
            return;
        } 
        
        else if (!isLoadingPrefs) {

            int currentNextLvl = playerController.stats.lvlUp;
            int currentLevel = playerController.stats.lvl;
            int currentMaxHp = playerController.stats.maxHp;
            int currentHp = playerController.stats.hp;
            int currentDex = playerController.stats.dex;
            int currentDef = playerController.stats.def;


            manager.playerPrompt.sprite = manager.lvlPrompt;
            playerController.stats.lvlUp = currentNextLvl * 2;
            playerController.stats.lvl = currentLevel + 1;
            playerController.stats.maxHp = currentMaxHp + 5;
            playerController.stats.hp = currentHp + 5;
            playerController.stats.dex = currentDex + 1;
            playerController.stats.def = currentDef + 1;
        }
    }

    public void CheckEnemyLevelUp()
    {
        for (int i = 0; i < unit.Count; i++) {
            if (unit[i] != null) {
                Enemy unitLvlUp = unit[i].GetComponent<Enemy>();
                if (unitLvlUp.stats.exp >= unitLvlUp.stats.lvlUp) {
                    EnemyLevelUp(unitLvlUp);
                    manager.ApplyPrompt(manager.unit[i].GetComponent<Enemy>().unitPrompt.GetComponent<SpriteRenderer>().sprite = manager.lvlPrompt);
                }
            }
        }
    }
    
    void EnemyLevelUp(Enemy unit)
    {
        if (unit.eState != state.dead) {
            unit.stats.lvl += 1;
            unit.stats.lvlUp *= 2;

            unit.stats.hp += unit.stats.maxHp;
            unit.stats.maxHp += 5;

            unit.stats.dex += 1;
            unit.stats.def += 1;
        }
    }

    #endregion
}
