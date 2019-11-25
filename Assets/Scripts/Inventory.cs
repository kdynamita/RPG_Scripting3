using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ScriptableObject items;
    public Sprite eWpn;
    public Sprite eShield;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Equip();
    }

    void Equip()
    {
        if (Input.GetKey(KeyCode.Q)) {

            for (int i = 0; i < items.Length; i++) {

                if (i < 1) {
                    i -= 1;
                    Debug.Log(i);
                } else {
                    return;
                }
            }
            
        }

        if (Input.GetKey(KeyCode.R)) {
            for (int i = 0; i < items.Length; i++) {

                if (i >= items.Length) {
                    i += 1;
                    Debug.Log(i);
                } else {
                    return;
                }
            }
        }
    }

    void AddObject()
    {

    }
}
