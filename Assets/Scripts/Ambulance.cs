using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ambulance : MonoBehaviour
{
    [SerializeField] private AxleInfo[] _ambulanceAxis = new AxleInfo[3];

    [SerializeField] private float accelerationSpeed = 100f;
    [SerializeField] private float brakeSpeed = 100f;
    [SerializeField] private float steeringAngle = 30f;
    [SerializeField] private float maxSpeed = 15f;

    [SerializeField] private Text speedText;

    [SerializeField] private Transform centerOfMass;

    [SerializeField] private ParticleSystem dirtLeft;
    [SerializeField] private ParticleSystem dirtRight;

    private MoveAmbulance _moveAmbulance;

    private float _horizontalInput;
    private float _verticalInput;
    private float _movementX;
    private float _movementY;

    private float _currentSpeed;
    private float _currentBrakeSpeed = 300f;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _moveAmbulance = new MoveAmbulance();
    }

    private void OnEnable()
    {
        _moveAmbulance.Enable();
    }

    private void OnDisable()
    {
        _moveAmbulance.Disable();
    }
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Accelerate();
        Brake();
        if (_currentSpeed > 0.2)
        {
            StartParticles();
        }
    }

    private void Brake()
    {
        foreach (AxleInfo axle in _ambulanceAxis)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _currentBrakeSpeed = brakeSpeed * _verticalInput;
            }
            else
            {
                _currentBrakeSpeed = 0f;
            }
            axle.leftWheelCollider.brakeTorque = _currentBrakeSpeed;
            axle.rightWheelCollider.brakeTorque = _currentBrakeSpeed;
        }
    }

    private void StartParticles()
    {
        dirtLeft.Play();
        dirtRight.Play();
    }

    private void Accelerate()
    {
        var vel = _rigidBody.velocity;
        _currentSpeed = vel.magnitude;

        foreach (AxleInfo axle in _ambulanceAxis)
        {
            if (axle.steering)
            {
                axle.leftWheelCollider.steerAngle = steeringAngle * _horizontalInput;
                axle.rightWheelCollider.steerAngle = steeringAngle * _horizontalInput;
            }

            if (axle.acceleration && _currentSpeed <= maxSpeed)
            {

                axle.leftWheelCollider.motorTorque = accelerationSpeed * _verticalInput;
                axle.rightWheelCollider.motorTorque = accelerationSpeed * _verticalInput;
            }
            else
            {
                axle.leftWheelCollider.motorTorque = 0f;
                axle.rightWheelCollider.motorTorque = 0f;
                axle.leftWheelCollider.ConfigureVehicleSubsteps(5, 10, 15);
                axle.rightWheelCollider.ConfigureVehicleSubsteps(5, 10, 15);

            }

            speedText.text = "SPEED: " + (_currentSpeed * 10).ToString("00");

            VisualWheelsToColliders(axle.leftWheelCollider, axle.leftWheel);
            VisualWheelsToColliders(axle.rightWheelCollider, axle.rightWheel);
        }
    }

    private void VisualWheelsToColliders(WheelCollider wheelCollider, Transform wheel)
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheel.position = position;
        wheel.rotation = rotation;
    }
}
