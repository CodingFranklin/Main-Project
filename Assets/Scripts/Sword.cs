using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    float swordDamge;

    [SerializeField] GameObject meleeWave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        swordDamge = GameManager.instance.swordDamge;
    }

    public void Instantiate(Vector3 dir)
    {
        Destroy(gameObject, 0.3f);

        RotateSwordToMouse(dir);

        Attack(dir);

    }

    void Attack(Vector3 dir)
    {
        Vector3 start = Quaternion.AngleAxis(60, Vector3.forward) * dir;
        Vector3 end = Quaternion.AngleAxis(-60, Vector3.forward) * dir;

        // Convert the start and end directions to Quaternions
        Quaternion startRotation = Quaternion.LookRotation(Vector3.forward, start);
        Quaternion endRotation = Quaternion.LookRotation(Vector3.forward, end);

        // Start the coroutine for smooth rotation
        StartCoroutine(RotateOverTime(startRotation, endRotation, 0.2f));

        GameObject wave = Instantiate(meleeWave, transform.position, transform.rotation);
        wave.gameObject.GetComponent<Wave>().Instantiate(dir);
    }

    IEnumerator RotateOverTime(Quaternion start, Quaternion end, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            // Interpolate between the start and end rotations
            transform.rotation = Quaternion.Slerp(start, end, t / duration);

            t += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the end rotation
        transform.rotation = end;
    }


    void RotateSwordToMouse(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Devil" || other.gameObject.tag == "Slime" ||
        other.gameObject.tag == "Goblin" || other.gameObject.tag == "Giant Goblin" ||
        other.gameObject.tag == "Skeleton" || other.gameObject.tag == "Pumpkin")
        {
            other.GetComponent<Enemy>().TakeDamage(swordDamge, "Sword");
        }
    }
}
