using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetToolbox : MonoBehaviour
{
    void Start()
    {
        Toolbox.GetInstance();

        Destroy(this.gameObject);
    }

}