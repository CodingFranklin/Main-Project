using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Pistol : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    Vector3 mousePosition;
    Vector3 worldMousePositioin;
    Vector3 mouseDirection;
    public Vector3 shootingDirection;
    float currentAmmo;
    bool isReloading;
    float reloadTime;
    float canFire = -1f;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isReloading = false;
        currentAmmo = GameManager.instance.ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteDirectionChecker();
        reloadTime = GameManager.instance.reloadTime;

        //Calculating the mouse direction for the gun to follow
        mousePosition = Input.mousePosition;
        worldMousePositioin = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = worldMousePositioin - transform.position;
        mouseDirection.z = 0f;
        RotateGunToMouse();

        
        

        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            shootingDirection = mouseDirection;

            //make sure it won't reload many times
            if (isReloading)
            {
                return;
            }


            Shoot();
            if (currentAmmo == 0)
            {
                Reload();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


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
        if (!isReloading && currentAmmo > 0 && Time.time > canFire)
        {
            canFire = Time.time + GameManager.instance.gunFireRate;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            currentAmmo--;
        }
        
    }

    void Reload()
    {
        StartCoroutine(Reloading());
    }

    void RotateGunToMouse()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = GameManager.instance.ammoCapacity;

        isReloading = false;
    }

    void AmmoChecker()
    {
        if (!isReloading)
        {
            ammoText.text = "" + currentAmmo;
        }
        else if (isReloading)
        {
            ammoText.text = "Reloading...";
        }
        
    }
}
