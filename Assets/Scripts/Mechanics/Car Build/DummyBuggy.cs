using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBuggy : MonoBehaviour
{
    [HideInInspector] public BuggyControl buggyControl;

    //parts Active bools
    public bool frontLeftBalancedActive;
    public bool frontLeftFastActive;
    public bool frontLeftSlowActive;
    public bool frontRightBalancedActive;
    public bool frontRightFastActive;
    public bool frontRightSlowActive;
    public bool rearLeftBalancedActive;
    public bool rearLeftFastActive;
    public bool rearLeftSlowActive;
    public bool rearRightBalancedActive;
    public bool rearRightFastActive;
    public bool rearRightSlowActive;

    [HideInInspector] public bool completed;

    // Start is called before the first frame update
    void Start()
    {
        buggyControl = FindObjectOfType<BuggyControl>();
        
        frontLeftBalancedActive = false;
        frontLeftFastActive = false;
        frontLeftSlowActive = false;
        frontRightBalancedActive = false;
        frontRightFastActive = false;
        frontRightSlowActive = false;
        rearLeftBalancedActive = false;
        rearLeftFastActive = false;
        rearLeftSlowActive = false;
        rearRightBalancedActive = false;
        rearRightFastActive = false;
        rearRightSlowActive = false;
}

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "BalancedWheelFR" && buggyControl.frontRightActive != true)
        {
            frontRightBalancedActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "BalancedWheelFL" && buggyControl.frontLeftActive != true)
        {
            frontLeftBalancedActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "BalancedWheelRR" && buggyControl.rearRightActive != true)
        {
            rearRightBalancedActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "BalancedWheelRL" && buggyControl.rearLeftActive != true)
        {
            rearLeftBalancedActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "FastWheelFR" && buggyControl.frontRightActive != true)
        {
            frontRightFastActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "FastWheelFL" && buggyControl.frontLeftActive != true)
        {
            frontLeftFastActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "FastWheelRR" && buggyControl.rearRightActive != true)
        {
            rearRightFastActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "FastWheelRL" && buggyControl.rearLeftActive != true)
        {
            rearLeftFastActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "SlowWheelFR" && buggyControl.frontRightActive != true)
        {
            frontRightSlowActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "SlowWheelFL" && buggyControl.frontLeftActive != true)
        {
            frontLeftSlowActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "SlowWheelRR" && buggyControl.rearRightActive != true)
        {
            rearRightSlowActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.tag == "SlowWheelRL" && buggyControl.rearLeftActive != true)
        {
            rearLeftSlowActive = true;
            Destroy(collider.gameObject);
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        //the balanced one
        if (collider.gameObject.tag == "SteeringWheel1" && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
        }

        //the fast one
        if (collider.gameObject.tag == "SteeringWheel2" && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            buggyControl.carSetting.LimitForwardSpeed += 50;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
        }

        //the slow one
        if (collider.gameObject.tag == "SteeringWheel3" && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
        }

        //the balanced one
        if (collider.gameObject.tag == "Engine1" && buggyControl.engineActive != true)
        {
            buggyControl.engineActive = true;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
        }

        //the fast one
        if (collider.gameObject.tag == "Engine2" && buggyControl.engineActive != true)
        {
            buggyControl.engineActive = true;
            buggyControl.carSetting.LimitForwardSpeed += 50;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
        }

        //the slow one
        if (collider.gameObject.tag == "Engine3" && buggyControl.engineActive != true)
        {
            buggyControl.speed -= buggyControl.myRigidbody.velocity.magnitude * 1f;
            buggyControl.engineActive = true;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
        }

        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
