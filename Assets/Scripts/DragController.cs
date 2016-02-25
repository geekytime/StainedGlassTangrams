using UnityEngine;
using System.Collections.Generic;

public class DragController : MonoBehaviour {	   

    List<Draggable> dragging = new List<Draggable>();
    float lastRotationTime = 0;
    float rotationDelay = .1f;

    public PieceSet PieceSet { get; set; }
    		
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hits = Physics2D.OverlapPointAll(worldPosition);
            foreach (var hit in hits)
            {
                if (hit != null)
                {
                    var draggable = hit.GetComponent<Draggable>();
                    if (draggable != null)
                    {
                        draggable.StartDrag(worldPosition);
                        PinOthers(draggable);
                        dragging.Add(draggable);
                        break;
                    }
                }
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            foreach (var draggable in dragging)
            {
                draggable.EndDrag();               
                PinThis(draggable);
            }
            dragging.Clear();
        }

		if (Input.GetKeyDown (KeyCode.X)) {
            RotatePiecesCounterClockwise();
        } else if (Input.GetKey(KeyCode.X)){
            if (Time.time - lastRotationTime > rotationDelay)
            {
                RotatePiecesCounterClockwise();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            RotatePiecesClockwise();
        } else if (Input.GetKey(KeyCode.Z)){
            if (Time.time - lastRotationTime > rotationDelay)
            {
                RotatePiecesClockwise();
            }

        }
	}

    void RotatePiecesClockwise(){
        lastRotationTime = Time.time;
        foreach (var draggable in dragging) {
            draggable.RotateClockwise ();
        }
    }

    void RotatePiecesCounterClockwise(){
        lastRotationTime = Time.time;
        foreach (var draggable in dragging) {
            draggable.RotateCounterClockwise ();
        }
    }

	void PinOthers(Draggable dragged){
        foreach (var piece in PieceSet.Pieces){
            if (piece.GetInstanceID() == dragged.GetInstanceID())
            {
                piece.Draggable.Unpin();
            } else
            {
                piece.Draggable.Pin();
            }
		}
	}

    void PinThis(Draggable dropped){
        foreach (var piece in PieceSet.Pieces){
            if (piece.GetInstanceID() == dropped.GetInstanceID())
            {
                piece.Draggable.Pin();
            } else
            {
                piece.Draggable.Unpin();
            }

        }
    }
}
