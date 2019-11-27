using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Sprite emptyIcon;
    public Button button;
    public Text targetText;

    Item item;

    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        button.interactable = true;
    }

    public void ClearSlot ()
    {
        item = null;
        icon.sprite = emptyIcon;
        button.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null) {
            item.Use();
        }
    }

    public void ShowDescription()
    {
        if (item != null) {
            targetText.text = item.itemName + " :  " + item.iText;

        } else { return; }
    }
}
