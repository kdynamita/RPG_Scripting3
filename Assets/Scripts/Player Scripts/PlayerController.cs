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
    dead,
    attacking,
    recovering,
    #endregion
}

public class PlayerController : MonoBehaviour
{
    #region - - - - - VARIABLES - - - - 
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

    [Space]
    public float shootDelay;
    public int shootCount;
    [Space]
    public int blockDef;
    public float timerLimit;
    public float realTimer;
    public float parryReset;
    [Space]
    public GameObject slayer;
    public float reviveDelay;

    #endregion 



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
                if (pState != state.attacking) {
                    Movement();
                }
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
        Equip eWpn = Toolbox.GetInstance().GetEquip().GetComponent<EquipManager>().currentEquip[0];

        if (action.attack && shootCount > 0 && 
            pState != state.blocking && pState != state.dead && 
            eWpn != null) {

            shootCount -= 1;

            pState = state.attacking;
            anim.SetBool("isAttacking", true);

            // - - - - Create Projectile Weapon & assign this rotation & position
            GameObject go = new GameObject("Arrow");

            go.tag = "Projectile";
            go.layer = LayerMask.NameToLayer("PlayerAttack");

            go.transform.position = bulletSpawn.transform.position;
            go.transform.rotation = bulletSpawn.transform.rotation;


            // - - - - Projectile Components & Values - - - - 
            go.AddComponent<Projectile>();
            Projectile goProjectile = go.GetComponent<Projectile>();

            goProjectile.autoDestroyDelay = 5f;
            goProjectile.damage = stats.dex + eWpn.damage;
            goProjectile.owner = this.gameObject;
            goProjectile.speed = 2f;

            go.AddComponent<Rigidbody2D>();
            go.GetComponent<Rigidbody2D>().gravityScale = 0f;

            // - - - - Add BoxCollider & make it a Trigger
            go.AddComponent<BoxCollider2D>();
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

            // - - - - Add SpriteRenderer & assign the sprite based on inventory's eShldped Weapon
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = eWpn.icon;
            go.GetComponent<SpriteRenderer>().sortingLayerName = "Units";
            go.GetComponent<SpriteRenderer>().sortingOrder = 5;

            StartCoroutine(ShootRecover());

        } else {

            return;
        }
        
    }

    public virtual void Block()
    {
        Equip eShld = Toolbox.GetInstance().GetEquip().GetComponent<EquipManager>().currentEquip[1];

        if (action.defend && eShld != null) {


            if (realTimer < timerLimit) {
                realTimer += Time.deltaTime;
                this.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(292, 63, 50);
            } else if (realTimer >= timerLimit){
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }


            rb.velocity = Vector2.zero;
            blockDef = stats.def + eShld.defense;
            pState = state.blocking;
            anim.SetBool("isBlocking", true);

        }

        else {
            pState = state.idle;
            anim.SetBool("isBlocking", false);
            this.GetComponent<SpriteRenderer>().color = Color.white;

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
        pState = state.idle;
        anim.SetBool("isAttacking", false);
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
        anim.SetBool("isDead", true);
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        Toolbox.GetInstance().GetInventory().GetComponent<Inventory>().canUse = false;
        StartCoroutine(DeathCo());
    }

    public IEnumerator DeathCo()
    {
        Toolbox.GetInstance().GetStats().GetComponent<StatsManager>().CheckEnemyLevelUp();
        yield return new WaitForSeconds(3f);
        Revive();

    }

    public void Revive()
    {
        if (pState == state.idle) {
            return;
        } 
        
        else {
            Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().Respawn();
            stats.hp = stats.maxHp;
            anim.SetBool("isDead", false);
            pState = state.idle;
            gameObject.layer = LayerMask.NameToLayer("Player");
            Toolbox.GetInstance().GetInventory().GetComponent<Inventory>().canUse = true;
        }
    }

    #region - Trigger Functions - 
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Interactable")) {
            other.GetComponent<Interactable>()?.Pickup();
            Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().playerPrompt.sprite = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().itemPrompt;
        }


        if (other.CompareTag("Projectile")) {

            slayer = other.GetComponent<Projectile>().owner.gameObject;

            if (pState == state.blocking && realTimer < timerLimit) {
                stats.hp += other.GetComponent<Projectile>().damage+1;
                Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().playerPrompt.sprite = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().parryPrompt;
            } 
            
            else if (pState == state.blocking && realTimer >= timerLimit) {
                Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().playerPrompt.sprite = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().blockPrompt;
            } 
            
            else {
                Toolbox.GetInstance().GetStats().GetComponent<StatsManager>().UpdateStats();
                Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().playerPrompt.sprite = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().hurtPrompt;
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
