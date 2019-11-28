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
    dead
    #endregion
}

public class PlayerController : MonoBehaviour
{
    public state pState;
    [Space]
    public PlayerAction action;
    public Stats stats;
    public InventoryUI invUI;
    public Animator anim;
    [Space]
    private Rigidbody2D rb;
    public GameObject bulletSpawn;
    [Space]
    public bool paused;
    public bool canClimb;
    public bool isReviving;
    [Space]
    public float shootDelay;
    public int shootCount;
    [Space]
    public int blockDef;
    public float timerLimit;
    public float realTimer;
    public float parryReset;
    [Space]
    public int slayer;
    public float reviveDelay;



    // Start is called before the first frame update
    void Start()
    {
        action = GetComponent<PlayerAction>();
        //stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        pState = state.idle;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RunStates();
        CheckStats();
    }

    void RunStates()
    {
        if (pState != state.dead) {
            if (pState != state.blocking) {
                Movement();
                Shoot();
            }
            Block();
        }
    }

    #region - - - - - - ACTION FUNCTIONS - - - - - 
    public virtual void Movement()
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

    public virtual void Shoot()
    {

        Equip eShld = EquipManager.instance.currentEquip[0];

        if (action.attack && shootCount > 0 && 
            pState != state.blocking && pState != state.dead && 
            eShld != null) {
            shootCount -= 1;

            // - - - - Create Projectile Weapon & assign this rotation & position
            GameObject go = new GameObject("Arrow");

            go.tag = "Projectile";
            go.layer = LayerMask.NameToLayer("PlayerAttack");

            go.transform.position = bulletSpawn.transform.position;
            go.transform.rotation = bulletSpawn.transform.rotation;


            // - - - - Projectile Components & Values - - - - 
            go.AddComponent<Projectile>();
            Projectile goProjectile = go.GetComponent<Projectile>();

            goProjectile.autoDestroyDelay = 3f;
            goProjectile.damage = stats.dex + eShld.damage;
            goProjectile.owner = this.gameObject;

            go.AddComponent<Rigidbody2D>();
            go.GetComponent<Rigidbody2D>().gravityScale = 0f;

            // - - - - Add BoxCollider & make it a Trigger
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

            // - - - - Add SpriteRenderer & assign the sprite based on inventory's eShldped Weapon
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = eShld.icon;

            StartCoroutine(ShootRecover());

        } else {

            return;
        }
        
    }

    public virtual void Block()
    {
        Equip eWpn = EquipManager.instance.currentEquip[1];

        if (action.defend && eWpn != null) {


            if (realTimer < timerLimit) {
                realTimer += Time.deltaTime;
            }


            rb.velocity = Vector2.zero;
            blockDef = stats.def + eWpn.defense;
            pState = state.blocking;
            anim.SetBool("isBlocking", true);
        }

        else {
            pState = state.idle;
            anim.SetBool("isBlocking", false);

            if (realTimer > 0) {
                StartCoroutine(ParryCo());
            }
        }
    }

    public IEnumerator ParryCo()
    {
        yield return new WaitForSeconds(parryReset);
        realTimer = 0f;
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
        pState = state.dead;
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        Inventory.instance.canUse = false;
        StartCoroutine(Revive());
    }

    public IEnumerator Revive()
    {
        StatsManager.instance.CheckEnemyLevelUp();
        yield return new WaitForSeconds(0.06f);



        // REVIVE YOUR BOY
    }


    #region - Trigger Functions - 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) {
            other.GetComponent<Interactable>().Pickup();
        }

        if (other.CompareTag("Projectile")) {

            if (pState == state.blocking && realTimer < timerLimit) {
                stats.hp += other.GetComponent<Projectile>().damage+1;
            } else {
                StatsManager.instance.UpdateStats();
            }
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
    #endregion

}
