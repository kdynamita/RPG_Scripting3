using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{       
    public void NewGame()
    {
        // - - - Gotta clear player preferences first?
        SceneManager.LoadScene("Play");
    }

    public void LoadGame()
    {
        // Disabled until there's playerprefs saved?
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

    public void ReturnBack()
    {
        SceneManager.LoadScene("Start");
    }

}