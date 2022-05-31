using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject axePrefab;
    public float bulletSpeed = 5f;
    [SerializeField] Transform shootingPos;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool facingRight;
    [SerializeField] Animator anim;
    [SerializeField] int health = 5;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] [Range(0, 1)] float audioVolume = 0.7f;
    private CircleCollider2D col;
    private SpriteRenderer sr;
    [SerializeField] GameObject DamageText;

    Vector2 lastClickedPos;
    private Vector2 shootDirection;
    Rigidbody2D rb;
    bool canShoot = true;

    bool moving;
    bool enabledPlayer = true;

    GameSession gameSession;

    [SerializeField] int gold = 0;

    public Vector2 moveVal;
    public float triggerVal;
    private float shootingSpeed = 5f;
    [SerializeField] int xp = 0;
    [SerializeField] bool winState;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        gameSession = FindObjectOfType<GameSession>();
        col = GetComponent<CircleCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        enablePlayer(false);
    }

    

    public void enablePlayer(bool enable)
    {
        col.enabled = enable;
        sr.enabled = enable;
        enabledPlayer = enable;
        health = 5;
        gold = 0;

        if (enable)
        {
            transform.position = Vector2.zero;
        }
        canShoot = enable;
    }

    private void FixedUpdate()
    {
        if (enabledPlayer && triggerVal == 1)
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (lastClickedPos.x < transform.position.x)
            {
                if (facingRight)
                    facingRight = false;
            }
            else
            {
                if (!facingRight)
                    facingRight = true;
            }
            Vector3 aim;
            if (facingRight)
            {
                aim = (transform.position + new Vector3(1f, 0f));
            }
            else
                aim = (transform.position + new Vector3(-1f, 0f));

            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot(aim, lastClickedPos));
            }
            triggerVal = 0;
        }


        if (moving && enabledPlayer)
        {
            anim.SetFloat("xInput", moveVal.x);
            anim.SetFloat("yInput", moveVal.y);
            anim.speed = 0.2f;

            float step = speed * Time.deltaTime;
            rb.MovePosition(rb.position + moveVal * speed * Time.fixedDeltaTime);
        }
        else
        {
            moving = false;
            anim.speed = 0f;
        }

        rb.velocity = Vector2.zero;
    }

    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
        moving = true;
    }

    void OnAttack(InputValue value)
    {
        triggerVal = value.Get<float>();
    }

    private IEnumerator Shoot(Vector3 aim, Vector3 lastClickedPos)
    {
        GameObject tempBullet;
        tempBullet = Instantiate(axePrefab, aim, Quaternion.identity) as GameObject;
        
        shootDirection = (lastClickedPos - aim).normalized * bulletSpeed;
        tempBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);

        FindObjectOfType<AudioSource>().PlayOneShot(audioClips[2], audioVolume);
        yield return new WaitForSeconds(shootingSpeed);
        canShoot = true;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        FindObjectOfType<AudioSource>().PlayOneShot(audioClips[1], audioVolume);
        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "-" + damage + "hp".ToString());
        if (health <= 0)
        {
            Vector2 whereDidIDie = transform.position;
            AudioListener.pause = true;
            
            FindObjectOfType<AudioSource>().PlayOneShot(audioClips[0], audioVolume);
            AudioListener.pause = false;
            gameSession.PlaceATombStone(whereDidIDie);
            FindObjectOfType<GameSession>().SendMessage("SetHighScore", gold);
            //gameSession.SetHighScore(5);
            FindObjectOfType<WorldSystem>().LoadGameOver();
        }
            
    }

    void AddGold(int goldValue)
    {
        gold += goldValue;
        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "+" + goldValue + "gold".ToString());
    }

    public string GetGold()
    {
        return "Gold: " + gold;
    }

    public string GetLives()
    {
        return "Hp: " + health;
    }

    public string GetLevel()
    {
        return "Level: " + (1 + (xp / 100));
    }

    public void GiveXP(int xpToGive)
    {
        xp += xpToGive;

        switch (1 + (xp / 100))
        {
            case 5: bulletSpeed -= 1; health += 1; break;
            case 10: bulletSpeed -= 1; health += 1; break;
            case 15: bulletSpeed -= 1; health += 1; break;
            case 20: bulletSpeed -= 1; health += 1; break;
        }
    }

    public void AddHealth(int healthValue)
    {
        health += healthValue;
        GameObject damageTxt = Instantiate(DamageText, transform.position, Quaternion.identity);
        damageTxt.SendMessage("PopupMyMessage", "+" + healthValue + "health".ToString());
    }

    public void setEndGame()
    {
        winState = true;

        //FindObjectOfType<MainMenu>().SendMessage("DisplayEndGame");

        FindObjectOfType<WorldSystem>().LoadGameOver();
    }

    public bool getEndGame()
    {
        return winState;
    }
}
