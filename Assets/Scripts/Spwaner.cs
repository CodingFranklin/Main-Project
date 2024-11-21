using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spwaner : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] GameObject slime;
    [SerializeField] GameObject devil;
    [SerializeField] GameObject goblin;
    [SerializeField] GameObject giantGoblin;
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject pumpkin;

    [SerializeField] Transform player;

    List<GameObject> enemies = new List<GameObject>();

    float canSpawn = -1f;
    float xmax;
    float xmin;
    float ymax;
    float ymin;


    // Start is called before the first frame update
    void Start()
    {   
        enemies.Add(slime);
        enemies.Add(slime);
        enemies.Add(slime);
        enemies.Add(devil);
        enemies.Add(goblin);
        enemies.Add(goblin);
        enemies.Add(pumpkin);
        enemies.Add(pumpkin);
        enemies.Add(giantGoblin);
        enemies.Add(skeleton);
        

        xmax = player.position.x + offset.x;
        ymax = player.position.y + offset.y;
        xmin = player.position.x - offset.x;
        ymin = player.position.y - offset.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        xmax = player.position.x + offset.x;
        ymax = player.position.y + offset.y;
        xmin = player.position.x - offset.x;
        ymin = player.position.y - offset.y;

        if (Time.time > canSpawn)
        {
            canSpawn = Time.time + GameManager.instance.spawnRate;
            SpawnEnemy();
        }
    }


    void SpawnEnemy()
    {
        int r1 = UnityEngine.Random.Range(0,2);
        int r2 = UnityEngine.Random.Range(0,2);

        float randomX = UnityEngine.Random.Range(xmin,xmax);
        float randomY = UnityEngine.Random.Range(ymin,ymax);

        int randomEnemy = UnityEngine.Random.Range(0,enemies.Count);

        if (r1 == 1)
        {
            //generate enemies on horizontal line
            if (r2 == 1)
            {
                Instantiate(enemies[randomEnemy], new Vector3(randomX,ymax,0), Quaternion.identity);
            }
            else
            {
                Instantiate(enemies[randomEnemy], new Vector3(randomX,ymin,0), Quaternion.identity);
            }
            
        }
        else
        {
            //generate enemies on vertical line
            if (r2 == 1)
            {
                Instantiate(enemies[randomEnemy], new Vector3(xmax,randomY,0), Quaternion.identity);
            }
            else
            {
                Instantiate(enemies[randomEnemy], new Vector3(xmin,randomY,0), Quaternion.identity);
            }
        }
        
        }
}
