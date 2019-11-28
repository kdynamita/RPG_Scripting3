using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{

    public delegate void OnEquipChanged(Equip newItem, Equip oldItem);
    public OnEquipChanged onEquipChanged;


    public Equip[] currentEquip;

    private void Start()
    {

        int numSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
        currentEquip = new Equip[numSlots];
    }

    public void Equip (Equip newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equip oldItem = null;

        if (currentEquip[slotIndex] != null) {
            oldItem = currentEquip[slotIndex];
            Toolbox.GetInstance().GetInventory().GetComponent<Inventory>().AddItem(oldItem);
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
