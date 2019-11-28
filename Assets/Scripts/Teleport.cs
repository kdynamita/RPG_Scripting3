using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : TriggerEvent
{
    public GameObject teleportPoint;

    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            GameObject player = other.gameObject;

            player.layer = LayerMask.NameToLayer("Interactable");
            //other.transform.position = teleportPoint.transform.position;
            player.transform.position = Vector2.Lerp(player.transform.position, teleportPoint.transform.position, eventValue);

            if (other.transform.position == teleportPoint.transform.position) {
                other.gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

}
