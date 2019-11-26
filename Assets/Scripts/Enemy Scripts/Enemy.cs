using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Stats stats;

    public state state;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) {
            GameObject.FindGameObjectWithTag("Player");
        }
    }



    public void CheckStats()
    {
        if (stats.hp <= 0) {
            Death();
        }

    }

    void Death()
    {
        // - - - - Give Exp
        player.GetComponent<PlayerController>().stats.exp += stats.exp;
        Destroy(this.gameObject);
    }

}
