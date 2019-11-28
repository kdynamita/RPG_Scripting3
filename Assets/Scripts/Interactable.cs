
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;
    public Item item;

    void Start()
    {
        this.gameObject.AddComponent<CircleCollider2D>();
        this.gameObject.GetComponent<CircleCollider2D>().radius = 0.75f;
        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public virtual void Pickup()
    {
        Debug.Log("Picking up " + item);
        
        bool pickedUp = Toolbox.GetInstance().GetInventory().AddItem(item);

        if (pickedUp) {
            Destroy(this.gameObject);
        }
    } 

}
