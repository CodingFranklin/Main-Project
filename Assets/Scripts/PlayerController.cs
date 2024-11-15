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
    Rigidbody2D rb;
    bool takeDamageIsCoolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        takeDamageIsCoolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        Move();
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
            E.isKnockBack = true;
            Vector3 knockBackDirection = (enemy.transform.position - transform.position).normalized;

            StartCoroutine(E.ApplyKnockback(knockBackDirection));
        }
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy" && !takeDamageIsCoolDown)
        {
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


}
