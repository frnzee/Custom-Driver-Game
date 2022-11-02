using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ambulance : MonoBehaviour
{
    [SerializeField] private AxleInfo [] _ambulanceAxis = new AxleInfo[3];

    public float accelerationSpeed = 100f;
    public float brakeSpeed = 100f;
    public float steeringAngle = 30f;
    public float maxSpeed = 15f;

    public Text speedText;

    public Transform centerOfMass;

    public ParticleSystem dirt_L;
    public ParticleSystem dirt_R;

    private float _horizontalInput;
    private float _verticalInput;

    private float _currentSpeed;
    private float _currentBrakeSpeed = 300f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Accelerate();
        Brake();
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

    private void Accelerate()
    {
        var vel = rb.velocity;
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
            }

            speedText.text = "SPEED: " + _currentSpeed.ToString("0");

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

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;

    public Transform leftWheel;
    public Transform rightWheel;

    public bool steering;
    public bool acceleration;
}