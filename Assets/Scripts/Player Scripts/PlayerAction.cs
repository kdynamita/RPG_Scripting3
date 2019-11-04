using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public PlayerInput input;

    public Vector3 moveDir;
    public Vector3 rotation;

    private bool confirm;
    private bool cancel;
    private bool menu;

    public bool slash;
    public bool block;
    public bool parry;

    public bool canMove = true;
    public bool canAct = false;

    public bool pause;


    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

        if (input != null)
        {
            if (canMove)
            {
                SetDirection();
                DetectActions();
            }
            if (canAct) {
                DetectBattle();
            }
        }
    }

    void SetDirection()
    {
        // move player up
        if (Input.GetKey(input.up))
        {
            moveDir += Vector3.up;
        }

        // move player down
        else if (Input.GetKey(input.down))
        {
            moveDir += Vector3.down;
        }

        //move player left
        else if (Input.GetKey(input.left))
        {
            // turn player to right
            rotation = new Vector3(0f, 180f, 0f);
            moveDir -= Vector3.left;
        }

        //move player right
        else if (Input.GetKey(input.right))
        {
            // turn player to left
            rotation = Vector3.zero;
            moveDir += Vector3.right;
        }

        //stop movement
        else
        {
            moveDir = Vector3.zero;
        }

        moveDir = moveDir.normalized;
    }

    void DetectActions()
    {
        confirm = Input.GetKey(input.confirm);
        cancel = Input.GetKey(input.cancel);
        menu = Input.GetKey(input.menu);

        // - - - Pause / Unpause - - - 
        pause = Input.GetKeyDown(input.pause);
    }

    void DetectBattle()
    {
        slash = Input.GetKey(input.attack);
        block = Input.GetKey(input.defend);
        parry = Input.GetKey(input.counter);
    }


}