using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anti_Flip : MonoBehaviour
{
    public int xRotationLimit = 70; 
    public int yRotationLimit = 70;
    public int zRotationLimit = 70;

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.x > xRotationLimit)
        {
            transform.rotation = Quaternion.identity;
        }

        if (transform.rotation.eulerAngles.y > yRotationLimit)
        {
            transform.rotation = Quaternion.identity;
        }

        if (transform.rotation.eulerAngles.z > zRotationLimit)
        {
            transform.rotation = Quaternion.identity;
        }

    }
}
