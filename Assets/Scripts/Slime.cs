using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject XP;
    
    Transform player;
    float currentEnemyHealth;
    Rigidbody2D rb;
    
    
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentEnemyHealth = GameManager.instance.maxEnemyHealth;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        if (player != null)
        {
            Vector3 target = Vector3.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(target);
        }
    }

    void DealDamageToEnemy(float damage)
    {
        if (currentEnemyHealth < damage)
        {
            currentEnemyHealth = 0;
            Destroy(gameObject);
            DropXP();
        }
        else
        {
            currentEnemyHealth -= damage;
        }
    }

    void DropXP()
    {
        Instantiate(XP, transform.position, Quaternion.identity);
    }

    

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Bullet")
        {
            DealDamageToEnemy(GameManager.instance.bulletDamage);
            Destroy(other.gameObject);
        }
    }
}
