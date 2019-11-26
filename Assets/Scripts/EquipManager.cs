using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region - - - - - Singleton - - - - - 
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion 

    Equip[] currentEquip;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
        currentEquip = new Equip[numSlots];
    }

    public void Equip (Equip newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        currentEquip[slotIndex] = newItem;
    }
}
