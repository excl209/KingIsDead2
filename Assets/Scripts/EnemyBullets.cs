using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 shootDirection;
    [SerializeField] private float bulletSpeed;

    PlayerMovement target;
    [SerializeField] float damage = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //target = FindObjectOfType<PlayerMovement>();
        
        //shootDirection = (target.transform.position - transform.position).normalized * bulletSpeed;
        //rb.velocity = new Vector2(shootDirection.x, shootDirection.y);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Ally"))
        {
            if(collision.gameObject.name != "BlackDragon")
                collision.gameObject.SendMessage("TakeDamage", damage);

            Destroy(gameObject);
        }
        else if (!collision.gameObject.name.Equals("Enemy"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
