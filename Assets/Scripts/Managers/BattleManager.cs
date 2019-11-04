using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckSpawn()
    {
        //check player & enemy
        //assign them
        Spawn();
    }

    void Spawn()
    {
        // spawn player
        // spawn enemy
    }

    void ChoosePhase()
    {
        // enable battle action UI elements
        // check picked option
        // move onto action phase
        
    }

    void ActionPhase()
    {
        // change unit state to display animation
        // change unit stats depending on outcome
    }

}
