using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : TriggerEvent
{
    public GameObject teleportPoint;
    public Color lineColor;

    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Gizmos.DrawLine(transform.position, teleportPoint.transform.position);

    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            GameObject player = other.gameObject;

            player.layer = LayerMask.NameToLayer("Interactable");
            player.transform.position = Vector2.Lerp(player.transform.position, teleportPoint.transform.position, eventValue);

            if (player.transform.position == teleportPoint.transform.position) {
                player.gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }

        if (other.CompareTag("Enemy")) {
            GameObject enemy = other.gameObject;
            enemy.layer = LayerMask.NameToLayer("Interactable");
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, teleportPoint.transform.position, eventValue);

            if (enemy.transform.position == teleportPoint.transform.position) {
                enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

        }
    }

}
