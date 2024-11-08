using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Transform player;
    protected Vector3 direction;
    protected float currentEnemyHealth;
    protected Rigidbody2D rb;

    public GameObject XP; // XP prefab for dropping XP on death
    public GameObject damageText; // Damage text prefab for showing damage numbers

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player by tag if not set
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }



    // Abstract method for movement, to be implemented by subclasses
    protected virtual void Move(float moveSpeed)
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    // Method to deal damage to the enemy, with a virtual method for additional behaviors
    public abstract void TakeDamage(float damage);

    // Method to drop XP when the enemy dies
    protected virtual void DropXP()
    {
        Instantiate(XP, transform.position, Quaternion.identity);
    }

    // Method to show damage text
    protected virtual void ShowDamageText()
    {
        GameObject textInstance = Instantiate(damageText, transform.position, Quaternion.identity);
        textInstance.transform.SetParent(null); 
    }

    // Method called when the enemy dies
    protected virtual void Die()
    {
        DropXP();
        Destroy(gameObject);
    }

    // Handles collision detection for bullets or other damage sources
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(GameManager.instance.bulletDamage);
            Destroy(other.gameObject);
        }
    }
}
