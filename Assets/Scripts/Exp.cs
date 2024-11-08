using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Transform player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= GameManager.instance.ExpPickUpRange)
        {
            MoveToplayer();
        }
    }

    void MoveToplayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.AddXP();
        }
    }
}
