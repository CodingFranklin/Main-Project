using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public float spawnRate = 0.5f;
    [SerializeField] GameObject slime;
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(-1.5f, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(2.5f, 0, 0)).x;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 2.5f, 0)).y;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, -1.5f, 0)).y;
        
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnEnemy()
    {
        float randonNumX = Random.Range(xMin, xMax);
        float randonNumY = Random.Range(yMin, yMax);

        Instantiate(slime, new Vector3(randonNumX, randonNumY, 0), Quaternion.identity);
    }
}
