using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    #region - - - - Overworld States - - - - 
    idle,
    walking,
    blocking,
    climbing,
    #endregion
}

public class PlayerController : MonoBehaviour
{
    public PlayerAction action;
    public Stats stats;

    private Rigidbody2D rb;
    public bool paused;

    public state state;

    public bool canClimb;

    public GameObject bulletSpawn;
    public float shootDelay;
    public int shootCount;

    public Inventory inventory;

    public Animator anim;

    [SerializeField] private state pState;


    // Start is called before the first frame update
    void Start()
    {
        action = GetComponent<PlayerAction>();
        //stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        state = state.idle;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RunStates();
    }

    void RunStates()
    {
        Movement();
        Shoot();
        Block();
    }

    #region - - - - - - ACTION FUNCTIONS - - - - - 
    void Movement()
    {
        // Movement Code
        if (action.moveDir.x != 0)
        {
            transform.rotation = Quaternion.Euler(action.rotation);
            bulletSpawn.transform.rotation = Quaternion.Euler(action.rotation);

            if (pState != state.blocking) 
                rb.velocity = new Vector2(action.moveDir.x * stats.spd, 0f);
        }
        else {
            rb.velocity = Vector2.zero;
        }

        if (canClimb)
        {
            transform.Translate(0f, action.moveDir.y * stats.spd * Time.deltaTime, 0f);
            rb.gravityScale = 0;
        }

        else if (!canClimb)
        {
            transform.Translate(Vector3.zero);
            rb.gravityScale = 1;
        }

    }

    void Shoot()
    {
        if (action.attack && shootCount > 0) {
            shootCount -= 1;
            GameObject go = new GameObject("Arrow");
            go.transform.position = bulletSpawn.transform.position;
            go.transform.rotation = bulletSpawn.transform.rotation;
            go.AddComponent<Projectile>();
            go.AddComponent<Rigidbody2D>();
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = inventory.eWpn;
            StartCoroutine(ShootRecover());

        } else {

            return;
        }
        
    }

    void Block()
    {
        if (action.defend) {
            pState = state.blocking;
            anim.SetBool("isBlocking", true);
        }

        else {
            pState = state.idle;
            anim.SetBool("isBlocking", false);
        }
    }

    public IEnumerator ShootRecover()
    {
        yield return new WaitForSeconds(shootDelay);
        shootCount = 1;
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }

}
