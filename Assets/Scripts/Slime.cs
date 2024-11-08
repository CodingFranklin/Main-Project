using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] float moveSpeed;
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
}
