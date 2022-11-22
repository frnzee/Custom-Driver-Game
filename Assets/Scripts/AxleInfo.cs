using System;
using UnityEngine;

[Serializable]

public class AxleInfo
{
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;

    public Transform leftWheel;
    public Transform rightWheel;

    public bool steering;
    public bool acceleration;
}
