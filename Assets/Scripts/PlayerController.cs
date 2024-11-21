using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 moveDirection;
    public float previousHorizontalVector;
    public float previousVertivcalVector;
    public float knockBackForce = 5f;
    public float knockBackRadius = 3f;
    [SerializeField] float takeDamageCoolDownTime;
    [SerializeField] GameObject sword;
    [SerializeField] AudioClip damageClip;
    Rigidbody2D rb;
    bool takeDamageIsCoolDown;
    bool isSwordCoolDown;
    float swordCoolDown;

    Vector3 mousePosition;
    Vector3 worldMousePositioin;
    Vector3 mouseDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        isSwordCoolDown = false;

        rb = GetComponent<Rigidbody2D>();
        takeDamageIsCoolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = GameManager.instance.playerMoveSpeed;
        swordCoolDown = GameManager.instance.swordCoolDown;

        mousePosition = Input.mousePosition;
        worldMousePositioin = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = worldMousePositioin - transform.position;
        mouseDirection.z = 0f;

        UpdateDirection();
        Move();

        if (Input.GetButtonDown("Fire2") && !isSwordCoolDown)
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Quaternion swordRotation = Quaternion.Euler(0f, 0f, angle);

            GameObject spawnSword = Instantiate(sword, transform.position, swordRotation, transform);
            spawnSword.gameObject.GetComponent<Sword>().Instantiate(mouseDirection);

            StartCoroutine(SwordCoolDown());
        }
    }

    void UpdateDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection.x != 0)
        {
            previousHorizontalVector = moveDirection.x;
        }
        if (moveDirection.y != 0)
        {
            previousVertivcalVector = moveDirection.y;
        }
    }
    

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void PlayerGetHit()
    {
        Collider2D[] knockBackEnemies = Physics2D.OverlapCircleAll(transform.position, knockBackRadius);

        foreach (Collider2D enemy in knockBackEnemies)
        {
            Enemy E = enemy.GetComponent<Enemy>();
            if (E != null)
            {
                E.isKnockBack = true;
                Vector3 knockBackDirection = (enemy.transform.position - transform.position).normalized;

                StartCoroutine(E.ApplyKnockback(knockBackDirection));
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if ((other.gameObject.tag == "Devil" || other.gameObject.tag == "Slime" ||
        other.gameObject.tag == "Goblin" || other.gameObject.tag == "Giant Goblin" ||
        other.gameObject.tag == "Skeleton" || other.gameObject.tag == "Pumpkin")
         && !takeDamageIsCoolDown)
        {
            SoundEffectsManager.instance.PlaySoundEffectClip(damageClip, transform, 1f);
            
            StartCoroutine(CollisionisCoolDown());

            GameManager.instance.DealDamageToPlayer(GameManager.instance.enemyDamage);
            
            PlayerGetHit();

            
        
            
        }
    }

    IEnumerator CollisionisCoolDown()
    {
        takeDamageIsCoolDown = true;
        yield return new WaitForSeconds(takeDamageCoolDownTime);
        takeDamageIsCoolDown = false;
    }

    IEnumerator SwordCoolDown()
    {
        isSwordCoolDown = true;
        yield return new WaitForSeconds(swordCoolDown);
        isSwordCoolDown = false;
    }


}
