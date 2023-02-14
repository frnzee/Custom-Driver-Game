using UnityEngine;
using UnityEngine.EventSystems;

public class BrakePedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool ButtonPressed { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }
}