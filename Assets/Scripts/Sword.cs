using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    Vector3 mousePosition;
    Vector3 worldMousePositioin;
    Vector3 mouseDirection;

    float swordDamge;

    [SerializeField] GameObject meleeWave;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        swordDamge = GameManager.instance.swordDamge;


        //Calculating the mouse direction for the gun to follow
        mousePosition = Input.mousePosition;
        worldMousePositioin = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = worldMousePositioin - transform.position;
        mouseDirection.z = 0f;
        RotateSwordToMouse();

        
    }

    void Attack()
    {
        Vector3 dir = mouseDirection.normalized;

        GameObject wave = Instantiate(meleeWave, transform.position, transform.rotation);
        wave.gameObject.GetComponent<Wave>().Instantiate(dir);
    }


    void RotateSwordToMouse()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Devil" || other.gameObject.tag == "Slime" ||
        other.gameObject.tag == "Goblin" || other.gameObject.tag == "Giant Goblin" ||
        other.gameObject.tag == "Skeleton" || other.gameObject.tag == "Pumpkin")
        {
            other.GetComponent<Enemy>().TakeDamage(swordDamge, "Sword");
        }
    }
}
