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
    public GameObject removeButton;

    Item item;


    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        button.interactable = true;
        removeButton.SetActive(true);
}

    public void ClearSlot ()
    {
        item = null;
        icon.sprite = emptyIcon;
        button.interactable = false;
        removeButton.SetActive(false);
    }

    public void OnRemoveButton()
    {
        Toolbox.GetInstance().GetInventory().GetComponent<Inventory>().RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null) {
            item.Use();
        }
    }

    public void DropItem()
    {
        if (item != null) {
            item.RemoveFromInventory();
        }
    }

    public void ShowDescription()
    {
        if (item != null) {
            targetText.text = item.itemName + " :  " + item.iText;

        } else { return; }
    }
}
