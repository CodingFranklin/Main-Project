using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //Player 
    public float currentPlayerHealth;
    public float maxPlayerHealth;
    public float knockBackDuation;

    //Enemy
    public float slimeHealth;
    public float devilHealth;
    public float enemyDamage;

    //Weapon
    public float bulletDamage;
    public float bulletRange;
    public float bulletSpeed;
    public float gunFireRate;
    public float ammoCapacity;
    public float reloadTime;
    public float shootLevel;
    
    //Exp
    public float ExpAmount ;
    public float currentExp;
    public float maxExp;
    public float level;
    public float ExpPickUpRange;

    //Game Management
    public float spawnRate;
    public float totalTime = 300f;
    public bool isLevelUp;
    public bool isGameOver;
    
    List<String> upgradeOptions = new List<string>();


    [SerializeField] UnityEngine.UI.Image heart1;
    [SerializeField] UnityEngine.UI.Image heart2;
    [SerializeField] UnityEngine.UI.Image heart3;
    [SerializeField] UnityEngine.UI.Image heart4;
    [SerializeField] UnityEngine.UI.Image heart5;
    [SerializeField] GameObject gameOverPage;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] RectTransform mouseIcon;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI upgradeOption1Text;
    [SerializeField] TextMeshProUGUI upgradeOption2Text;
    [SerializeField] TextMeshProUGUI upgradeOption3Text;
    [SerializeField] GameObject upgradePage;

    
    


    private void Awake() 
    {
        instance = this;
        currentPlayerHealth = maxPlayerHealth;
        upgradePage.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Cursor.visible = false;
        isLevelUp = false;


        upgradeOptions.Add("Bullet Damage + 20%");
        upgradeOptions.Add("Pick-up Range + 20%");
        upgradeOptions.Add("Bullet Range + 20%");
        upgradeOptions.Add("Shooting Speed + 30%");
        upgradeOptions.Add("Ammo Capacity + 30%");
        upgradeOptions.Add("Reload Time - 20%");
        upgradeOptions.Add("Bullet Speed + 50%");
        upgradeOptions.Add("Bullet + 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelUp)
        {
            ResumeGame();
            upgradePage.SetActive(false);
        }

        //make sure the pause also works for Update
        if (Mathf.Approximately(Time.timeScale, 0)) 
        {
            return;
        }

        if (totalTime <= 0 || currentPlayerHealth <= 0)
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            GameOver();
            return;
        }

        

        HealthChecker();
        DifficultyHandler();
        

        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            UpdateTimerText();

            if (totalTime <= 0)
            {
                totalTime = 0;
            }

        }

        Vector2 mousePosition = Input.mousePosition;
        mouseIcon.position = mousePosition;

        if (isLevelUp)
        {
            upgradePage.SetActive(true);
            PauseGame();
            RandomUpgradeOptions();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void DealDamageToPlayer(float damage)
    {
        if (currentPlayerHealth < damage)
        {
            currentPlayerHealth = 0;
        }
        else
        {
            currentPlayerHealth -= damage;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if (currentPlayerHealth > 0)
        {
            gameOverText.text = "Victory!";
        }
        gameOverPage.SetActive(true);
        PauseGame();
        Cursor.visible = true;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        if (seconds < 10)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
    }

    void HealthChecker()
    {
        if (currentPlayerHealth == 9)
        {
            heart5.fillAmount = 0.5f;
        }
        else if (currentPlayerHealth == 8)
        {
            heart5.fillAmount = 0f;
        }
        else if (currentPlayerHealth == 7)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0.5f;
        }
        else if (currentPlayerHealth == 6)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
        }
        else if (currentPlayerHealth == 5)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0.5f;
        }
        else if (currentPlayerHealth == 4)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0f;
        }
        else if (currentPlayerHealth == 3)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0f;
            heart2.fillAmount = 0.5f;
        }
        else if (currentPlayerHealth == 2)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0f;
            heart2.fillAmount = 0f;
        }
        else if (currentPlayerHealth == 1)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0f;
            heart2.fillAmount = 0f;
            heart1.fillAmount = 0.5f;
        }
        else if (currentPlayerHealth == 0)
        {
            heart5.fillAmount = 0f;
            heart4.fillAmount = 0f;
            heart3.fillAmount = 0f;
            heart2.fillAmount = 0f;
            heart1.fillAmount = 0f;
        }
    }

    public void AddXP()
    {
        if (currentExp + ExpAmount < maxExp)
        {
            currentExp += ExpAmount;
        }
        else
        {
            currentExp = 0;
            level++;
            levelText.text = "Level " + level;
        }
    }

    public void RandomUpgradeOptions()
    {
        List<String> remainingOptions = new List<string>(upgradeOptions);
        int a = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption1Text.text = remainingOptions[a];
        remainingOptions.RemoveAt(a);

        int b = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption2Text.text = remainingOptions[b];
        remainingOptions.RemoveAt(b);

        int c = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption3Text.text = remainingOptions[c];
        remainingOptions.RemoveAt(c);
    }

    public void SelectOption1()
    {
        CheckUpgrade(upgradeOption1Text.text);
        isLevelUp = false;
    }

    public void SelectOption2()
    {
        CheckUpgrade(upgradeOption2Text.text);
        isLevelUp = false;
    }

    public void SelectOption3()
    {
        CheckUpgrade(upgradeOption3Text.text);
        isLevelUp = false;
    }


    public void CheckUpgrade(String choice)
    {
        if (choice.Equals("Bullet Damage + 20%"))
        {
            bulletDamage += Mathf.FloorToInt(bulletDamage * 0.2f);
        }
        else if (choice.Equals("Pick-up Range + 20%"))
        {
            ExpPickUpRange += ExpPickUpRange * 0.2f;
        }
        else if (choice.Equals("Bullet Range + 20%"))
        {
            bulletRange += bulletRange * 0.2f;
        }
        else if (choice.Equals("Shooting Speed + 30%"))
        {
            gunFireRate -= gunFireRate * 0.3f;
        }
        else if (choice.Equals("Ammo Capacity + 30%"))
        {
            ammoCapacity += Mathf.FloorToInt(ammoCapacity * 0.3f);
        }
        else if (choice.Equals("Reload Time - 20%"))
        {
            reloadTime -= reloadTime * 0.2f;
        }
        else if (choice.Equals("Bullet Speed + 50%"))
        {
            bulletSpeed += bulletSpeed * 0.5f;
        }
        else if (choice.Equals("Bullet + 1"))
        {
            shootLevel++;
        }
    }

    void DifficultyHandler()
    {
        //first minute
        if (totalTime <= 300 && totalTime > 240)
        {
            maxExp = 10;

            spawnRate = 1f;

            slimeHealth = 10;
            devilHealth = 20;
        }
        //second minute
        else if (totalTime <= 240 && totalTime > 180)
        {
            maxExp = 15;

            spawnRate = 0.5f;

            slimeHealth = 15;
            devilHealth = 25;
        }
        //third minute
        else if (totalTime <= 180 && totalTime > 120)
        {
            maxExp = 30;

            spawnRate = 0.2f;

            slimeHealth = 20;
            devilHealth = 30;
        }
        //fourth minute
        else if (totalTime <= 120 && totalTime > 60)
        {
            maxExp = 50;

            spawnRate = 0.1f;

            slimeHealth = 35;
            devilHealth = 50;
        }
        //last minute
        else if (totalTime <= 60)
        {
            maxExp = 150;

            spawnRate = 0.05f;

            slimeHealth = 45;
            devilHealth = 80;
        }
        
    }
    




    

    


}
