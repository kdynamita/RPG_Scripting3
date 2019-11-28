using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector2 respawnPoint;

    void Start()
    {

    }

    void Update()
    {
        if (respawnPoint == Vector2.zero) {
            respawnPoint = StatsManager.instance.player.transform.position;
        }
    }

}
