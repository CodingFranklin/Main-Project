using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamageText : MonoBehaviour
{
    public float duation = 1f;
    private TextMesh textMesh;    
    private String damage;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();

        damage = GameManager.instance.bulletDamage.ToString();

        Destroy(gameObject, duation);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = damage;
    }
}
