using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public float eventValue;


    void Start()
    {

    }

    public virtual void Trigger()
    {
        //Override stuff here
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Override stuff here!
    }
}

