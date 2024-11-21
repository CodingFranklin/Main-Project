using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Transform player;
    protected Vector3 direction;
    protected float currentEnemyHealth;
    protected Rigidbody2D rb;
    protected float knockbackDuration;
    public bool isKnockBack;

    public GameObject XP; // XP prefab for dropping XP on death
    public GameObject damageText; // Damage text prefab for showing damage numbers

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        knockbackDuration = GameManager.instance.knockBackDuation;

        // Find the player by tag if not set
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }



    // Abstract method for movement, to be implemented by subclasses
    protected virtual void Move(float moveSpeed)
    {
        if (player != null && !isKnockBack)
        {
            direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    // Method to deal damage to the enemy, with a virtual method for additional behaviors
    public abstract void TakeDamage(float damage, string type);

    // Method to drop XP when the enemy dies
    protected virtual void DropXP()
    {
        Instantiate(XP, transform.position, Quaternion.identity);
    }

    // Method to show damage text
    protected virtual void ShowDamageText(string type)
    {
        Debug.Log(type);
        GameObject textInstance = Instantiate(damageText, transform.position, Quaternion.identity);

        DamageText dt = textInstance.GetComponent<DamageText>();
        dt.damageType = type;
        Debug.Log(type);

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
            TakeDamage(GameManager.instance.bulletDamage, "Bullet");
        }
    }

    public IEnumerator ApplyKnockback(Vector2 knockbackDirection)
    {
        isKnockBack = true;

        PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        rb.AddForce(pc.knockBackForce * knockbackDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockBack = false;
    }
}
