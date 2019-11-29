
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float transition;
    bool isDone;

    void Start()
    {
        GoToNextScreen();
    }

    void Update()
    {

    }

    void GoToNextScreen()
    {
        StartCoroutine(NextScreen());
    }

    public IEnumerator NextScreen() {
        yield return new WaitForSeconds(transition);
        SceneManager.LoadScene("Start");
    }
}
