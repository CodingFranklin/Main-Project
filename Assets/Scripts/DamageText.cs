using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamageText : MonoBehaviour
{
    public float duation = 1f;
    public String damageType;
    private TextMesh textMesh;    
    private String damage;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();

        if (damageType.Equals("Bullet"))
        {
            damage = GameManager.instance.bulletDamage.ToString();
        }
        else if (damageType.Equals("Sword"))
        {
            damage = GameManager.instance.swordDamge.ToString();
        }
        else if (damageType.Equals("Wave"))
        {
            damage = GameManager.instance.waveDamage.ToString();
        }
        

        Destroy(gameObject, duation);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = damage;
    }
}
