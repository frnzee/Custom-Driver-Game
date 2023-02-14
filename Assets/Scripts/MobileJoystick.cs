using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private float _dragThreshold = 0.2f;
    [SerializeField] private int _dragMovementDistance = 75;
    [SerializeField] private int _dragOffsetDistance = 100;

    private RectTransform _joystickTransform;

    public Vector2 InputVector { get; private set; }

    private void Awake()
    {
        _joystickTransform = (RectTransform)transform;
        InputVector = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, eventData.position, null, out Vector2 offset);
        offset = Vector2.ClampMagnitude(offset, _dragOffsetDistance) / _dragOffsetDistance;

        _joystickTransform.anchoredPosition = offset * _dragMovementDistance;
        InputVector = CalculateMovementInput(offset);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > _dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > _dragThreshold ? offset.y : 0;
        return new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickTransform.anchoredPosition = Vector2.zero;
        InputVector = new Vector2(0, 0);
    }
}
