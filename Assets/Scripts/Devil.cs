using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : Enemy
{
    [SerializeField] float moveSpeed;
    SpriteRenderer spriteRenderer;
    float previousHorizontalVector;
    float currentHealth;

    
    
     
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = GameManager.instance.devilHealth;
    }

    private void Update() 
    {
        Move(moveSpeed);

        if (direction.x != 0)
        {
            previousHorizontalVector = direction.x;
        }
        SpriteDirectionChecker();
    }

    protected override void Move(float moveSpeed)
    {
        base.Move(this.moveSpeed);
    }

    public override void TakeDamage(float damage)
    {
        base.ShowDamageText();

        if (currentHealth <= damage)
        {
            currentHealth = 0;
            base.Die();
        }
        else
        {
            currentHealth -= damage;
        }
        //maybe some animations here
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(GameManager.instance.bulletDamage);
            Destroy(other.gameObject);
        }
    }

    void SpriteDirectionChecker()
    {
        if (previousHorizontalVector < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
