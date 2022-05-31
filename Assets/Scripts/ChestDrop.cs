using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    private int gold;

    public void AddGold(int goldValue)
    {
        gold += goldValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Ally"))
        {
            collision.gameObject.SendMessage("AddGold", gold);
            Destroy(gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up;
        }
            
    }
}
