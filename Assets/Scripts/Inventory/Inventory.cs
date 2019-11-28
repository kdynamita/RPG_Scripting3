using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public bool canUse = true;

    public GameObject invUi;

    public List<Item> items = new List<Item>();


    private bool isActive = false;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback; 

    public int invSpace = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && canUse) {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        invUi.GetComponent<InventoryUI>().UpdateUI();

        if (!isActive) {
            invUi.SetActive(true);
            isActive = true;
            Time.timeScale = 0;
        } else if (isActive) {
            invUi.SetActive(false);
            isActive = false;
            Time.timeScale = 1;
        }
    }

    public bool AddItem(Item item)
    {
        if (!item.isDefault) {
            if (items.Count >= invSpace) {
                Debug.Log("Inventory is FULL!");
                return false;
            }

            items.Add(item);

            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
