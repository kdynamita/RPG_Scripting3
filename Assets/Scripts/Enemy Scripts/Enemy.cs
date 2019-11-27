using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    public state eState;
    [Space]
    public GameObject player;
    //public AIPath aiPath;
    public Stats stats;
    [Space]
    public float atkDelay;
    public float atkRecover;
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

    public int unitIndex = 0;
    public bool hasLeveled;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }


    void FixedUpdate()
    {
        RunState();
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
            player = StatsManager.instance.player;
        } else { return; }
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
        } */

    }

    void UpdatePath()
    {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
        }
    }


    public void Movement()
    {
        if (path == null || player.GetComponent<PlayerController>().pState == state.dead) {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndPath = true;
            return;
        } else {
            reachedEndPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * stats.spd * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        float distance2Player = Vector2.Distance(rb.position, player.transform.position);
        nextWaypoint = Random.Range(minRange, maxRange);

        if (distance < nextWaypoint) {
            if (distance2Player < nextWaypoint) {
                rb.velocity = Vector2.zero;
                if (atkCount > 0) {
                    StartCoroutine(Attack());
                }
            } else {
                currentWaypoint++;
            }
        }

    }

    public virtual IEnumerator Attack()
    {
        if (weapon != null) {
            atkCount -= 1;

            yield return new WaitForSeconds(atkDelay);

            GameObject go = new GameObject("Arrow");

            go.tag = "Projectile";
            go.layer = LayerMask.NameToLayer("EnemyAttack");


            go.transform.position = bulletSpawn.transform.position;
            go.transform.rotation = bulletSpawn.transform.rotation;


            // - - - - Add Components
            go.AddComponent<Projectile>();
            Projectile goProjectile = go.GetComponent<Projectile>();

            goProjectile.autoDestroyDelay = 3f;
            goProjectile.damage = stats.dex; //+ EquipManager.instance.currentEquip[0].damage;
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

        }

        StartCoroutine(AttackRecover());

    }

    public IEnumerator AttackRecover()
    {
        yield return new WaitForSeconds(atkRecover);
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

        if (item != null) {
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

}
