using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonRenderer : MonoBehaviour {

    PolygonCollider2D polygon;
    LineRenderer lineRenderer;
    List<Vector3> vertices = new List<Vector3>();

    void Start () {
        polygon = GetComponent<PolygonCollider2D>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetWidth(.2f, .2f);
        lineRenderer.material = (Material)Resources.Load("CurveLineMaterial", typeof(Material));

        AddLines();
	}

    void ClearVertices(){
        vertices.Clear();
        lineRenderer.SetVertexCount(0);
    }

    void AddLines(){
        ClearVertices();
        foreach (var point in polygon.points)
        {
            var worldPoint = transform.TransformPoint(point);
            worldPoint.z = 0;
            AddVertex(point);
        }
        //Reconnect back to the first point
        AddVertex(polygon.points[0]);
    }

    void AddVertex(Vector2 vertex){
        vertices.Add(vertex);
        lineRenderer.SetVertexCount(vertices.Count);
        lineRenderer.SetPositions(vertices.ToArray());
    }

    void Update(){
        if (Application.isEditor)
        {
            AddLines();
        }
    }
}
