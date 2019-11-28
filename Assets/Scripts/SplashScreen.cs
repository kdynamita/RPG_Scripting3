using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float transition;

    void Start()
    {
        GoToNextScreen();
    }

    void GoToNextScreen()
    {
        StartCoroutine(NextCo());
    }

    public IEnumerator NextCo() {
        yield return new WaitForSeconds(transition);
    }
}
