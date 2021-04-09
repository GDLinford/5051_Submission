using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float motorForce;
    public float brakeForce;
    public float maxSteeringAngle;
    [HideInInspector] public float Angle;
    [HideInInspector] public float currentBreakForce;
    [HideInInspector] public float mass;

     public float m_Topspeed = 150;

}
