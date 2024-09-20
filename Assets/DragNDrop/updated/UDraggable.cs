using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class UDraggable : MonoBehaviour
{
    enum DragState { Idle, Dragging }

    public event Action<UDraggable> OnDragStart = delegate { };
    public event Action<UDraggable> OnDragStop = delegate { };

    DragState state = DragState.Idle;
    Vector3 originalPos = Vector3.zero;
    Vector3 dragOffset = Vector3.zero;

    public Vector3 OriginalPos => originalPos;

    // Set the original position for the draggable object
    public void SetOriginalPos(Vector3 newOriginalPos)
    {
        originalPos = newOriginalPos;
    }
    
    void Start()
    {
        originalPos = transform.position;
    }

    void OnMouseUp()
    {
        if (!enabled || state != DragState.Dragging)
            return;

        state = DragState.Idle;
        OnDragStop(this);
    }

    void OnMouseDown()
    {
        if (!enabled || state != DragState.Idle)
            return;

        dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragOffset.z = 0f;

        state = DragState.Dragging;
        OnDragStart(this);
    }

    void OnMouseDrag()
    {
        if (!enabled)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = dragOffset + mousePos;
    }

    public static UDraggable CreateDraggable(GameObject obj)
    {
        return obj.AddComponent<UDraggable>();
    }
}
