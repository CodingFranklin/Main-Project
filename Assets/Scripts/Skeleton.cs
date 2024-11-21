using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] float moveSpeed;
    [SerializeField] AudioClip skeletonDieClip;
    SpriteRenderer spriteRenderer;
    float previousHorizontalVector;
    float currentHealth;

    
    
     
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = GameManager.instance.skeletonHealth;
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

    protected override void Die()
    {
        SoundEffectsManager.instance.PlaySoundEffectClip(skeletonDieClip, transform, 1f);
        base.Die();
    }

    protected override void Move(float moveSpeed)
    {
        base.Move(this.moveSpeed);
    }

    public override void TakeDamage(float damage, string type)
    {
        base.ShowDamageText(type);

        if (currentHealth <= damage)
        {
            currentHealth = 0;
            Die();
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
            TakeDamage(GameManager.instance.bulletDamage, "Bullet");
        }
        if (other.gameObject.CompareTag("Sword"))
        {
            base.ShowDamageText("Sword");
            TakeDamage(GameManager.instance.swordDamge, "Sword");
        }
        if (other.gameObject.CompareTag("Wave"))
        {
            base.ShowDamageText("Wave");
            TakeDamage(GameManager.instance.waveDamage, "Wave");
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
