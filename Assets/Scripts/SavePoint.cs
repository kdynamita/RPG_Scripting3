﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public LevelManager lvlManager;
    public SavePlayerPrefs savePrefs;
    public Transform spawnPoint;

    public SpriteRenderer spriteSlot;

    public Sprite savePrompt;
    public Sprite saving;
    public Sprite saved;

    public bool hasSaved = false;
    public bool isIn = false;



    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.AddComponent<BoxCollider2D>();
        //this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5f, 5f);
        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }


    void FixedUpdate()
    {
        ConfirmSave();
    }

    public void Save() {
        savePrefs.Save();
    }

    void ConfirmSave() {
        if (Input.GetKeyDown(KeyCode.K)) {
            if (!hasSaved && isIn) {
                lvlManager.respawnPoint = spawnPoint.transform.position;
                spriteSlot.sprite = saving;
                hasSaved = true;
                StartCoroutine("SaveFinished");
                Save();
            }
        } 


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            spriteSlot.sprite = savePrompt;
            isIn = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            spriteSlot.sprite = null;
            isIn = false;
            hasSaved = false;
        }   
    }

    public IEnumerator SaveFinished() {
        yield return new WaitForSeconds(0.5f);
        spriteSlot.sprite = saved;
    }
}
