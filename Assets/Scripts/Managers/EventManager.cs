using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyUnit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleEncounter()
    {
        SceneManager.LoadScene("Battle");
    }
}
