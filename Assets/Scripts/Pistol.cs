using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    Vector3 mousePosition;
    Vector3 worldMousePositioin;
    Vector3 mouseDirection;
    public Vector3 shootingDirection;
    bool isCoolDown;
    float coolDownTime;
    float ammoCapacity;
    float currentAmmo;
    bool isReloading;
    float reloadTime;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isCoolDown = false;
        coolDownTime = GameManager.instance.gunCoolDownTime;
        ammoCapacity = GameManager.instance.ammoCapacity;
        currentAmmo = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteDirectionChecker();
        reloadTime = GameManager.instance.reloadTime;
        

        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            Shoot();
            shootingDirection = mouseDirection;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        //Calculating the mouse direction for the gun to follow
        mousePosition = Input.mousePosition;
        worldMousePositioin = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = worldMousePositioin - transform.position;
        mouseDirection.z = 0f;
        RotateGunToMouse();


        AmmoChecker();
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
        if (!isCoolDown && !isReloading && currentAmmo > 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            currentAmmo--;
            StartCoroutine(ShootCoolDown());
        }
        
    }

    void Reload()
    {
        StartCoroutine(Reloading());
        currentAmmo = ammoCapacity;
    }

    void RotateGunToMouse()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    IEnumerator ShootCoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(coolDownTime);
        isCoolDown = false;
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    void AmmoChecker()
    {
        ammoText.text = "" + currentAmmo;
    }
}
