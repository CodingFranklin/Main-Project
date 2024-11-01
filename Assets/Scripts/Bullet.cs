using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Pistol pistol;
    Vector3 direction;
    float range;
    Vector3 initialPosition;

    [SerializeField] float speed;

    
    

    // Start is called before the first frame update
    void Start()
    {
        range = GameManager.instance.bulletRange;
        pistol = GameObject.FindGameObjectWithTag("Gun_1").GetComponent<Pistol>();
        direction = pistol.shootingDirection;

        initialPosition = transform.position;
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


}
