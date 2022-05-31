using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Public Variables 

    public bool canMove = true;
    public float enemyKnockBackThrust = 15f;
    public int damageDoneToHero;
    PlayerMovement target;
    Allies[] alliesTarget;

    #endregion

    #region Private Variables

    [SerializeField] GameObject DamageText;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject enemyAxePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] Transform shootingPos;
    [SerializeField] private float bulletSpeed;
    [SerializeField] GameObject chestPrefab;

    private int xDir;
    private int yDir;
    private float randomNum;
    private Vector2 movement;
    private Vector2 shootDirection;
    [SerializeField] Animator anim;
    [SerializeField] private bool facingRight;
    public int health = 2;
    [SerializeField] AudioClip enemyThrow;
    [SerializeField] [Range(0, 1)] float throwVolume = 0.7f;

    private int gold;

    float shootFromDistance = 15f;
    [SerializeField] float shootingSpeed = 10f;
    [SerializeField] List<AudioClip> enemyNoises;

    #endregion

    private void Start()
    {
        target = GameObject.FindObjectOfType<PlayerMovement>();
        
        StartCoroutine(ChangeDirectionRoutine());
        StartCoroutine(Shoot());
        StartCoroutine(BossLaugh());
        facingRight = true;
        
        gold = Random.Range(0, 100);
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


        if (target.transform.position.x < transform.position.x)
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

    private IEnumerator BossLaugh()
    {
        while (true)
        {
            randomNum = 10f;
            if (gameObject.transform.name == "EnemyBoss")
            {
                float distFromPlayer = Vector2.Distance(target.transform.position, transform.position);
                if (distFromPlayer <= shootFromDistance)
                {
                    randomNum = Random.Range(0, shootingSpeed);
                    FindObjectOfType<AudioSource>().PlayOneShot(enemyNoises[0], throwVolume);
                }
            }
            yield return new WaitForSeconds(randomNum);
        }
    }

    // random movement around map
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
        float randomNumDerp = 10f;
        while (true)
        {
            float distFromPlayer = Vector2.Distance(target.transform.position, transform.position);
            if (distFromPlayer <= shootFromDistance)
            {
                randomNumDerp = Random.Range(0, shootingSpeed);
                GameObject newBullet = Instantiate(enemyAxePrefab, shootingPos.position, Quaternion.identity);

                //target = FindObjectOfType<PlayerMovement>();

                shootDirection = (target.transform.position - shootingPos.position).normalized * bulletSpeed;
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);

                FindObjectOfType<AudioSource>().PlayOneShot(enemyThrow, throwVolume);
            }
            alliesTarget = GameObject.FindObjectsOfType<Allies>();
            foreach (Allies ally in alliesTarget)
            {
                float distFromAllies = Vector2.Distance(ally.transform.position, transform.position);
                if (distFromAllies <= shootFromDistance)
                {
                    randomNumDerp = Random.Range(0, shootingSpeed);
                    GameObject newBullet = Instantiate(enemyAxePrefab, shootingPos.position, Quaternion.identity);

                    //target = FindObjectOfType<PlayerMovement>();

                    shootDirection = (ally.transform.position - shootingPos.position).normalized * bulletSpeed;
                    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);

                    FindObjectOfType<AudioSource>().PlayOneShot(enemyThrow, throwVolume);
                }
            }
            yield return new WaitForSeconds(randomNumDerp);
        }
        
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        FindObjectOfType<AudioSource>().PlayOneShot(enemyNoises[1], throwVolume);

        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "-" + damage + "hp".ToString());

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject drop = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            drop.SendMessage("AddGold", gold);
            FindObjectOfType<PlayerMovement>().SendMessage("GiveXP", 10);
        }
    }

    void AddGold(int goldValue)
    {
        gold += goldValue;
    }

    

    public void AddHealth(int healthValue)
    {
        health += healthValue;
        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "+" + healthValue + "health".ToString());
    }
}
