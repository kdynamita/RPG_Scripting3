using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1f;
    public int damage;
    public float autoDestroyDelay;

    public GameObject owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

    }

    private void FixedUpdate()
    {
        if (rb)
            Travel();
    }

    void Travel()
    {
        //Moves bullet
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        StartCoroutine(AutoDestroy());
    }

    public IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(autoDestroyDelay);;

        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

        if (otherRb) {
            otherRb.velocity = Vector3.zero;
        }

        if (other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().stats.hp -= damage;
            other.GetComponent<Enemy>().unitPrompt.GetComponent<SpriteRenderer>().sprite = Toolbox.GetInstance().GetManager().GetComponent<GameworldManager>().hurtPrompt;
            other.GetComponent<Enemy>().CheckStats();

        } else if (other.CompareTag("Player")) {
            state pState = other.GetComponent<PlayerController>().pState;
            if (pState != state.blocking) {
                other.GetComponent<PlayerController>().stats.hp -= damage;
            } else {
                damage -= other.GetComponent<PlayerController>().blockDef;
                other.GetComponent<PlayerController>().stats.hp -= damage;
            }
        }

        Debug.Log(other.name);
        Destroy(this.gameObject);
        
    }
}