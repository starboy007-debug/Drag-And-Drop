/*
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Dropzone : MonoBehaviour {

	Collider2D _collider;
	Dropable _ref = null;

	void Start () {
		_collider = GetComponent<Collider2D> ();
	}

	public System.Action<Dropzone> OnDrop = delegate {};
	public System.Action<Dropzone> OnLift = delegate {};

	public bool IsFull {
		get {
			return _ref != null;
		}
	}
	
	public bool CanDrop(Collider2D dropCollider){
		return enabled 
			&& !IsFull 
			&& (dropCollider.bounds.Intersects(_collider.bounds) 
				||  _collider.bounds.Contains(dropCollider.transform.position));
	}

	public void Drop(Dropable obj){
		_ref = obj;
		OnDrop (this);
	}

	public void Lift(){
		_ref = null;
		OnLift (this);
	}
}
*/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Dropzone : MonoBehaviour {

    Collider2D _collider;
    Dropable _ref = null;
    SpriteRenderer _spriteRenderer;
    Color defaultColor;
    public Color hoverColor = Color.yellow;
    public Color dropColor = Color.green;

    void Start () {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = _spriteRenderer.color;
        _collider.isTrigger = true;  // Ensure the collider is a trigger
    }

    public System.Action<Dropzone> OnDrop = delegate {};
    public System.Action<Dropzone> OnLift = delegate {};

    public bool IsFull {
        get {
            return _ref != null;
        }
    }
    
    public bool CanDrop(Collider2D dropCollider){
        return enabled 
            && !IsFull 
            && (dropCollider.bounds.Intersects(_collider.bounds) 
                || _collider.bounds.Contains(dropCollider.transform.position));
    }

    public void Drop(Dropable obj){
        _ref = obj;
        _spriteRenderer.color = dropColor;
        OnDrop(this);
    }

    public void Lift(){
        _ref = null;
        _spriteRenderer.color = defaultColor;
        OnLift(this);
    }

    // Trigger when a draggable object enters the dropzone
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Object entered dropzone: " + other.name); // Debugging
        if (CanDrop(other)) {
            _spriteRenderer.color = hoverColor;
        }
    }

    // Trigger when a draggable object exits the dropzone
    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Object exited dropzone: " + other.name); // Debugging
        if (!IsFull) {
            _spriteRenderer.color = defaultColor;
        }
    }
}
