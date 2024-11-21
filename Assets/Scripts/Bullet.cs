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
    float maxPenetrations;

    
    

    // Start is called before the first frame update
    void Start()
    {
        range = GameManager.instance.bulletRange;
        speed = GameManager.instance.bulletSpeed;
        maxPenetrations = GameManager.instance.maxPenetrations;
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        

        float distanceTraveled = Vector3.Distance(initialPosition, transform.position);
        if (distanceTraveled > range)
        {
            Destroy(gameObject);
        }
    }

    public void Instantiate(Vector3 dir)
    {
        range = GameManager.instance.bulletRange;
        speed = GameManager.instance.bulletSpeed;
        direction = dir;

        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Devil" || other.gameObject.tag == "Slime" ||
        other.gameObject.tag == "Goblin" || other.gameObject.tag == "Giant Goblin" ||
        other.gameObject.tag == "Skeleton" || other.gameObject.tag == "Pumpkin")
        {
            other.GetComponent<Enemy>().TakeDamage(GameManager.instance.bulletDamage, "Bullet");

            if (maxPenetrations > 0)
            {
                maxPenetrations--;
                return;
            }
            Destroy (gameObject);
        }
    }

}
