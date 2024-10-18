using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;
    Vector3 mousePosition;
    Vector3 worldMousePositioin;
    Vector3 mouseDirection;
    public Vector3 shootingDirection;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteDirectionChecker();

        

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            shootingDirection = mouseDirection;
        }

        //Calculating the mouse direction for the gun to follow
        mousePosition = Input.mousePosition;
        worldMousePositioin = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = worldMousePositioin - transform.position;
        mouseDirection.z = 0f;
        RotateGunToMouse();
    }

    void SpriteDirectionChecker()
    {
        if (worldMousePositioin.x < player.position.x)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

     void RotateGunToMouse()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
