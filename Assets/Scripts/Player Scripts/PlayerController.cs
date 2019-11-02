using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    idle,
    walking,
    climbing,

    attack,
    defend,
    counter,
}

public class PlayerController : MonoBehaviour
{
    public PlayerAction action;
    public Stats stats;

    private Rigidbody rb;
    public bool paused;



    // Start is called before the first frame update
    void Start()
    {
        //action = Toolbox.GetInstance().GetAction();
        //stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RunStates();
    }

    void RunStates()
    {
        Movement();
    }

    #region - - - - - - ACTION FUNCTIONS - - - - - 
    void Movement()
    {
        // Movement Code
        if (action.moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(action.rotation);
            transform.Translate(action.moveDir * stats.spd * Time.deltaTime);
            //rb.AddForce(action.moveDir.x, action.moveDir.y, action.moveDir.z * speed * Time.deltaTime);
        }
        else {
            transform.Translate(Vector3.zero);
            //rb.velocity = Vector3.zero;
        }

    }

    void BattleActions()
    {
        if (action.canAct)
        {
            if (action.slash)
            {
                //do attack
            }

            else if (action.block)
            {
                //do defend
            }

            else if (action.parry)
            {
                //do counter
            }


        }
    }
    #endregion

    #region - - - - - Pause / Unpause - - - - - 
    public void PauseGame()
    {
        if (action.pause)
        {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
            }

            else if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            }
        }
    }
    #endregion
}
