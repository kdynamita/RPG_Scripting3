using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    public state eState;
    [Space]
    public GameObject player;
    public AIPath aiPath;
    public Stats stats;
    [Space]
    public float atkDelay;
    public float atkRecover;
    public float moveSpd;
    [Space]
    private float nextWaypoint;
    public float minRange;
    public float maxRange;
    [Space]
    public int atkCount;
    public int maxAtkCount;
    public float projectileSpd;

    [Space]
    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;
    [Space]
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 rotation;
    [Space]
    public GameObject bulletSpawn;
    public Equip weapon;
    public Equip shield;
    public Item item;
    public Animator anim;
    public Sprite corpse;
    public GameObject unitPrompt;
    [Space]
    public int minDrop;
    public int unitIndex = 0;
    public bool hasLeveled;
    [Space]
    public float minRangeAlert;





    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

 
    }


    void FixedUpdate()
    {
        if (eState != state.dead) {
            RunState();
        } else {
            return;
        }
    }

    void RunState()
    {
        
        AssignPlayer();
        FlipSprite();

        if (eState != state.dead) {
            Movement();
        }
    }

    void AssignPlayer()
    {
        if (player == null) {
            player = Toolbox.GetInstance().GetStats().player;
        }
    }



    void OnPathComplete(Path p)
    {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }


    public void FlipSprite()
    {
        transform.rotation = Quaternion.Euler(rotation);
        bulletSpawn.transform.rotation = Quaternion.Euler(rotation);
        
        if (player.transform.position.x < rb.position.x) {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            rotation = new Vector3(0f, 180f, 0f);
        } else if (player.transform.position.x > rb.position.x)  {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            rotation = new Vector3(0f, 0f, 0f);
        }
        
        /*
        if (aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }  */
         
    }


    void UpdatePath()
    {
        if (seeker.IsDone()) {
                seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
        }
    }


    public void Movement()
    {

        if (path == null || player.GetComponent<PlayerController>().pState == state.dead || eState == state.dead || eState == state.attacking) {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndPath = true;
            return;
        } else {
            reachedEndPath = false;
        }

        eState = state.walking;
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * stats.spd * Time.deltaTime;


        //rb.AddForce(force);
        rb.velocity = new Vector2(force.x * moveSpd * Time.deltaTime, 0f);



        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        float distance2Player = Vector2.Distance(rb.position, player.transform.position);
        nextWaypoint = Random.Range(minRange, maxRange);

        if (distance < nextWaypoint) {
            if (distance2Player < nextWaypoint) {
                rb.velocity = Vector2.zero;
                if (atkCount > 0 || eState != state.dead) {
                    if (rb.velocity == Vector2.zero) {
                        StartCoroutine(Attack());
                    }
                }
                currentWaypoint++;
            }
        }
    }

    public virtual IEnumerator Attack()
    {
        if (weapon != null) {
            atkCount -= 1;

            if (eState != state.dead) {

                eState = state.attacking;
                anim.SetBool("isAttacking", true);
                yield return new WaitForSeconds(atkDelay);

                GameObject go = new GameObject("Arrow");

                rb.velocity = Vector2.zero;
                go.tag = "Projectile";
                go.layer = LayerMask.NameToLayer("EnemyAttack");


                go.transform.position = bulletSpawn.transform.position;
                go.transform.rotation = bulletSpawn.transform.rotation;


                // - - - - Add Components
                go.AddComponent<Projectile>();
                Projectile goProjectile = go.GetComponent<Projectile>();

                goProjectile.autoDestroyDelay = 5f;
                goProjectile.damage = stats.dex + weapon.damage;
                goProjectile.owner = this.gameObject;
                goProjectile.speed = projectileSpd;

                go.AddComponent<Rigidbody2D>();
                go.GetComponent<Rigidbody2D>().gravityScale = 0f;

                // - - - - Add BoxCollider & make it a Trigger
                go.AddComponent<BoxCollider2D>();
                go.GetComponent<BoxCollider2D>().isTrigger = true;
                go.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

                // - - - - Add SpriteRenderer & assign the sprite based on inventory's equipped Weapon
                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = weapon.icon;
                go.GetComponent<SpriteRenderer>().sortingLayerName = "Units";
                go.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

        }
            StartCoroutine(AttackRecover());
    }

    public IEnumerator AttackRecover()
    {

        yield return new WaitForSeconds(atkRecover);
        anim.SetBool("isAttacking", false);
        if (eState != state.dead) {
            eState = state.idle;
        }
        atkCount = maxAtkCount;
    }


    public void CheckStats()
    {
        if (stats.hp <= 0) {
            Death();
        }

    }

    void Death()
    {
        eState = state.dead;
        anim.SetBool("isDead", true);
        gameObject.layer = LayerMask.NameToLayer("Dead");

        player.GetComponent<PlayerController>().stats.exp = stats.exp;
        if (item != null) {
            int dropChance = Random.Range(0, 100);

            if (dropChance < minDrop) {
                GameObject go = new GameObject("Drop");
                go.tag = "Interactable";
                go.transform.position = this.transform.position;

                go.AddComponent<Interactable>();
                go.GetComponent<Interactable>().item = item;

                go.AddComponent<BoxCollider2D>();
                go.GetComponent<BoxCollider2D>().isTrigger = true;
                go.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = item.icon;
            }
        }

        StartCoroutine(DestroyAll());
    }

    public IEnumerator DestroyAll()
    {
        yield return new WaitForSeconds(1f);

        GameObject go = new GameObject("Corpse");
        go.transform.position = this.transform.position;
        if (player.transform.position.x < this.transform.position.x) {
            go.transform.localScale = new Vector2(-1f, 1f);
        } else if (player.transform.position.x > this.transform.position.x) {
            go.transform.localScale = new Vector2(-1f, 1f);
        }
        go.AddComponent<SpriteRenderer>().sprite = corpse;
        go.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        go.GetComponent<SpriteRenderer>().sortingOrder = 1;



        Destroy(this.gameObject);
    }

}
