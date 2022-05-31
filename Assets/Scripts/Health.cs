using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 1;

    public void AddHealth(int healthValue)
    {
        health += healthValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Ally"))
        {
            collision.gameObject.SendMessage("AddHealth", health);
            Destroy(gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up;
        }

    }
}
