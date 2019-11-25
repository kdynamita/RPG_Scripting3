using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1f;

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
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}