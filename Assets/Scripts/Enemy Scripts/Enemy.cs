using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : PlayerController
{
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

    void RunState()
    {

    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Block()
    {
        base.Block();
    }

    public override void Shoot()
    {
        base.Shoot();
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
