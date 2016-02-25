using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PieceSet : MonoBehaviour {
	
    List<Piece> pieces;

	void Start () {
        pieces = GetComponentsInChildren<Piece>().ToList();
	}
	
    public List<Piece> Pieces{
        get{
            return pieces;
        }
    }
       
}
