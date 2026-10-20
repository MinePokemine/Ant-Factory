using System;
using UnityEngine;

public class GridSystem : MonoBehaviour {
    public Vector2Int corner;
    public Vector2 scale;
    public Vector2 offset;

    public Vector2Int Abstract(Vector2 pos) {
        return ((pos - offset) / scale).Round();
    }

    public Vector2 Get2D(Vector2Int pos) {
        return (pos * scale) + offset;
    }

    public Vector3 Get3D(Vector2Int pos) {
        return Get2D(pos).Expand();
    }

    public bool InBounds(Vector2Int pos) {
        return Math.Abs(pos.x) <= Math.Abs(corner.x)
            && Math.Abs(pos.y) <= Math.Abs(corner.y);
    }

    void Start()
    {
        Debug.Log("InBounds(0,0) = " + InBounds(Vector2Int.zero));
    }
}
