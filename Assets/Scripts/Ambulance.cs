using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{
    [SerializeField] private AxleInfo [] _ambulanceAxis = new AxleInfo[3];

    public float ambulanceSpeed = 300f;
    public float brakeSpeed = 200f;
    public float steeringAngle = 30;
    public float maxSpeed;

    public Transform centerOfMass;

    public ParticleSystem dirt_L;
    public ParticleSystem dirt_R;

    private float horizontalInput;
    private float verticalInput;

    private float _currentAmbulanceSpeed = 0f;
    private float _currentBrakeSpeed = 300f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Accelerate();
        Brake();
    }

    private void Brake()
    {
        foreach (AxleInfo axle in _ambulanceAxis)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _currentBrakeSpeed = brakeSpeed * verticalInput;
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
        foreach (AxleInfo axle in _ambulanceAxis)
        {
            if (axle.steering)
            {
                axle.leftWheelCollider.steerAngle = steeringAngle * horizontalInput;
                axle.rightWheelCollider.steerAngle = steeringAngle * horizontalInput;
            }

            if (axle.acceleration)
            {
                axle.leftWheelCollider.motorTorque = ambulanceSpeed * verticalInput;
                axle.rightWheelCollider.motorTorque = ambulanceSpeed * verticalInput;
            }

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