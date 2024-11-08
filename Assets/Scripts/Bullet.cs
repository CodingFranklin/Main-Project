using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Pistol pistol;
    Vector3 direction;

    float range;
    Vector3 initialPosition;

    float speed;

    
    

    // Start is called before the first frame update
    void Start()
    {
        range = GameManager.instance.bulletRange;
        speed = GameManager.instance.bulletSpeed;
        pistol = GameObject.FindGameObjectWithTag("Gun_1").GetComponent<Pistol>();
        direction = pistol.shootingDirection;

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        range = GameManager.instance.bulletRange;
        speed = GameManager.instance.bulletSpeed;


        transform.position += direction.normalized * speed * Time.deltaTime;
        

        float distanceTraveled = Vector3.Distance(initialPosition, transform.position);
        if (distanceTraveled > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(GameManager.instance.bulletDamage);
        }
    }

}
