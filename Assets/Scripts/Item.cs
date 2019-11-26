using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [Space]
    public int value;
    [Space]
    public Sprite icon = null;
    [Space]
    public bool isDefault = false;

    public virtual void Use()
    {
        // Use item
        // Make effect happen
        

        Debug.Log("Using " + name);
    }
}
