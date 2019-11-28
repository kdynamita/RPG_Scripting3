﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameworldManager : MonoBehaviour
{
    #region - - - - - Variables - - - - - 

    private float promptTimer = 0f;
    private float maxPtimer = 1f;
    [Space]
    public Sprite itemPrompt;
    public Sprite hitPrompt;
    public Sprite hurtPrompt;
    public Sprite blockPrompt;
    public Sprite parryPrompt;
    public Sprite lvlPrompt;
    [Space]
    public GameObject player;
    public SpriteRenderer playerPrompt;
    [Space]
    public Vector3 respawnPoint;

    public StatsManager stats;

    public List<GameObject> unit;
    public List<Sprite> unitPrompt;

    public List<float> unitTimer;

    #endregion



    void Start()
    {
        stats = Toolbox.GetInstance().GetStats().GetComponent<StatsManager>();
        player = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().player;
    
    }



    void Update()
    {
        ManagePlayerPrompt();
        ManageEnemyPrompt();
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
    }

    public void ManagePlayerPrompt()
    {
        if (playerPrompt.sprite != null) {

            if (playerPrompt.gameObject.transform.rotation.y != 0f) {
                playerPrompt.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            promptTimer += Time.deltaTime;

            if (promptTimer >= maxPtimer) {
                playerPrompt.sprite = null;
                promptTimer = 0;
            }
        }
    }

    public void ApplyPrompt(Sprite prompt)
    {
        for (int i=0; i<unit.Count; i++) {
            unitPrompt[i] = unit[i].GetComponent<Enemy>().unitPrompt.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ManageEnemyPrompt() {
    {
            //- - - - - Checks all enemy prompts
            if (stats != null) {
                unit = Toolbox.GetInstance().GetStats().GetComponent<StatsManager>().unit;
            }


            for (int i = 0; i < unit.Count; i++) {

                if (unit.Count <= 0) {
                    return;
                }

                if (unit[i] != null) {

                    if (unit[i].transform.rotation.y != 0f) {
                        unit[i].GetComponent<Enemy>().unitPrompt.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }

                    // if prompt isn't empty, start counting
                    if (unitPrompt[i] != null) {
                        unitTimer[i] += Time.deltaTime;
                    }

                    // If unitTimer reached maxPTimer, unitPrompt[i] will do wonderfully for now
                    if (unitTimer[i] >= maxPtimer) {
                        unitPrompt[i] = null;
                    }
                    unitTimer[i] = 0f;
                }
            }
        }
    }

    void SpawnStuff()
    {

    }

}
