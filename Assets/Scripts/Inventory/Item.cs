using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [Space]
    public int Quantity;
    public int maxQuantity;
    [Space]
    public Sprite icon = null;
    [Space]
    public bool isDefault = false;
    [Space]
    [TextArea(4, 20)] public string iText;
    public int potency;
    public int idItem;

    public virtual void Use()
    {
        PlayerController player = StatsManager.instance.player.GetComponent<PlayerController>();

        if (player.stats.hp < player.stats.maxHp) {
            player.stats.hp += potency;
            RemoveFromInventory();
        }
    }
    public virtual void Drop(){
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}
