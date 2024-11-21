using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //Player 
    public float currentPlayerHealth;
    public float maxPlayerHealth;
    public float knockBackDuation;
    public float playerMoveSpeed;

    //Enemy
    public float slimeHealth;
    public float devilHealth;
    public float goblinHealth;
    public float skeletonHealth;
    public float pumpkinHealth;
    public float giantGoblinHealth;
    public float enemyDamage;
    

    //Weapon
    public float bulletDamage;
    public float bulletRange;
    public float bulletSpeed;
    public float gunFireRate;
    public float ammoCapacity;
    public float reloadTime;
    public float shootLevel;
    public float maxPenetrations;
    public float swordDamge;
    public float waveDamage;
    public float waveRange;
    public float waveSpeed;
    public float swordCoolDown;

    
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
    [SerializeField] GameObject pausePage;
    [SerializeField] Button keepPlaying;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] RectTransform mouseIcon;
    [SerializeField] Light2D playerLight;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI upgradeOption1Text;
    [SerializeField] TextMeshProUGUI upgradeOption2Text;
    [SerializeField] TextMeshProUGUI upgradeOption3Text;
    [SerializeField] TextMeshProUGUI option1Level;
    [SerializeField] TextMeshProUGUI option2Level;
    [SerializeField] TextMeshProUGUI option3Level;
    [SerializeField] GameObject upgradePage;
    [SerializeField] AudioClip upgradeClip;
    [SerializeField] AudioClip victoryClip;
    [SerializeField] AudioClip gameOverClip;

    private Dictionary<string, int> optionLevels = new Dictionary<string, int>();



    private void Awake() 
    {
        instance = this;
        currentPlayerHealth = maxPlayerHealth;
        upgradePage.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        Cursor.visible = false;
        isLevelUp = false;


        upgradeOptions.Add("Bullet Damage + 30%");
        upgradeOptions.Add("Pick-up Range + 15%");
        upgradeOptions.Add("Bullet Range + 20%");
        upgradeOptions.Add("Shooting Speed + 20%");
        upgradeOptions.Add("Ammo Capacity + 30%");
        upgradeOptions.Add("Reload Time - 20%");
        upgradeOptions.Add("Bullet Speed + 20%");
        upgradeOptions.Add("Bullet Level + 1");
        upgradeOptions.Add("Bullet Penetration + 1");
        upgradeOptions.Add("Move Speed + 10%");
        upgradeOptions.Add("Visible Range + 1");
        upgradeOptions.Add("Sword Damage + 30%");
        upgradeOptions.Add("Wave Damage + 20%");
        upgradeOptions.Add("Sword Cooldown - 10%");

        foreach (String option in upgradeOptions)
        {
            optionLevels[option] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        //Pause the game by pressing Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Mathf.Approximately(Time.timeScale, 0))
            {
                ResumeGameFromPausePage();
            }
            else
            {
                PauseGameWithKey();
            }
            return;
        }

        //make sure the pause also works for Update
        if (Mathf.Approximately(Time.timeScale, 0)) 
        {
            return;
        }

        
        if (!isLevelUp)
        {
            ResumeGame();
            upgradePage.SetActive(false);
        }

        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            UpdateTimerText();
        }

       

        if (totalTime < 0 || currentPlayerHealth <= 0)
        {
            Debug.Log(isGameOver);
            totalTime = -1;
            isGameOver = true;
        }

        if (isGameOver)
        {
            GameOver();
            return;
        }

        if (optionLevels["Bullet Level + 1"] == 2)
        {
            upgradeOptions.Remove("Bullet Level + 1");
        }
        
        if (optionLevels["Bullet Penetration + 1"] == 3)
        {
            upgradeOptions.Remove("Bullet Penetration + 1");
        }

        

        HealthChecker();
        DifficultyHandler();
        

        

        

        Vector2 mousePosition = Input.mousePosition;
        mouseIcon.position = mousePosition;

        if (isLevelUp)
        {
            SoundEffectsManager.instance.PlaySoundEffectClip(upgradeClip, transform, 1f);
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

    public void PauseGameWithKey()
    {
        pausePage.SetActive(true);
        PauseGame();
    }

    public void ResumeGameFromPausePage()
    {
        pausePage.SetActive(false);
        ResumeGame();
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        PauseGame();
        Cursor.visible = true;
        gameOverPage.SetActive(true);

        if (currentPlayerHealth > 0)
        {
            gameOverText.text = "You Survived!";
            SoundEffectsManager.instance.PlaySoundEffectClip(victoryClip, transform, 1f);
        }
        else
        {
            SoundEffectsManager.instance.PlaySoundEffectClip(gameOverClip, transform, 1f);
            gameOverText.text = "Oops You Died!";
            keepPlaying.gameObject.SetActive(false);
        }
        
    }

    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void KeepPlaying()
    {
        totalTime = 0;
        isGameOver = false;
        gameOverPage.SetActive(false);
        ResumeGame();
        Cursor.visible = false;
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

        if (seconds == 0 && minutes == 0)
        {
            timerText.text = "00:00";
        }

        if (totalTime < 0)
        {
            timerText.text = "--:--";
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

    public void SkipUpgrade()
    {
        isLevelUp = false;
    }

    public void RandomUpgradeOptions()
    {
        List<String> remainingOptions = new List<string>(upgradeOptions);

        //option 1
        int a = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption1Text.text = remainingOptions[a];
        option1Level.text = "LV " + optionLevels[remainingOptions[a]] + " --> LV " + (optionLevels[remainingOptions[a]] + 1);
        remainingOptions.RemoveAt(a);

        //option 2
        int b = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption2Text.text = remainingOptions[b];
        option2Level.text = "LV " + optionLevels[remainingOptions[b]] + " --> LV " + (optionLevels[remainingOptions[b]] + 1);
        remainingOptions.RemoveAt(b);

        //option 3
        int c = UnityEngine.Random.Range(0,remainingOptions.Count);
        upgradeOption3Text.text = remainingOptions[c];
        option3Level.text = "LV " + optionLevels[remainingOptions[c]] + " --> LV " + (optionLevels[remainingOptions[c]] + 1);
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
        if (optionLevels.ContainsKey(choice))
        {
            optionLevels[choice]++;
        }


        if (choice.Equals("Bullet Damage + 30%"))
        {
            bulletDamage += Mathf.FloorToInt(bulletDamage * 0.3f);
        }
        else if (choice.Equals("Pick-up Range + 15%"))
        {
            ExpPickUpRange += ExpPickUpRange * 0.15f;
        }
        else if (choice.Equals("Bullet Range + 20%"))
        {
            bulletRange += bulletRange * 0.2f;
        }
        else if (choice.Equals("Shooting Speed + 20%"))
        {
            gunFireRate -= gunFireRate * 0.2f;
        }
        else if (choice.Equals("Ammo Capacity + 30%"))
        {
            ammoCapacity += Mathf.FloorToInt(ammoCapacity * 0.3f);
        }
        else if (choice.Equals("Reload Time - 20%"))
        {
            reloadTime -= reloadTime * 0.2f;
        }
        else if (choice.Equals("Bullet Speed + 20%"))
        {
            bulletSpeed += bulletSpeed * 0.2f;
        }
        else if (choice.Equals("Bullet Level + 1"))
        {
            shootLevel++;
        }
        else if (choice.Equals("Bullet Penetration + 1"))
        {
            maxPenetrations++;
        }
        else if (choice.Equals("Move Speed + 10%"))
        {
            playerMoveSpeed += playerMoveSpeed * 0.1f;
        }
        else if (choice.Equals("Visible Range + 1"))
        {
            playerLight.pointLightInnerRadius += 1;
            playerLight.pointLightOuterRadius += 1;
        }
        else if (choice.Equals("Sword Damage + 30%"))
        {
            swordDamge += Mathf.FloorToInt(swordDamge * 0.3f);
        }
        else if (choice.Equals("Wave Damage + 20%"))
        {
            waveDamage += Mathf.FloorToInt(waveDamage * 0.2f);
        }
        else if (choice.Equals("Sword Cooldown - 10%"))
        {
            swordCoolDown -= swordCoolDown * 0.1f;
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
            goblinHealth = 15;
            skeletonHealth = 10;
            pumpkinHealth = 10;
            giantGoblinHealth = 30;
        }
        //second minute
        else if (totalTime <= 240 && totalTime > 180)
        {
            maxExp = 15;

            spawnRate = 0.5f;

            slimeHealth = 15;
            devilHealth = 25;
            goblinHealth = 20;
            skeletonHealth = 15;
            pumpkinHealth = 15;
            giantGoblinHealth = 40;
        }
        //third minute
        else if (totalTime <= 180 && totalTime > 120)
        {
            maxExp = 30;

            spawnRate = 0.2f;

            slimeHealth = 20;
            devilHealth = 30;
            goblinHealth = 25;
            skeletonHealth = 20;
            pumpkinHealth = 20;
            giantGoblinHealth = 50;
        }
        //fourth minute
        else if (totalTime <= 120 && totalTime > 60)
        {
            maxExp = 50;

            spawnRate = 0.1f;

            slimeHealth = 35;
            devilHealth = 50;
            goblinHealth = 45;
            skeletonHealth = 30;
            pumpkinHealth = 25;
            giantGoblinHealth = 70;
        }
        //last minute
        else if (totalTime <= 60 && totalTime > 30)
        {
            maxExp = 150;

            spawnRate = 0.05f;

            slimeHealth = 45;
            devilHealth = 80;
            goblinHealth = 55;
            skeletonHealth = 40;
            pumpkinHealth = 30;
            giantGoblinHealth = 100;
        }
        //last 30 seconds
        else if (totalTime <= 30 && totalTime != 0)
        {
            maxExp = 250;

            spawnRate = 0.01f;

            slimeHealth = 60;
            devilHealth = 100;
            goblinHealth = 70;
            skeletonHealth = 50;
            pumpkinHealth = 40;
            giantGoblinHealth = 150;
        }
        //After clicking Keep Playing (infinite mode)
        else if (totalTime == 0 && isLevelUp)
        {
            maxExp += 50;

            spawnRate *= 0.7f;

            slimeHealth += 30;
            devilHealth += 50;
            goblinHealth += 40;
            skeletonHealth += 20;
            pumpkinHealth += 20;
            giantGoblinHealth += 70;
        }
        
    }
    




    

    


}
