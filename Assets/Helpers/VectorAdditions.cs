using UnityEngine;

public static class VectorEx {
    public static Vector2Int Floor(this Vector2 xy) {
        return new Vector2Int(Mathf.FloorToInt(xy.x), Mathf.FloorToInt(xy.y));
    }

    public static Vector2Int Ceil(this Vector2 xy) {
        return new Vector2Int(Mathf.CeilToInt(xy.x), Mathf.CeilToInt(xy.y));
    }

    public static Vector2Int Round(this Vector2 xy) {
        return new Vector2Int(Mathf.RoundToInt(xy.x), Mathf.RoundToInt(xy.y));
    }

    public static Vector3 Expand(this Vector2 xy, float z) {
        return new Vector3(xy.x, xy.y, z);
    }
    public static Vector3 Expand(this Vector2 xy) {
        return Expand(xy, 0);
    }
    public static Vector2 XY(this Vector3 xyz) {
        return new Vector2(xyz.x, xyz.y);
    }
}