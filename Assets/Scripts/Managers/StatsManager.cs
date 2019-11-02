using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    #region - - - - - Battle States - - - - - 
    active,
    hurt,
    whiffed,
    attack,
    defend,
    counter,
    #endregion
}

public class StatsManager : MonoBehaviour
{
    public Stats stats;
    void Start()
    {
        stats = GetComponent<Stats>();
    }

    void Heal(int recover)
    {
        stats.hp += recover;
    }

    void Hurt(int damage)
    {
        stats.hp -= damage;
        CheckDeath();
    }

    void CheckDeath()
    {
        if (stats.hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
