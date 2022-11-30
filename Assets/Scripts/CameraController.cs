using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTarget;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _startRotation;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTarget.position;
        _startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, playerTarget.position + transform.rotation * offset, _moveSpeed * Time.fixedDeltaTime),
                                      Quaternion.Lerp(transform.rotation, playerTarget.rotation * _startRotation, _rotationSpeed * Time.fixedDeltaTime));
    }
}
