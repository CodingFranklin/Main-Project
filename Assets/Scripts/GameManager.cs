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
    public float bulletDamage;
    public float bulletRange;
    public float enemyDamage;
    public bool isGameOver;
    public float totalTime = 300f;
    public float ExpAmount ;
    public float currentExp;
    public float maxExp;
    public float level;
    public bool isLevelUp;
    public float gunCoolDownTime;
    public float ammoCapacity;
    public float reloadTime;



    [SerializeField] UnityEngine.UI.Image heart1;
    [SerializeField] UnityEngine.UI.Image heart2;
    [SerializeField] UnityEngine.UI.Image heart3;
    [SerializeField] UnityEngine.UI.Image heart4;
    [SerializeField] UnityEngine.UI.Image heart5;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI timerText;
    


    private void Awake() 
    {
        instance = this;
        currentPlayerHealth = maxPlayerHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    

    

    


}
