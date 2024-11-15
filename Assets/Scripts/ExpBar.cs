using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    RectTransform rt;
    float currentExp;
    float maxExp;
    float ExpPercentage;
    float maxWidth;
    float maxHeight;
    float moveDistance;
    float previousExp;
    float previousLevel;
    float currentLevel;
    float ExpAmount;
    Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    
        maxExp = GameManager.instance.maxExp;
        previousExp = GameManager.instance.currentExp;
        previousLevel = GameManager.instance.level;
        ExpAmount = GameManager.instance.ExpAmount;
        maxWidth = 750f;
        maxHeight = rt.sizeDelta.y;
        initialPosition = rt.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        maxExp = GameManager.instance.maxExp;
        currentExp = GameManager.instance.currentExp;
        currentLevel = GameManager.instance.level;

        ExpPercentage = currentExp / maxExp;
        // moveDistance = maxWidth * (ExpAmount / maxExp) / 2;

        if(GameManager.instance.isLevelUp || Mathf.Approximately(Time.timeScale, 0))
        {
            return;
        }

        if (currentExp > previousExp && currentLevel == previousLevel)
        {
            rt.sizeDelta = new Vector2(maxWidth * ExpPercentage, maxHeight);
            

            previousExp = currentExp;
        }

        
        
        else if(currentLevel > previousLevel)
        {
            rt.sizeDelta = new Vector2(maxWidth * ExpPercentage, maxHeight);
            
            Debug.Log("Level Changing!!");

            previousExp = currentExp;
            previousLevel = currentLevel;

            GameManager.instance.isLevelUp = true;
        }

    }
}
