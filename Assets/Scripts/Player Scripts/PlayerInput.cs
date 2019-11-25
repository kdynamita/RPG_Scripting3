using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

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

        #region - - - - - Action Inputs - - - - -
        defend = KeyCode.K;
        attack = KeyCode.J;
        menu = KeyCode.I;

        pause = KeyCode.Space;
        #endregion


    }
}