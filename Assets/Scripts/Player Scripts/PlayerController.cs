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

    public InventoryUI invUI;

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
        CheckStats();
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
        if (action.attack && shootCount > 0 && pState != state.blocking) {
            shootCount -= 1;

            // - - - - Create Projectile Weapon & assign this rotation & position
            GameObject go = new GameObject("Arrow");

            go.layer = LayerMask.NameToLayer("PlayerAttack");

            go.transform.position = bulletSpawn.transform.position;
            go.transform.rotation = bulletSpawn.transform.rotation;


            // - - - - Add Components
            go.AddComponent<Projectile>();
            go.GetComponent<Projectile>().damage = stats.dex;
            go.AddComponent<Rigidbody2D>();

            // - - - - Add BoxCollider & make it a Trigger
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

            // - - - - Add SpriteRenderer & assign the sprite based on inventory's equipped Weapon
            go.AddComponent<SpriteRenderer>();
            //go.GetComponent<SpriteRenderer>().sprite = invUI.eWpnSprite;

            StartCoroutine(ShootRecover());

        } else {

            return;
        }
        
    }

    void Block()
    {


        if (action.defend) {
            rb.velocity = Vector2.zero;
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

    void CheckStats()
    {
        if (stats.hp <= 0) {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) {
            other.GetComponent<Interactable>().Pickup();
        }
    }


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
