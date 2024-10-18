using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GunController gunController;
    Vector3 direction;

    [SerializeField] float speed;
    [SerializeField] float damage;

    

    // Start is called before the first frame update
    void Start()
    {
        gunController = GameObject.FindGameObjectWithTag("Gun_1").GetComponent<GunController>();
        direction = gunController.shootingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            
        }
    }
}
