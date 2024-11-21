using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] float moveSpeed;
    [SerializeField] AudioClip slimeDieClip;
    float currentHealth;

    
    
     
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentHealth = GameManager.instance.slimeHealth;
    }

    private void Update() 
    {
        Move(moveSpeed);
    }

    protected override void Move(float moveSpeed)
    {
        base.Move(this.moveSpeed);
    }

    protected override void Die()
    {
        SoundEffectsManager.instance.PlaySoundEffectClip(slimeDieClip, transform, 1f);
        base.Die();
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
}
