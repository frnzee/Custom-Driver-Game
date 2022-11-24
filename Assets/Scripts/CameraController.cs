using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTarget;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    Quaternion startRotation;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTarget.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position + transform.rotation * offset, _moveSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTarget.rotation * startRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
