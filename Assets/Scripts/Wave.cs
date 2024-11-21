using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 direction;
    float speed;
    float range;
    

    

    // Start is called before the first frame update
    void Start()
    {
        range = GameManager.instance.bulletRange;
        speed = GameManager.instance.bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        range = GameManager.instance.waveRange;
        speed = GameManager.instance.waveSpeed;

        transform.position += direction.normalized * speed * Time.deltaTime;
        

        float distanceTraveled = Vector3.Distance(initialPosition, transform.position);
        if (distanceTraveled > range)
        {
            Destroy(gameObject);
        }
    }

    public void Instantiate(Vector3 dir)
    {
        range = GameManager.instance.waveRange;
        speed = GameManager.instance.bulletSpeed;
        direction = dir;

        initialPosition = transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Devil" || other.gameObject.tag == "Slime" ||
        other.gameObject.tag == "Goblin" || other.gameObject.tag == "Giant Goblin" ||
        other.gameObject.tag == "Skeleton" || other.gameObject.tag == "Pumpkin")
        {
            other.GetComponent<Enemy>().TakeDamage(GameManager.instance.waveDamage, "Wave");
        }
    }
}
