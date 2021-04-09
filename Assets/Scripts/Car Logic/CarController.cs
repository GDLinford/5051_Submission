using UnityEngine;


#pragma warning disable 649
namespace UnityStandardAssets.Vehicles.Car
{
    internal enum CarDriveType
    {
        FrontWheelDrive,
        RearWheelDrive,
        FourWheelDrive
    }

    internal enum SpeedType
    {
        MPH,
        KPH
    }

    public class CarController : Stats
    {
        [Header("State Change")]
        // For switching between car and character
        private StateChanger stateChange;

        [HideInInspector] public WheelHp WheelHp;

        [SerializeField] private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive;
        [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4];
        [SerializeField] private WheelEffects[] m_WheelEffects = new WheelEffects[4];
        [SerializeField] private Vector3 m_CentreOfMassOffset;
        [SerializeField] private float m_MaximumSteerAngle;
        private float collideTimer;
        private bool collided;
        [Range(0, 1)] [SerializeField] private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
        [Range(0, 1)] [SerializeField] private float m_TractionControl; // 0 is no traction control, 1 is full interference
        [SerializeField] private float m_FullTorqueOverAllWheels;
        [SerializeField] private float m_ReverseTorque;
        [SerializeField] private float m_MaxHandbrakeTorque;
        [SerializeField] private float m_Downforce = 100f;
        [SerializeField] private SpeedType m_SpeedType;
        [SerializeField] private static int NoOfGears = 5;
        [SerializeField] private float m_RevRangeBoundary = 1f;
        [SerializeField] private float m_SlipLimit;
        [SerializeField] private float m_BrakeTorque;

        [Header("Wheel visuals")]

        [SerializeField] private GameObject[] m_WheelMeshes = new GameObject[12];

        //parts Active bools
        [HideInInspector] public bool frontLeftActive;
        [HideInInspector] public bool frontRightActive;
        [HideInInspector] public bool rearLeftActive;
        [HideInInspector] public bool rearRightActive;
        [HideInInspector] public bool steeringWheelActive;
        [HideInInspector] public bool engineActive;

        private Quaternion[] m_WheelMeshLocalRotations;
        private Vector3 m_Prevpos, m_Pos;
        private float m_SteerAngle;
        private int m_GearNum;
        private float m_GearFactor;
        private float m_OldRotation;
        private float m_CurrentTorque;
        private Rigidbody m_Rigidbody;
        private const float k_ReversingThreshold = 0.01f;

        public bool Skidding { get; private set; }
        public float BrakeInput { get; private set; }
        public float CurrentSteerAngle{ get { return m_SteerAngle; }}
        public float CurrentSpeed{ get { return m_Rigidbody.velocity.magnitude*2.23693629f; }}
        public float MaxSpeed{get { return m_Topspeed; }}
        public float Revs { get; private set; }
        public float AccelInput { get; private set; }

        // Use this for initialization
        private void Start()
        {
            WheelHp = GetComponent<WheelHp>();
            WheelHp.SetHealth(50);

            stateChange = GetComponent<StateChanger>();

            m_WheelMeshLocalRotations = new Quaternion[4];
            for (int i = 0; i < 4; i++)
            {
                m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;
            }
            m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;

            m_MaxHandbrakeTorque = float.MaxValue;

            m_Rigidbody = GetComponent<Rigidbody>();
            m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl*m_FullTorqueOverAllWheels);
        }

        private void OnCollisionEnter(Collision collider)
        {
            //if the wheels have a specific tag on them then a specific object in the hierachy will become active

            if (collider.gameObject.tag == "BalancedWheelFR" && frontRightActive != true)
            {
                frontRightActive = true;
                m_WheelMeshes[0].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "BalancedWheelFL" && frontLeftActive != true)
            {
                frontLeftActive = true;
                m_WheelMeshes[1].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "BalancedWheelRR" && rearRightActive != true)
            {
                rearRightActive = true;
                m_WheelMeshes[2].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "BalancedWheelRL" && rearLeftActive != true)
            {
                rearLeftActive = true;
                m_WheelMeshes[3].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "FastWheelFR" && frontRightActive != true)
            {
                frontRightActive = true;
                m_WheelMeshes[6].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working

                m_Topspeed += 12;
            }

            if (collider.gameObject.tag == "FastWheelFL" && frontLeftActive != true)
            {
                frontLeftActive = true;
                m_WheelMeshes[7].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed += 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "FastWheelRR" && rearRightActive != true)
            {
                rearRightActive = true;
                m_WheelMeshes[4].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed += 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "FastWheelRL" && rearLeftActive != true)
            {
                rearLeftActive = true;
                m_WheelMeshes[5].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed += 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "SlowWheelFR" && frontRightActive != true)
            {
                frontRightActive = true;
                m_WheelMeshes[9].SetActive(true);
                Destroy(collider.gameObject);
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working

                m_Topspeed -= 12;
            }

            if (collider.gameObject.tag == "SlowWheelFL" && frontLeftActive != true)
            {
                frontLeftActive = true;
                m_WheelMeshes[11].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed -= 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "SlowWheelRR" && rearRightActive != true)
            {
                rearRightActive = true;
                m_WheelMeshes[8].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed -= 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "SlowWheelRL" && rearLeftActive != true)
            {
                rearLeftActive = true;
                m_WheelMeshes[9].SetActive(true);
                Destroy(collider.gameObject);
                m_Topspeed -= 12;
                //there will be more logic if another wheel is already active but that will be added when I know the basics are working
            }

            if (collider.gameObject.tag == "SteeringWheel1" && steeringWheelActive != true)
            {
                steeringWheelActive = true;
                //add the game obhect active for the steering wheel when its on the car
                Destroy(collider.gameObject);
            }

            if (collider.gameObject.tag == "SteeringWheel2" && steeringWheelActive != true)
            {
                steeringWheelActive = true;
                //add the game obhect active for the steering wheel when its on the car
                Destroy(collider.gameObject);
                m_Topspeed += 12;
            }

            if (collider.gameObject.tag == "SteeringWheel3" && steeringWheelActive != true)
            {
                steeringWheelActive = true;
                //add the game obhect active for the steering wheel when its on the car
                Destroy(collider.gameObject);
            }

            if (collider.gameObject.tag == "Engine1" && engineActive != true)
            {
                engineActive = true;
                //As with steering Wheels when tthe engine is added to the car enable/show it on the model
                Destroy(collider.gameObject);
                m_Topspeed += 12;
            }

            if (collider.gameObject.tag == "Engine2" && engineActive != true)
            {
                engineActive = true;
                //As with steering Wheels when tthe engine is added to the car enable/show it on the model
                Destroy(collider.gameObject);
            }

            if (collider.gameObject.tag == "Engine3" && engineActive != true)
            {
                engineActive = true;
                //As with steering Wheels when tthe engine is added to the car enable/show it on the model
                Destroy(collider.gameObject);
            }

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
                        Physics.IgnoreCollision(m_WheelColliders[0], GetComponent<Collider>(), true);
                        m_WheelColliders[0].gameObject.SetActive(false);
                        //add in the meshes as well so those get destroyed 
                    }

                    if (WheelHp.WheelsHP == 6)
                    {
                        Physics.IgnoreCollision(m_WheelColliders[1], GetComponent<Collider>(), true);
                        m_WheelColliders[1].gameObject.SetActive(false);
                    }

                    if (WheelHp.WheelsHP == 4)
                    {
                        Physics.IgnoreCollision(m_WheelColliders[2], GetComponent<Collider>(), true);
                        m_WheelColliders[2].gameObject.SetActive(false);
                    }

                    if (WheelHp.WheelsHP == 2)
                    {
                        Physics.IgnoreCollision(m_WheelColliders[3], GetComponent<Collider>(), true);
                        m_WheelColliders[3].gameObject.SetActive(false);
                    }

                    if (WheelHp.WheelsHP > 4)
                    {
                        m_WheelColliders[2].gameObject.SetActive(true);
                    }

                    if (WheelHp.WheelsHP > 6)
                    {
                        m_WheelColliders[1].gameObject.SetActive(true);
                    }

                    if (WheelHp.WheelsHP > 8)
                    {
                        m_WheelColliders[0].gameObject.SetActive(true);
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

        public void Hurt(int amount, int delay)
        {
            StartCoroutine(WheelHp.DelayDamage(amount, delay));
        }

        private void GearChanging()
        {
            float f = Mathf.Abs(CurrentSpeed/MaxSpeed);
            float upgearlimit = (1/(float) NoOfGears)*(m_GearNum + 1);
            float downgearlimit = (1/(float) NoOfGears)*m_GearNum;

            if (m_GearNum > 0 && f < downgearlimit)
            {
                m_GearNum--;
            }

            if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
            {
                m_GearNum++;
            }
        }


        // simple function to add a curved bias towards 1 for a value in the 0-1 range
        private static float CurveFactor(float factor)
        {
            return 1 - (1 - factor)*(1 - factor);
        }


        // unclamped version of Lerp, to allow value to exceed the from-to range
        private static float ULerp(float from, float to, float value)
        {
            return (1.0f - value)*from + value*to;
        }


        private void CalculateGearFactor()
        {
            float f = (1/(float) NoOfGears);
            // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
            // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
            var targetGearFactor = Mathf.InverseLerp(f*m_GearNum, f*(m_GearNum + 1), Mathf.Abs(CurrentSpeed/MaxSpeed));
            m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime*5f);
        }


        private void CalculateRevs()
        {
            // calculate engine revs (for display / sound)
            // (this is done in retrospect - revs are not used in force/power calculations)
            CalculateGearFactor();
            var gearNumFactor = m_GearNum/(float) NoOfGears;
            var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
            Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
        }


        public void Move(float steering, float accel, float footbrake, float handbrake)
        {
            if (stateChange.changed == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    Quaternion quat;
                    Vector3 position;
                    m_WheelColliders[i].GetWorldPose(out position, out quat);
                    m_WheelMeshes[i].transform.position = position;
                    m_WheelMeshes[i].transform.rotation = quat;
                }

                //clamp input values
                steering = Mathf.Clamp(steering, -1, 1);
                AccelInput = accel = Mathf.Clamp(accel, 0, 1);
                BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);
                handbrake = Mathf.Clamp(handbrake, 0, 1);

                //Set the steer on the front wheels.
                //Assuming that wheels 0 and 1 are the front wheels.
                m_SteerAngle = steering * m_MaximumSteerAngle;
                m_WheelColliders[0].steerAngle = m_SteerAngle;
                m_WheelColliders[1].steerAngle = m_SteerAngle;

                SteerHelper();
                ApplyDrive(accel, footbrake);
                CapSpeed();

                //Set the handbrake.
                //Assuming that wheels 2 and 3 are the rear wheels.
                if (handbrake > 0f)
                {
                    var hbTorque = handbrake * m_MaxHandbrakeTorque;
                    m_WheelColliders[2].brakeTorque = hbTorque;
                    m_WheelColliders[3].brakeTorque = hbTorque;
                }


                CalculateRevs();
                GearChanging();

                AddDownForce();
                CheckForWheelSpin();
                TractionControl();
            }    
        }


        private void CapSpeed()
        {
            float speed = m_Rigidbody.velocity.magnitude;
            switch (m_SpeedType)
            {
                case SpeedType.MPH:

                    speed *= 2.23693629f;
                    if (speed > m_Topspeed)
                        m_Rigidbody.velocity = (m_Topspeed/2.23693629f) * m_Rigidbody.velocity.normalized;
                    break;

                case SpeedType.KPH:
                    speed *= 3.6f;
                    if (speed > m_Topspeed)
                        m_Rigidbody.velocity = (m_Topspeed/3.6f) * m_Rigidbody.velocity.normalized;
                    break;
            }
        }


        private void ApplyDrive(float accel, float footbrake)
        {

            float thrustTorque;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 4f);
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].motorTorque = thrustTorque;
                    }
                    break;

                case CarDriveType.FrontWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[0].motorTorque = m_WheelColliders[1].motorTorque = thrustTorque;
                    break;

                case CarDriveType.RearWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[2].motorTorque = m_WheelColliders[3].motorTorque = thrustTorque;
                    break;

            }

            for (int i = 0; i < 4; i++)
            {
                if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f)
                {
                    m_WheelColliders[i].brakeTorque = m_BrakeTorque*footbrake;
                }
                else if (footbrake > 0)
                {
                    m_WheelColliders[i].brakeTorque = 0f;
                    m_WheelColliders[i].motorTorque = -m_ReverseTorque*footbrake;
                }
            }
        }


        private void SteerHelper()
        {
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelhit;
                m_WheelColliders[i].GetGroundHit(out wheelhit);
                if (wheelhit.normal == Vector3.zero)
                    return; // wheels arent on the ground so dont realign the rigidbody velocity
            }

            // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
            if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
            {
                var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
                Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
                m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
            }
            m_OldRotation = transform.eulerAngles.y;
        }


        // this is used to add more grip in relation to speed
        private void AddDownForce()
        {
            m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up*m_Downforce*
                                                         m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
        }


        // checks if the wheels are spinning and is so does three things
        // 1) emits particles
        // 2) plays tiure skidding sounds
        // 3) leaves skidmarks on the ground
        // these effects are controlled through the WheelEffects class
        private void CheckForWheelSpin()
        {
            // loop through all wheels
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelHit;
                m_WheelColliders[i].GetGroundHit(out wheelHit);

                // is the tire slipping above the given threshhold
                if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit)
                {
                    m_WheelEffects[i].EmitTyreSmoke();

                    // avoiding all four tires screeching at the same time
                    // if they do it can lead to some strange audio artefacts
                    if (!AnySkidSoundPlaying())
                    {
                        m_WheelEffects[i].PlayAudio();
                    }
                    continue;
                }

                // if it wasnt slipping stop all the audio
                if (m_WheelEffects[i].PlayingAudio)
                {
                    m_WheelEffects[i].StopAudio();
                }
                // end the trail generation
                m_WheelEffects[i].EndSkidTrail();
            }
        }

        // crude traction control that reduces the power to wheel if the car is wheel spinning too much
        private void TractionControl()
        {
            WheelHit wheelHit;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    // loop through all wheels
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].GetGroundHit(out wheelHit);

                        AdjustTorque(wheelHit.forwardSlip);
                    }
                    break;

                case CarDriveType.RearWheelDrive:
                    m_WheelColliders[2].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[3].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;

                case CarDriveType.FrontWheelDrive:
                    m_WheelColliders[0].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[1].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;
            }
        }


        private void AdjustTorque(float forwardSlip)
        {
            if (forwardSlip >= m_SlipLimit && m_CurrentTorque >= 0)
            {
                m_CurrentTorque -= 10 * m_TractionControl;
            }
            else
            {
                m_CurrentTorque += 10 * m_TractionControl;
                if (m_CurrentTorque > m_FullTorqueOverAllWheels)
                {
                    m_CurrentTorque = m_FullTorqueOverAllWheels;
                }
            }
        }


        private bool AnySkidSoundPlaying()
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_WheelEffects[i].PlayingAudio)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
