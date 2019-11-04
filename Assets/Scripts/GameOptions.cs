using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{       
    public void NewGame()
    {
        SceneManager.LoadScene("Testscene");
    }

    public void LoadGame()
    {

    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}