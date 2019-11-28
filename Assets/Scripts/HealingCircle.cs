using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : TriggerEvent
{
    [SerializeField]int heal;
    Sprite disabledSprite;

    public override void Trigger() {
        if (eventValue <= 0) {
            //disable
            GetComponent<SpriteRenderer>().sprite = disabledSprite;
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(this.GetComponent<HealingCircle>());
        }
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && 
            other.GetComponent<PlayerController>().stats.hp != other.GetComponent<PlayerController>().stats.maxHp &&
            eventValue > 0) {
            other.GetComponent<PlayerController>().stats.hp += heal;
            eventValue -= 1;

        } else { return; }
    }


}
