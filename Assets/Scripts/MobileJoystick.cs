using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private RectTransform _joystickTransform;

    [SerializeField] private float _dragThreshold = 0;
    [SerializeField] private int _dragMovementDistance = 30;
    [SerializeField] private int _dragOffsetDistance = 100;

    public event Action<Vector2> OnMove;

    public Vector2 InputVector;

    private void Awake()
    {
        _joystickTransform = (RectTransform)transform;
        InputVector = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, eventData.position, null, out offset);
        offset = Vector2.ClampMagnitude(offset, _dragOffsetDistance) / _dragOffsetDistance;
        Debug.Log(offset);


        _joystickTransform.anchoredPosition = offset * _dragMovementDistance;
        InputVector = CalculateMovementInput(offset);
        OnMove?.Invoke(InputVector);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > _dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > _dragThreshold ? offset.y : 0;
        return new Vector2(x, y);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InputVector = new Vector2(0, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
    }
}
