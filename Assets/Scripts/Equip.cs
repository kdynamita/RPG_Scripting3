using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "ScriptableObjects/Equip")]
[System.Serializable]
public class Equip : Item
{
    public EquipSlot equipSlot;
    public int damage;
    public int defense;

    public override void Use()
    {
        base.Use();
        EquipManager.instance.Equip(this);
        //Remove item
    }
}

public enum EquipSlot
{ weapon, shield }