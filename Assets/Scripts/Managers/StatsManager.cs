using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> unit;
    public Text hudHp;
    public Image eWpn;
    public Image eShield;

    void Start()
    {

    }

    void Update()
    {
        if (unit[0] == null) {
            FindTargets();
        }

        CheckStats();
    }

    void FindTargets()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Heal(int recover)
    {

    }

    void Hurt(int damage)
    {

    }

    void CheckDeath()
    {

    }

    void Death()
    {

    }

    void CheckStats()
    {
        int exp = player.GetComponent<PlayerController>().stats.exp;
        int nextLvl = player.GetComponent<PlayerController>().stats.lvlUp;

        if (eWpn.sprite != null) {
            eWpn.sprite = EquipManager.instance.currentEquip[0].icon;
        }

        if (eShield.sprite != null) {
            eShield.sprite = EquipManager.instance.currentEquip[1].icon;
        }

        hudHp.text = "HP: " + player.GetComponent<PlayerController>().stats.hp + " / " + player.GetComponent<PlayerController>().stats.maxHp;

        if (exp >= nextLvl) {


            LevelUp();
        }
    }

    void LevelUp()
    {   player.GetComponent<PlayerController>().stats.lvl += 1;
        player.GetComponent<PlayerController>().stats.lvlUp *= 2;

        player.GetComponent<PlayerController>().stats.maxHp += 5;
        player.GetComponent<PlayerController>().stats.hp += 5;

        player.GetComponent<PlayerController>().stats.dex += 1;
        player.GetComponent<PlayerController>().stats.def += 1;
    }
}
