using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class UDropable : MonoBehaviour
{
    public float autoMoveArrivalThreshold = 0.05f;

    public event Action<UDropable> OnDropComplete = delegate { };
    public event Action<UDropable> OnReturnComplete = delegate { };

    public Func<UDropable, bool> OnDropAccepted = delegate { return true; };
    public Func<UDropable, bool> OnDropRejected = delegate { return true; };

    List<UDropzone> dropZones = new List<UDropzone>();
    UDropzone targetDropzone = null;
    Collider2D _collider;
    UDraggable _dragging;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragging = GetComponent<UDraggable>();
        _dragging.OnDragStop += HandleDragStop;

        foreach (var dropzone in GameObject.FindObjectsOfType<UDropzone>())
            dropZones.Add(dropzone);
    }

    void HandleDragStop(UDraggable draggable)
    {
        UDropzone newTargetDropzone = null;
        foreach (var dropzone in dropZones)
        {
            if (dropzone.CanDrop(_collider))
            {
                newTargetDropzone = dropzone;
                break;
            }
        }

        if (newTargetDropzone != null)
        {
            targetDropzone?.Lift();
            targetDropzone = newTargetDropzone;

            if (OnDropAccepted(this))
            {
                MoveToTarget.Go(gameObject, targetDropzone.transform.position, autoMoveArrivalThreshold)
                    .OnArrival = (_) => CompleteDrop();
            }
        }
        else if (OnDropRejected(this))
        {
            MoveToTarget.Go(gameObject, _dragging.OriginalPos, autoMoveArrivalThreshold)
                .OnArrival = (_) => CompleteReturn();
        }
    }

    bool CompleteDrop()
    {
        targetDropzone.Drop(this);
        _dragging.SetOriginalPos(targetDropzone.transform.position);
        OnDropComplete(this);
        return true;
    }

    bool CompleteReturn()
    {
        OnReturnComplete(this);
        return true;
    }

    public static UDropable CreateDropable(GameObject obj)
    {
        return obj.AddComponent<UDropable>();
    }
}
