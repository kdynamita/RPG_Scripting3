using UnityEngine;

public class Toolbox : MonoBehaviour
{
    // Make this script STATIC named as _instance in order to keep it centralized
    // Remain private so that nothing else can edit this script
    private static Toolbox _instance;


    // GetInstance() is the interactable static variable
    // that will check if the script is present in the scene
    // if not, then create a new "Toolbox" gameObject
    // make sure Toolbox persists by making it undestroyable on load
    // add this script to the Toolbox gameObject
    public static Toolbox GetInstance()
    {
        if (Toolbox._instance == null)
        {
            var goToolbox = new GameObject("Toolbox");
            DontDestroyOnLoad(goToolbox);
            Toolbox._instance = goToolbox.AddComponent<Toolbox>();
        }
        return Toolbox._instance;
    }

    //private LevelManager lvlManager;
    //private StatManager statManager;
    private EventManager eventManager;
    private PlayerInput playerInput;
    private PlayerAction playerAction;


    // Before the game starts, check if a Toolbox instance already exists
    // If it does, destroy this one && keep the already existing one 
    // Create a new gameObject called "Manager"
    // make the Toolbox gameObject the parent of the Manager
    // Add the "Manager" script to the gameObject
    void Awake()
    {
        if (Toolbox._instance != null)
        {
            Destroy(this.gameObject);
        }


        var go = new GameObject("Manager");
        go.transform.parent = this.gameObject.transform;
        /*this.lvlManager = go.AddComponent<LevelManager>();
        this.statManager = go.AddComponent<StatManager>(); */
        this.eventManager = go.AddComponent<EventManager>();
        this.playerInput = go.AddComponent<PlayerInput>();
        this.playerAction = go.AddComponent<PlayerAction>();

    }

    // Public function that will allow other objects to pick the same Manager as the Toolbox's
    /*public LevelManager GetLevel()
    {
        return this.lvlManager;
    }

    public StatManager GetStats()
    {
        return this.statManager;
    }*/

    public PlayerInput GetInput()
    {
        return this.playerInput;
    }

    public PlayerAction GetAction()
    {
        return this.playerAction;
    }

    public EventManager GetEvent()
    {
        return this.eventManager;
    }

}