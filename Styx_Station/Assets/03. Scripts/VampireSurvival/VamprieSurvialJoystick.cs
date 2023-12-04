using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VamprieSurvialJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public enum Axis
    {
        H,
        V,
    }
    private RectTransform rectTransform;
    public Image stick;
    private float radius;
    private Vector3 originalPoint;
    private Vector2 value;
    public CanvasScaler canvasSclaer;
    private int pointerId;
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        radius = (rectTransform.rect.height * 0.5f * Screen.height) / canvasSclaer.referenceResolution.y;
    }
    private void OnEnable()
    {
        rectTransform.position = Input.mousePosition;
        originalPoint = stick.rectTransform.position;
    }
    private void UpdateStickPos(Vector3 screenPos)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, screenPos, null, out Vector3 newPoint);

        var delta = Vector3.ClampMagnitude(newPoint - originalPoint, radius);

        value = delta / radius;
        stick.rectTransform.position = originalPoint + delta;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsDragging)
            return;
        IsDragging = true;
        pointerId = eventData.pointerId;

        UpdateStickPos(eventData.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        UpdateStickPos(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        IsDragging = false;
        UpdateStickPos(originalPoint);
    }
    public float GetAxis(Axis axis)
    {
        switch (axis)
        {
            case Axis.H:
                return value.x;
            case Axis.V:
                return value.y;
        }
        return 0f;
    }

}
