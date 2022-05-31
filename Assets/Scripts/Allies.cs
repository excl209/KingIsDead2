using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
    [SerializeField] private bool facingRight;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    bool canMove = true;
    Animator anim;
    private Vector2 movement;
    private float randomNum;
    Enemy[] target;
    float shootFromDistance = 15f;
    [SerializeField] Transform shootingPos;
    [SerializeField] private List<GameObject> bulletPrefab;
    public int health = 2;
    [SerializeField] List<AudioClip> allyNoises;
    [SerializeField] GameObject DamageText;
    [SerializeField] [Range(0, 1)] float allyVolume = 0.7f;
    int gold = 10;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] float shootingSpeed = 5f;
    private Vector2 shootDirection;
    [SerializeField] private float bulletSpeed = 3f;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionRoutine());
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Shoot());
        //StartCoroutine(BossLaugh());
        facingRight = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        anim.SetFloat("xInput", movement.x);
        anim.SetFloat("yInput", movement.y);
        anim.speed = 0.2f;


        if (movement.x < 0)
        {
            if (facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = false;
            }
        }
        else
        {

            if (!facingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = true;
            }
        }

    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            randomNum = Random.Range(-5, 5);
            movement.x = Random.Range(-1, 2);
            movement.y = Random.Range(-1, 2);
            yield return new WaitForSeconds(randomNum);
        }
    }

    private IEnumerator Shoot()
    {
        float timeUntilNextShot = 10f;
        while (true)
        {
            Vector3 mainTargets = Vector3.zero;
            timeUntilNextShot = Random.Range(0, shootingSpeed);
            target = FindObjectsOfType<Enemy>();
            foreach (var it in target)
            {
                if (it != null)
                {
                    
                    float distFromEnemy = Vector2.Distance(it.transform.position, transform.position);
                    if (distFromEnemy <= shootFromDistance)
                    {
                        if(Vector2.Distance(it.transform.position, transform.position) < Vector2.Distance(mainTargets, transform.position))
                            mainTargets = it.transform.position;
                    }
                }
            }
            if (mainTargets != Vector3.zero)
            {
                if (transform.position.x < mainTargets.x)
                {
                    movement.x = 0;

                    GameObject newBullet = Instantiate(bulletPrefab[0], transform.position + new Vector3(1f, 0f), Quaternion.identity);
                    shootDirection = (mainTargets - transform.position).normalized * bulletSpeed;
                    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);
                    Destroy(newBullet, 5f);
                }
                else
                {
                    movement.x = 0;

                    GameObject newBullet = Instantiate(bulletPrefab[0], transform.position + new Vector3(-1f, 0f), Quaternion.identity);
                    shootDirection = (mainTargets - transform.position).normalized * bulletSpeed;
                    newBullet.GetComponent<SpriteRenderer>().flipX = true;
                    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);
                    Destroy(newBullet, 5f);
                }
                
                //FindObjectOfType<AudioSource>().PlayOneShot(enemyThrow, throwVolume);
            }

            yield return new WaitForSeconds(timeUntilNextShot);
        }

    }

    void TakeDamage(int damage)
    {
        health -= damage;

        FindObjectOfType<AudioSource>().PlayOneShot(allyNoises[0], allyVolume);

        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "-" + damage + "hp".ToString());

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject drop = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            drop.SendMessage("AddGold", gold);
            //FindObjectOfType<PlayerMovement>().SendMessage("GiveXP", 10);
        }
    }

    public void AddHealth(int healthValue)
    {
        health += healthValue;
        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "+" + healthValue + "health".ToString());
    }
}
