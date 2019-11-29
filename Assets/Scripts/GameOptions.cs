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
        if (pauseCanvas != null) {
            if (!paused) {
                paused = true;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log(paused);
            } else if (paused) {
                paused = false;
                pauseCanvas.SetActive(false);
                Time.timeScale = 1f;
                Debug.Log(paused);
            }
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Play");
    }

    public void LoadGame()
    {
        // Disabled until there's playerprefs saved?
        Time.timeScale = 1;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}