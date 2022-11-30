using UnityEngine;
using UnityEngine.UI;

public class Ambulance : MonoBehaviour
{
    private static Ambulance _instance;
    public static Ambulance Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogError("Instance is not specified");
            }
            return _instance;
        }
    }

    [SerializeField] private AxleInfo[] _ambulanceAxis = new AxleInfo[3];

    [SerializeField] private float _accelerationSpeed = 100f;
    [SerializeField] private float _brakeSpeed = 100f;
    [SerializeField] private float _steeringAngle = 40f;
    [SerializeField] private float _maxSpeed = 15f;

    [SerializeField] private Transform _centerOfMass;

    [SerializeField] private ParticleSystem _dirtLeft;
    [SerializeField] private ParticleSystem _dirtRight;

    [SerializeField] private Text _speedText;

    private MobileJoystick _mobileJoystick;

    private float _horizontalInput;
    private float _verticalInput;

    private float _currentSpeed;
    private float _currentBrakeSpeed;

    private Rigidbody _rigidBody;

    private GasPedal _gasPedal;
    private BrakePedal _brakePedal;

    public float CurrentSpeed => _currentSpeed;

    private void Awake()
    {
        _instance = this;

        _rigidBody = GetComponent<Rigidbody>();
        _mobileJoystick = GetComponentInChildren<MobileJoystick>();
        _gasPedal = GetComponentInChildren<GasPedal>();
        _brakePedal = GetComponentInChildren<BrakePedal>();
    }

    private void Start()
    {
        _rigidBody.centerOfMass = _centerOfMass.localPosition;

        foreach (AxleInfo axle in _ambulanceAxis)
        {
            axle.leftWheelCollider.ConfigureVehicleSubsteps(5, 10, 15);
            axle.rightWheelCollider.ConfigureVehicleSubsteps(5, 10, 15);
        }
    }

    private void FixedUpdate()
    {
        AccelerateAndSteer();
        Brake();
    }

    private void Update()
    {
        _horizontalInput = _mobileJoystick.InputVector.x;

        if (_mobileJoystick.InputVector.y == 0)
        {
            _verticalInput = 0;
        }
        else
        {
            _verticalInput = _mobileJoystick.InputVector.y < 0 ? -1 : 1;
        }

        _currentSpeed = _rigidBody.velocity.magnitude;

        StartOrStopParticles();
    }

    private void Brake()
    {
        foreach (AxleInfo axle in _ambulanceAxis)
        {
            _currentBrakeSpeed = _brakePedal.ButtonPressed ? _brakeSpeed : 0f;

            axle.leftWheelCollider.brakeTorque = _currentBrakeSpeed;
            axle.rightWheelCollider.brakeTorque = _currentBrakeSpeed;
        }
    }

    private void StartOrStopParticles()
    {
        if (_gasPedal.ButtonPressed)
        {
            _dirtLeft.Play();
            _dirtRight.Play();
        }
        else
        {
            _dirtLeft.Stop();
            _dirtRight.Stop();
        }
    }

    private void AccelerateAndSteer()
    {
        foreach (AxleInfo axle in _ambulanceAxis)
        {
            if (axle.steering)
            {
                axle.leftWheelCollider.steerAngle = _steeringAngle * _horizontalInput;
                axle.rightWheelCollider.steerAngle = _steeringAngle * _horizontalInput;
            }

            if (axle.acceleration && _gasPedal.ButtonPressed)
            {
                if (_verticalInput < 0)
                {
                    axle.leftWheelCollider.motorTorque = -_accelerationSpeed;
                    axle.rightWheelCollider.motorTorque = -_accelerationSpeed;
                }
                else if (_currentSpeed <= _maxSpeed && _verticalInput > 0)
                {
                    axle.leftWheelCollider.motorTorque = _accelerationSpeed;
                    axle.rightWheelCollider.motorTorque = _accelerationSpeed;
                }
                else
                {
                    axle.leftWheelCollider.motorTorque = 0f;
                    axle.rightWheelCollider.motorTorque = 0f;
                }
            }

            RotateWheels(axle.leftWheelCollider, axle.leftWheel);
            RotateWheels(axle.rightWheelCollider, axle.rightWheel);
        }
    }

    private void RotateWheels(WheelCollider wheelCollider, Transform wheel)
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheel.SetPositionAndRotation(position, rotation);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
