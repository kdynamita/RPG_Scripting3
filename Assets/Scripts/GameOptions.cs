using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseCanvas;


    public void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PauseGame();
        }
    }

    public void PauseGame() {

        if (!paused) {
            paused = true;
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        } 
        
        else if (paused) {
            paused = false;
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }


    public void NewGame()
    {
        // - - - Gotta clear player preferences first?
        SceneManager.LoadScene("Play");
    }

    public void LoadGame()
    {
        // Disabled until there's playerprefs saved?
        SceneManager.LoadScene("Play");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Start");
    }

}