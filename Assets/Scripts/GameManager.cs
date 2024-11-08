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

    public float currentPlayerHealth;
    public float maxPlayerHealth;
    public float maxEnemyHealth;
    public float slimeHealth;
    public float devilHealth;
    public float bulletDamage;
    public float bulletRange;
    public float bulletSpeed;
    public float gunFireRate;
    public float enemyDamage;
    public bool isGameOver;
    public float totalTime = 300f;
    public float ExpAmount ;
    public float currentExp;
    public float maxExp;
    public float level;
    public bool isLevelUp;
    public float ammoCapacity;
    public float reloadTime;
    public float ExpPickUpRange;
    public float spawnRate;



    List<String> upgradeOptions = new List<string>();




    [SerializeField] UnityEngine.UI.Image heart1;
    [SerializeField] UnityEngine.UI.Image heart2;
    [SerializeField] UnityEngine.UI.Image heart3;
    [SerializeField] UnityEngine.UI.Image heart4;
    [SerializeField] UnityEngine.UI.Image heart5;
    [SerializeField] GameObject gameOverText;
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


        if (currentPlayerHealth == 0)
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            GameOver();
        }

        HealthChecker();
        

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
        gameOverText.SetActive(true);
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        timerText.text = minutes + ":" + seconds;
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
    }
    




    

    


}
