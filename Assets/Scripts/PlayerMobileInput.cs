using System;
using UnityEngine;

public class PlayerMobileInput : MonoBehaviour
{
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttack;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action<Vector2> OnMovement;
    public event Action OnWeaponChange;

    [SerializeField] private MobileJoystick _joystick;

    private void Start()
    {
        _joystick.OnMove += Move;
    }

    private void Move(Vector2 input)
    {
        MovementVector = input;
        OnMovement?.Invoke(MovementVector);

    }
}
