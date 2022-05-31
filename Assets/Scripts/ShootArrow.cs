using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Ally"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1f);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.name.Equals("Enemy"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
