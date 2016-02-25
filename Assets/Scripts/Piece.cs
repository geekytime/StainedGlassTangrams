using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Piece : MonoBehaviour {

    Draggable draggable;
    PolygonCollider2D polygon;
    Vector3 startPosition;
    float startRotation;
	SpriteRenderer outline;
	bool isFadingOutline = true;

	
	void Awake () {
        draggable = GetComponent<Draggable>();
        polygon = GetComponent<PolygonCollider2D>();
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles.z;
		outline = transform.FindChild ("Outline").GetComponent<SpriteRenderer>();
	}
		
    public Draggable Draggable
    {
        get
        {
            return draggable;
        }
    }

    public PolygonCollider2D Polygon
    {
        get
        {           
            return polygon;
        }
    }      

	void Update(){
		if (isFadingOutline) {
			outline.color = Color.Lerp (outline.color, Color.black, Time.deltaTime);
		}
	}

    public void Reset(){
        draggable.SetPosition(startPosition);
        draggable.ResetRotation(startRotation);
		HideOutline ();
//		ShowOutline ();
    }

	public void HideOutline(){
		outline.gameObject.SetActive (false);
		isFadingOutline = false;
	}

	public void ShowOutline(float delay){
		Invoke ("ShowOutline", delay);
	}

	public void ShowOutline(){
		outline.color = Color.clear;
		outline.gameObject.SetActive (true);
		isFadingOutline = true;
	}
}
