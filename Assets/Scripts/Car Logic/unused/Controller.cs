using UnityEngine;
using UnityEngine.UI;


public class Controller : Stats
{
    [HideInInspector] public WheelHp WheelHp;

    [Header("State Change")]
	// For switching between car and character
	private StateChanger stateChange;

    [Header("Inputs")]
	public KeyCode FlipRight;
    public KeyCode FlipLeft;
    public KeyCode BoostPower;
    public KeyCode Accelerate;
    public KeyCode Decelerate;

    public bool braking;
    public bool collided;
    public bool Accelerating;

    [Header("Boost")]
    //Boost Logic
    public bool boosting;
    [HideInInspector]
    public bool canBoost = true;
    [SerializeField]
    private float TotalBoost = 10f;
    public float MaxBoost { get { return TotalBoost; } set { TotalBoost = value; } }

    //amount of boost we currently have left
    [SerializeField]
    private float boostLeft;
    public float Boost { get { return boostLeft; } set { boostLeft = Mathf.Clamp(value, 0f, TotalBoost); } }

    //force thats going to be applied to the car itself
    [SerializeField]
    private float boostForce;
    public float BoostForce { get { return boostForce; } set { boostForce = value; } }

    private Rigidbody rgBody;

    public int FuelA;
    public Text FuelText;

    [HideInInspector]
    public float cooldown = 3;

    private const string HORIZONTAL = "P2 Horizontal";
    private const string VERTICAL = "P2 Vertical";

    private float horizontalInput;
    private float verticalInput;

    public double carSpeed;
    private float collideTimer;

    [Header("Wheels")]
    //Wheel colliders
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider RearLeft;
    public WheelCollider RearRight;

    //The transforms for the wheels
    public Transform frontLeftT;
    public Transform frontRightT;
    public Transform RearLeftT;
    public Transform RearRightT;

    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        //Calculates car speed
        motorForce = 7500 + (float)(rgBody.velocity.magnitude * 2.237);
        maxSteeringAngle = 60f;
        WheelHp = GetComponent<WheelHp>();
        WheelHp.SetHealth(50);
        mass = -2f;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, mass, 0f);
        FuelA = Mathf.Clamp(FuelA, 0, 5);
        boostLeft = TotalBoost;
        stateChange = GetComponent<StateChanger>();
    }

    private void OnCollisionEnter(Collision collider)
    {
        float collisionForce = collider.impulse.magnitude / Time.deltaTime;
        collideTimer -= Time.deltaTime;
        Debug.Log("Collide");

        if (collisionForce > 50f && collideTimer <= 0)
        {
            collided = true;
            if (collided == true)
            {
                WheelHp.WheelsHP -= 1;

                Debug.Log("Wheels HP: " + WheelHp.WheelsHP);

                if (WheelHp.WheelsHP == 8)
                {
                    Physics.IgnoreCollision(RearLeft, GetComponent<Collider>(), true);
                    RearLeft.gameObject.SetActive(false);
                }

                if (WheelHp.WheelsHP == 6)
                {
                    Physics.IgnoreCollision(RearRight, GetComponent<Collider>(), true);
                    RearRight.gameObject.SetActive(false);
                }

                if (WheelHp.WheelsHP == 4)
                {
                    Physics.IgnoreCollision(frontLeft, GetComponent<Collider>(), true);
                    frontLeft.gameObject.SetActive(false);
                }

                if (WheelHp.WheelsHP == 2)
                {
                    Physics.IgnoreCollision(frontRight, GetComponent<Collider>(), true);
                    frontRight.gameObject.SetActive(false);
                }

                if (WheelHp.WheelsHP > 4)
                {
                    RearLeft.gameObject.SetActive(true);
                }

                if (WheelHp.WheelsHP > 6)
                {
                    frontRight.gameObject.SetActive(true);
                }

                if (WheelHp.WheelsHP > 8)
                {
                    frontLeft.gameObject.SetActive(true);
                }

                collided = false;
                collideTimer = 10f;
                //else if (WheelHp.WheelsHP == 0 && Input.GetKeyDown(KeyCode.Q))
                //{
                //    WheelHp.WheelsHP += 10;

                //    RearLeft.gameObject.SetActive(true);
                //    RearRight.gameObject.SetActive(true);
                //    frontLeft.gameObject.SetActive(true);
                //    frontRight.gameObject.SetActive(true);

                //    //decrease score here
                //}
            }
        }

    }

	private void Update() {

        if (canBoost)
        {
            boostLeft += Time.deltaTime;
            if (boostLeft > TotalBoost)
            {
                boostLeft = TotalBoost;
            }
        }
		if (Input.GetKey(KeyCode.Space))
        {
            frontLeft.brakeTorque = brakeForce;
            frontRight.brakeTorque = brakeForce;
            braking = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
            braking = false;
        }

        if (WheelHp.WheelsHP == 0)
        {
            Debug.Log("We dead");
        }
    } 

	private void FixedUpdate()
    {
        GetInput();
        Forward();
        Steer();
        WheelVisuals();
        if (Input.GetKey(FlipRight))
            transform.Rotate(0, 0, 30);

        if (Input.GetKey(FlipLeft))
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);

        boosting = Input.GetKey(BoostPower);

        if (boosting && canBoost && boostLeft > 0.1f)
        {
            rgBody.AddForce(transform.forward * boostForce);

            boostLeft -= Time.fixedDeltaTime;
            if (boostLeft <= 9f && boostLeft >= 8f)
            {
                boostLeft = 9f;
            }
        }
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
    }

    private void Forward()
    {
        if(stateChange.changed == true)
        {
            //This increases Motoforce
            if (Input.GetKey(KeyCode.W))
            {
                Accelerating = true;
                motorForce++;
            }
            else
            {
                //when its let go it will decrease
                motorForce--;
            }

            //if the cars going too fast decelerate, unless the car is boosting
            if (motorForce >= 10000f && !boosting)
            {
                motorForce = 10000f;
            }

            else if (motorForce <= 0)
            {
                motorForce = 0;
            }
            frontLeft.motorTorque = verticalInput * motorForce;
            frontRight.motorTorque = verticalInput * motorForce;
        }
    }

    private void Steer()
    {
        Angle = maxSteeringAngle * horizontalInput;
        frontLeft.steerAngle = Angle;
        frontRight.steerAngle = Angle;
    }

    private void WheelVisuals()
    {
        UpdateSingleWheel(frontLeft, frontLeftT);
        UpdateSingleWheel(frontRight, frontRightT);
        UpdateSingleWheel(RearLeft, RearLeftT);
        UpdateSingleWheel(RearRight, RearRightT);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform WTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        WTransform.rotation = rot;
        WTransform.position = pos;
    }

    //private void BeginBreaking()
    //{
    //    RearLeft.brakeTorque = currentBreakForce;
    //    RearRight.brakeTorque = currentBreakForce;
    //    frontLeft.brakeTorque = currentBreakForce;
    //    frontRight.brakeTorque = currentBreakForce;
    //}

    //private void ResetWheels()
    //{
    //    frontLeft.brakeTorque = 0f;
    //    frontRight.brakeTorque = 0f;
    //    RearLeft.brakeTorque = 0f;
    //    RearRight.brakeTorque = 0f;
    //}

    public void ItemBoost(float boostAmount)
    {
        if (boostAmount == 0)
        {
            return;
        }
    }

    public void AddFuel (int amount)
    {
        FuelA += amount;
        FuelText.text = "Fuel Amount " + FuelA;
    }

    public void Hurt(int amount, int delay)
    {
        StartCoroutine(WheelHp.DelayDamage(amount, delay));
    }
}
