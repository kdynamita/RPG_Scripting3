using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode confirm;
    public KeyCode cancel;
    public KeyCode interact;
    public KeyCode menu;
    public KeyCode pause;

    public KeyCode attack;
    public KeyCode defend;
    public KeyCode counter;

    void Start()
    {
        #region - - - - - Directional Inputs
        up = KeyCode.W;
        down = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;
        #endregion

        #region - - - - - Exploration Inputs - - - - -
        confirm = KeyCode.K;
        cancel = KeyCode.J;
        menu = KeyCode.I;

        pause = KeyCode.Space;
        #endregion

        #region - - - - - Battle Inputs - - - - - 
        attack = KeyCode.L;
        defend = KeyCode.J;
        counter = KeyCode.I;
        #endregion

    }
}