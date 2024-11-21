using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffects : MonoBehaviour
{
    public float duation = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
