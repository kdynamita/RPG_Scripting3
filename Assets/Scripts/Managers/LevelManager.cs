using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> unit;

    void Start()
    {

    }

    private void Update()
    {
        if (unit[0] == null) {
            FindTargets();
        }

        CheckLvlUp();
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

    void CheckLvlUp()
    {
        int exp = player.GetComponent<PlayerController>().stats.exp;
        int nextLvl = player.GetComponent<PlayerController>().stats.lvlUp;

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
