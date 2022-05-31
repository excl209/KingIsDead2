using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullets : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 shootDirection;
    [SerializeField] private float bulletSpeed;
    Vector2 lastClickedPos;
    //Enemy enemy;

    Vector3 target;

    void Start()
    {
        //target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        rb = GetComponent<Rigidbody2D>();
        //shootDirection = (target - transform.position).normalized * bulletSpeed;
        //rb.velocity = new Vector2(shootDirection.x, shootDirection.y);
        
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Ally"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1f);
            Destroy(gameObject);
        }
    }
}
