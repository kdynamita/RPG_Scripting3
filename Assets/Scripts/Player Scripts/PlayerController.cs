using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    #region - - - - Overworld States - - - - 
    idle,
    walking,
    climbing,
    #endregion
}

public class PlayerController : MonoBehaviour
{
    public PlayerAction action;
    public Stats stats;

    private Rigidbody2D rb;
    public bool paused;

    public state state;

    public bool canClimb;


    // Start is called before the first frame update
    void Start()
    {
        action = Toolbox.GetInstance().GetAction();
        //stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        state = state.idle;
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
        if (action.moveDir.x != 0)
        {
            transform.rotation = Quaternion.Euler(action.rotation);
            transform.Translate(action.moveDir * stats.spd * Time.deltaTime);
            //transform.Translate(action.moveDir.x * stats.spd * Time.deltaTime   , 0f, 0f);
            //transform.position += action.moveDir * Mathf.RoundToInt(stats.spd * Time.deltaTime);




        }
        else {
            transform.Translate(Vector3.zero);
        }

        if (canClimb)
        {
            transform.Translate(0f, action.moveDir.y * stats.spd * Time.deltaTime, 0f);
            rb.gravityScale = 0;
        }

        else if (!canClimb)
        {
            transform.Translate(Vector3.zero);
            rb.gravityScale = 1;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Toolbox.GetInstance().GetEvent().player = this.gameObject;
            Toolbox.GetInstance().GetEvent().enemyUnit = other.gameObject;
            Toolbox.GetInstance().GetEvent().BattleEncounter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }

}
