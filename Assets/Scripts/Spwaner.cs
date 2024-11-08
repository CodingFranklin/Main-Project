using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spwaner : MonoBehaviour
{
    [SerializeField] GameObject slime;
    [SerializeField] GameObject devil;


    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] GameObject topLeft;
    [SerializeField] GameObject bottomLeft;
    [SerializeField] GameObject topRight;
    [SerializeField] GameObject bottomRight;


    List<GameObject> spawnPoints = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {   
        
        spawnPoints.Add(top);
        spawnPoints.Add(bottom);
        spawnPoints.Add(left);
        spawnPoints.Add(right);
        spawnPoints.Add(topLeft);
        spawnPoints.Add(topRight);
        spawnPoints.Add(bottomLeft);
        spawnPoints.Add(bottomRight);

        enemies.Add(slime);
        enemies.Add(devil);

        
        InvokeRepeating("SpawnEnemy", 0, GameManager.instance.spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnEnemy()
    {
        int randomSP = UnityEngine.Random.Range(0,spawnPoints.Count);
        int randomEnemy = UnityEngine.Random.Range(0,enemies.Count);

        Instantiate(enemies[randomEnemy], spawnPoints[randomSP].transform.position, Quaternion.identity);
    }
}
