using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class UDropzone : MonoBehaviour
{
    public Color hoverColor = Color.yellow;
    public Color dropColor = Color.green;

    Collider2D _collider;
    UDropable _ref = null;
    SpriteRenderer _spriteRenderer;
    Color defaultColor;

    public event Action<UDropzone> OnDrop = delegate { };
    public event Action<UDropzone> OnLift = delegate { };

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = _spriteRenderer.color;
        _collider.isTrigger = true;
    }

    public bool IsFull => _ref != null;

    public bool CanDrop(Collider2D dropCollider)
    {
        return enabled && !IsFull && dropCollider.bounds.Intersects(_collider.bounds);
    }

    public void Drop(UDropable obj)
    {
        _ref = obj;
        _spriteRenderer.color = dropColor;
        OnDrop(this);
    }

    public void Lift()
    {
        _ref = null;
        _spriteRenderer.color = defaultColor;
        OnLift(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (CanDrop(other))
            _spriteRenderer.color = hoverColor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsFull)
            _spriteRenderer.color = defaultColor;
    }

    public static UDropzone CreateDropzone(GameObject obj)
    {
        return obj.AddComponent<UDropzone>();
    }
}
