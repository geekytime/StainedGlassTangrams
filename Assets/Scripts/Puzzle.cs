using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Puzzle : MonoBehaviour {

    public PieceSet pieceSetPrefab;
    public List<PolygonCollider2D> puzzleSections;
	bool isSolved = false;
    public string PuzzleName;

    PieceSet pieceSet;

	void Start () {
        //TODO: Fix this when we get to multi-piece puzzles
        puzzleSections = GetComponents<PolygonCollider2D>().ToList();
        pieceSet = Instantiate(pieceSetPrefab).GetComponent<PieceSet>();
        pieceSet.transform.SetParent(this.transform);

        TangramsSupervisor.GetInstance().DragController.PieceSet = pieceSet;
	}

    public bool IsSolved
    {
        get
        {
            return isSolved;
        }
    }

    public PieceSet PieceSet {
        get {
            return pieceSet;
        }
    }
		
	void Update () {
		if (!Input.GetMouseButton (0) && !isSolved) {
			isSolved = CheckIsSolved ();
			if (isSolved) {
				ShowOutlines ();
                Data.GetInstance().SetSolved(PuzzleName);
                TangramsSupervisor.GetInstance().SetSolved(this);
			}
		}
	}
        
    bool CheckIsSolved(){
        if (pieceSet == null || pieceSet.Pieces == null)
        {
            return false;
        }
        foreach (var piece in pieceSet.Pieces)
        {
            if (piece.Draggable.IsMoving())
            {
                return false;
            }
            if (!IsContained(piece))
            {
                return false;
            }
            if (IsOverlappingOtherPieces(piece))
            {    
                return false;
            }         
        }
        return true;
    }

    bool IsOverlappingOtherPieces(Piece piece){        
        foreach (var otherPiece in pieceSet.Pieces)
        {
            if (piece.GetInstanceID() != otherPiece.GetInstanceID()){
                if (IsOverlappingOtherPiece(piece, otherPiece)){                    
                    return true;
                }
            }
        }
        return false;
    }

    bool IsOverlappingOtherPiece(Piece piece, Piece otherPiece){
        foreach (var point in piece.Polygon.points)
        {
            var worldPoint = piece.transform.TransformPoint(point);
            if (otherPiece.Polygon.OverlapPoint(worldPoint))
            {
                return true;
            }
        }
        return false;
    }

    bool IsContained(Piece piece){
        foreach (var puzzleSection in puzzleSections)
        {
			if (!IsContained(piece.Polygon, puzzleSection))
            {
                return false;
            }
        }
        return true;
    }

    bool IsContained(PolygonCollider2D piece, PolygonCollider2D puzzleSection){
        foreach (var point in piece.points)
        {
            var worldPoint = piece.transform.TransformPoint(point);
            if (!puzzleSection.OverlapPoint(worldPoint))
            {
                return false;
            }
        }
        return true;
    }

	void ShowOutlines(){
		var delay = 0f;
		foreach (var piece in pieceSet.Pieces) {
			delay = delay + .15f;
			piece.ShowOutline (delay);
		}
	}
}

