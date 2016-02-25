using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Draggable : MonoBehaviour {

	bool isDragging = false;
    bool isSelected = false;
	Vector3 offset;
    Rigidbody2D body2d;
	SpriteRenderer sprite;

    float targetRotation;
    Vector3 targetPosition;
    bool isResetting = false;

    void Start(){
        body2d = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer> ();
        targetRotation = body2d.rotation;
        targetPosition = transform.position;
    }

	void Update () {
		if (isDragging) {
            isResetting = false;
			var worldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            worldPosition.z = transform.position.z;
            transform.position = worldPosition - offset;
		}

        if (targetRotation != body2d.rotation)
        {
            float velocity = .5f;
            var rotationSpeed = GetRotationSpeed();
            body2d.rotation = Mathf.SmoothDamp(body2d.rotation, targetRotation, ref velocity, rotationSpeed);
        }

        if (isResetting)
        {            
            var velocity = new Vector2();
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime * 3);
            if (transform.position == targetPosition)
            {
                isResetting = false;
            }
        }

        if (isSelected)
        {

        }
	}

    float GetRotationSpeed(){
        if (isResetting)
        {
            return Time.deltaTime * 3;
        } else
        {
            return Time.deltaTime;
        }
    }

    public void StartDrag(Vector2 worldPosition){
        isSelected = true;
		isDragging = true;
        offset = worldPosition - body2d.position;
        offset.z = 0;
        body2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		sprite.sortingOrder = 999;
	}

    public void SetSelected(bool value){
        isSelected = value;
    }

    public void EndDrag(){
        isDragging = false;
		sprite.sortingOrder = 0;
    }

	public void Pin(){		
		body2d.constraints = RigidbodyConstraints2D.FreezeAll;
	}

    public void Unpin(){      
        body2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetPosition(Vector3 position){
        targetPosition = position;
    }

    public void ResetRotation(float rotation){
        isResetting = true;
        targetRotation = rotation;
    }

	public void RotateClockwise(){
        targetRotation = targetRotation + 15;
	}

	public void RotateCounterClockwise(){
        targetRotation = targetRotation - 15;
	}

	public bool IsMoving(){
		return body2d.velocity != Vector2.zero;
	}
}
