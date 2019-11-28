using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    // * I know I shouldn't use multiple singletons, but I've been scrambling just to get this done, been too sick <_> * 
    #region - - - - - Singleton - - - - - 
    public static StatsManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion


    public GameObject player;
    public PlayerController playerController;

    public List<GameObject> unit;
    public Text hudHp;
    public Image eWpn;
    public Image eShield;

    public int died;
    public float playTime;
    public int totalKills;

    void Start()
    {

    }

    void FixedUpdate()
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
            // - - - - - Update exp & next level exp required - - - - - 
            int exp = playerController.stats.exp;
            int nextLvl = playerController.stats.lvlUp;

            // - - - - Check if there's a weapon equipped - - - - - 
            // - - - If there is one, then assign the icon - - -
            if (eWpn.sprite != null) {
                eWpn.sprite = EquipManager.instance.currentEquip[0].icon;
            }

            // - - - - Check if there's a shield equipped - - - - - 
            // - - - If there is one, then assign the icon - - -
            if (eShield.sprite != null) {
                eShield.sprite = EquipManager.instance.currentEquip[1].icon;
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
            if (exp >= nextLvl) {
                LevelUp();
            }


            if (playerController.stats.hp >= playerController.stats.maxHp) {
                playerController.stats.hp = playerController.stats.maxHp;
            }

            // - - - - - If the player is dead, award exp to enemy who did it - - - - 
            if (playerController.pState == state.dead) {
                if (!unit[playerController.slayer].GetComponent<Enemy>().hasLeveled) {
                    unit[playerController.slayer].GetComponent<Enemy>().hasLeveled = true;
                    unit[playerController.slayer].GetComponent<Enemy>().stats.exp += playerController.stats.lvl;
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
                // - - - - - Check if the enemy is dead - - - - -
                if (unit[i].GetComponent<Enemy>().eState == state.dead) {

                    // - - - - - - If enemy is dead, then give the player exp, 
                    // - - - - Destroy the enemy unit
                    // - - - Remove the unit from the enemy list - - - - - 
                    playerController.stats.exp = unit[i].GetComponent<Enemy>().stats.exp;
                    Destroy(unit[i]);
                    unit.Remove(unit[i]);
                    AssignUnitIndex();
                }
            }
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
    
        if (playerController.pState != state.dead){
            playerController.stats.lvl += 1;
            playerController.stats.lvlUp *= 2;

            playerController.stats.maxHp += 5;
            playerController.stats.hp += 5;

            playerController.stats.dex += 1;
            playerController.stats.def += 1;
        }
    }

    public void CheckEnemyLevelUp()
    {
        for (int i = 0; i < unit.Count; i++) {
            if (unit[i] != null) {
                Enemy unitLvlUp = unit[i].GetComponent<Enemy>();
                if (unitLvlUp.stats.exp >= unitLvlUp.stats.lvlUp) {
                    EnemyLevelUp(unitLvlUp);
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
