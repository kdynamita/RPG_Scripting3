using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;

    public InventorySlot[] slots;
    public PlayerController player;

    public Text lvlText;
    public Text hpText;
    public Text dexText;
    public Text defText;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI");
        UpdateStatsUI();

        for (int i=0; i<slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }


    }
    

    public void UpdateStatsUI()
    {
        lvlText.text = player.stats.lvl.ToString();
        hpText.text = player.stats.hp + " / " + player.stats.maxHp;
        dexText.text = player.stats.dex.ToString();
        defText.text = player.stats.def.ToString();
    }
}
