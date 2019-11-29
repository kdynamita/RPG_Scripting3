using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public GameObject target;

    public void Toggle()
    {
        if (target != null) {

            if (target.gameObject.activeInHierarchy) {
                target.SetActive(false);
            } else {
                target.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player")) {
            Toggle();
        }
    }
}
