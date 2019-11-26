using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public GameObject invUi;

    public List<Item> items = new List<Item>();


    private bool isActive = false;

    #region - - - - - SINGLETON - - - - - 
    void Awake()
    {
        if (instance != null) {
            Debug.LogWarning("Multiple instances!!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback; 

    public int invSpace = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        invUi.GetComponent<InventoryUI>().UpdateUI();

        if (!isActive) {
            invUi.SetActive(true);
            isActive = true;
        } else if (isActive) {
            invUi.SetActive(false);
            isActive = false;
        }
    }

    void EquipWeapon()
    {
        //eWpn = ;
    }

    void EquipShield()
    {
        //eShield = ;
    }

    void UsePotion()
    {

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
