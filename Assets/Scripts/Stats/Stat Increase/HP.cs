using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : Stats
{
    //probably wouldnt ever go above 10 unless we wanted to put something really out of the way
    [HideInInspector] public WheelHp WheelHp;

    public int increase;
    private void OnTriggerEnter(Collider other)
    {
        Controller controller = other.GetComponent<Controller>();
        controller.WheelHp.WheelsHP += increase;
        Destroy(gameObject);
    }
}
