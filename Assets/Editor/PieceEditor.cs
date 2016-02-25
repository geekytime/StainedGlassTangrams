using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Piece))]
public class PieceEditor: Editor
{
	void OnSceneGUI()
	{
//		VertexPaint script = (VertexPaint)target;
		var piece = (Piece)target;
		Event e = Event.current;
		switch (e.type)
		{
		case EventType.keyDown:
			{
				if (Event.current.keyCode == (KeyCode.A)) {
					piece.transform.Rotate (new Vector3 (0, 0, 15));
				} else if (Event.current.keyCode == KeyCode.S) {
					piece.transform.Rotate (new Vector3 (0, 0, -15));
				}
				
				break;
			}
		}
	}
}