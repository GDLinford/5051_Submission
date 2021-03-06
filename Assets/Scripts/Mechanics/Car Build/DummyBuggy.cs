using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBuggy : MonoBehaviour
{
    [HideInInspector] public BuggyControl buggyControl;

    private AudioSource gSource;
    [SerializeField] AudioClip drill;

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
        gSource = GetComponent<AudioSource>();

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
        if (collider.gameObject.CompareTag("BalancedWheelFR") && buggyControl.frontRightActive != true)
        {
            frontRightBalancedActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("FastWheelFR"));
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelFR"));
        }

        if (collider.gameObject.CompareTag("BalancedWheelFL") && buggyControl.frontLeftActive != true)
        {
            frontLeftBalancedActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelFL"));
            Destroy(GameObject.FindGameObjectWithTag("FastWheelFL"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("BalancedWheelRR") && buggyControl.rearRightActive != true)
        {
            rearRightBalancedActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelRR"));
            Destroy(GameObject.FindGameObjectWithTag("FastWheelRR"));

            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("BalancedWheelRL") && buggyControl.rearLeftActive != true)
        {
            rearLeftBalancedActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelRL"));
            Destroy(GameObject.FindGameObjectWithTag("FastWheelRL"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("FastWheelFR") && buggyControl.frontRightActive != true)
        {
            frontRightFastActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelFR"));
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelFR"));
        }

        if (collider.gameObject.CompareTag("FastWheelFL") && buggyControl.frontLeftActive != true)
        {
            frontLeftFastActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelFL"));
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelFL"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("FastWheelRR") && buggyControl.rearRightActive != true)
        {
            rearRightFastActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelRR"));
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelRR"));
        }

        if (collider.gameObject.CompareTag("FastWheelRL") && buggyControl.rearLeftActive != true)
        {
            rearLeftFastActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelRL"));
            Destroy(GameObject.FindGameObjectWithTag("SlowWheelRL"));
        }

        if (collider.gameObject.CompareTag("SlowWheelFR") && buggyControl.frontRightActive != true)
        {
            frontRightSlowActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("FastWheelFR"));
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelFR"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("SlowWheelFL") && buggyControl.frontLeftActive != true)
        {
            frontLeftSlowActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("FastWheelFL"));
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelFL"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("SlowWheelRR") && buggyControl.rearRightActive != true)
        {
            rearRightSlowActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("FastWheelRR"));
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelRR"));
            //there will be more logic if another wheel is already active but that will be added when I know the basics are working
        }

        if (collider.gameObject.CompareTag("SlowWheelRL") && buggyControl.rearLeftActive != true)
        {
            rearLeftSlowActive = true;
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("FastWheelRL"));
            Destroy(GameObject.FindGameObjectWithTag("BalancedWheelRL"));
        }

        //the balanced one
        if (collider.gameObject.CompareTag("SteeringWheel1") && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            buggyControl.carSetting.maxSteerAngle += 200;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel2"));
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel3"));
        }

        //the fast one
        if (collider.gameObject.CompareTag("SteeringWheel2") && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            buggyControl.carSetting.LimitForwardSpeed += 50;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel1"));
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel3"));
        }

        //the slow one
        if (collider.gameObject.CompareTag("SteeringWheel3") && buggyControl.steeringWheelActive != true)
        {
            buggyControl.steeringWheelActive = true;
            //add the game obhect active for the steering wheel when its on the car
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel1"));
            Destroy(GameObject.FindGameObjectWithTag("SteeringWheel3"));
        }

        //the balanced one
        if (collider.gameObject.CompareTag("Engine1") && buggyControl.engineActive != true)
        {
            buggyControl.engineActive = true;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("Engine2"));
            Destroy(GameObject.FindGameObjectWithTag("Engine3"));
        }

        //the fast one
        if (collider.gameObject.CompareTag("Engine2") && buggyControl.engineActive != true)
        {
            buggyControl.engineActive = true;
            buggyControl.carSetting.LimitForwardSpeed += 50;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("Engine1"));
            Destroy(GameObject.FindGameObjectWithTag("Engine3"));
        }

        //the slow one
        if (collider.gameObject.CompareTag("Engine3") && buggyControl.engineActive != true)
        {
            buggyControl.speed -= buggyControl.myRigidbody.velocity.magnitude * 1f;
            buggyControl.engineActive = true;
            //As with steering Wheels when tthe engine is added to the car enable/show it on the model
            Destroy(collider.gameObject);
            gSource.PlayOneShot(drill);
            Destroy(GameObject.FindGameObjectWithTag("Engine1"));
            Destroy(GameObject.FindGameObjectWithTag("Engine2"));
        }
    }
}
