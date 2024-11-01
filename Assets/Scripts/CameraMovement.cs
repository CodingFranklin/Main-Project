using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    float xMin = -16.08f;
    float xMax = 16.08f;
    float yMax = 13f;
    float yMin = -13f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool xInRange = target.position.x >= xMin && target.position.x <= xMax;
        bool YInRange = target.position.y >= yMin && target.position.y <= yMax;

        // In the range
        if (xInRange && YInRange)
        {
            transform.position = target.position + offset;
        }
        //Y out of range
        else if (xInRange && !YInRange)
        {
            //out of Top
            if (target.position.y > yMax)
            {
                transform.position = new Vector3(target.position.x, yMax, 0) + offset;
            }
            //out of Bottom
            else if (target.position.y < yMin)
            {
                transform.position = new Vector3(target.position.x, yMin, 0) + offset;
            }
            
        }
        //X out of range
        else if (!xInRange && YInRange)
        {
            //out of Right side
            if (target.position.x > xMax)
            {
                transform.position = new Vector3(xMax, target.position.y, 0) + offset;
            }
            //out of Left side
            else if (target.position.x < xMin)
            {
                transform.position = new Vector3(xMin, target.position.y, 0) + offset;
            }
        }
        //both X and Y out of range
        else
        {
            //out of Top-Right Corner
            if (target.position.x > xMax && target.position.y > yMax)
            {
                transform.position = new Vector3(xMax, yMax, 0) + offset;
            }
            //out of Bottom-Right Corner
            else if (target.position.x > xMax && target.position.y < yMin)
            {
                transform.position = new Vector3(xMax, yMin, 0) + offset;
            }
            //out of Top-Left Corner
            else if (target.position.x < xMin && target.position.y > yMax)
            {
                transform.position = new Vector3(xMin, yMax, 0) + offset;
            }
            //out of Bottom-Left Corner
            else if (target.position.x < xMin && target.position.y < yMin)
            {
                transform.position = new Vector3(xMin, yMin, 0) + offset;
            }
        }
    }
}
