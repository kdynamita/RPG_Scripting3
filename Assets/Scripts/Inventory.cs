using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Weapon eWpn;
    public Sprite eWpnSprite;
    public Image eWpnImg;
    public Text eWpnTxt;

    public ScriptableObject eShield;
    public Sprite eShieldSprite;
    public Image eShieldImg;
    public Text eShieldTxt;



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
        if (eWpnSprite == null) {
            eWpnSprite = eWpn.wpnSprite;
            eWpnImg.sprite = eWpnSprite;
            eWpnTxt.text = eWpn.wpnName;
        }

        if (Input.GetKey(KeyCode.Q)) {

            
        }

        if (Input.GetKey(KeyCode.R)) {

        }
    }

    void AddObject()
    {

    }
}
