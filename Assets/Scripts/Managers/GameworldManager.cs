using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameworldManager : MonoBehaviour
{
    #region - - - - - Variables - - - - - 

    public static GameworldManager instance;

    void Awake()
    {
        instance = this; 
    }

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

    #endregion



    void Start()
    {
        GameObject player = StatsManager.instance.player;
    }

    void Update()
    {
        ManagePlayerPrompt();
    }

    public void Respawn()
    {
        PlayerController player = StatsManager.instance.playerController;
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

    void SpawnStuff()
    {
        // spawn enemies
        // spawn items
    }


}
