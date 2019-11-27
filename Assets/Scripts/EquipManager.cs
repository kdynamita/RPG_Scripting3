using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    // * I know I shouldn't use multiple singletons, but I've been scrambling just to get this done, been too sick <_> * 
    #region - - - - - Singleton - - - - - 
    public static EquipManager instance;
    Inventory inventory;

    public delegate void OnEquipChanged(Equip newItem, Equip oldItem);
    public OnEquipChanged onEquipChanged;

    private void Awake()
    {
        instance = this;
    }
    #endregion 

    public Equip[] currentEquip;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
        currentEquip = new Equip[numSlots];
    }

    public void Equip (Equip newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equip oldItem = null;

        if (currentEquip[slotIndex] != null) {
            oldItem = currentEquip[slotIndex];
            inventory.AddItem(oldItem);
        }

        if (onEquipChanged != null) {
            onEquipChanged.Invoke(newItem, oldItem);
        }

        currentEquip[slotIndex] = newItem;
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquip[slotIndex] != null) {
            Equip oldItem = currentEquip[slotIndex];
            currentEquip[slotIndex] = null;
        }
    }

}
