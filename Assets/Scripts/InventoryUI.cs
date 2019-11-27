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

    public Image wpnSprite;
    public Image shldSprite;

    public Image wpnHud;
    public Image shldHud;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        UpdateStatsUI();
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
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }

        UpdateStatsUI();




    }
    

    public void UpdateStatsUI()
    {
        Equip currentWpn = EquipManager.instance.currentEquip[0];
        Equip currentShld = EquipManager.instance.currentEquip[1];

        lvlText.text = player.stats.lvl.ToString();
        hpText.text = player.stats.hp + " / " + player.stats.maxHp;

        if (currentWpn == null) {
            dexText.text = player.stats.dex.ToString();
        }

        if (currentShld == null) {
            defText.text = player.stats.def.ToString();
        }

        if (currentWpn != null) {
            dexText.text = (player.stats.dex + EquipManager.instance.currentEquip[0].damage).ToString();
            wpnSprite.sprite = currentWpn.icon;
            wpnHud.sprite = wpnSprite.sprite;
        }

        if (currentShld != null) {
            defText.text = (player.stats.def + EquipManager.instance.currentEquip[1].defense).ToString();
            shldSprite.sprite = currentShld.icon;
            shldHud.sprite = shldSprite.sprite;
        }




    }
}
