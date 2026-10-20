using System.Numerics;
using UnityEngine;

public enum Direction {
    UP, DOWN, LEFT, RIGHT, INVALID
}

public static class DirectionEx {
    public static Vector2Int GetOffset(this Direction dir) {
        switch (dir) {
            case Direction.UP:
                return Vector2Int.up;
            case Direction.DOWN:
                return Vector2Int.down;
            case Direction.LEFT:
                return Vector2Int.left;
            case Direction.RIGHT:
                return Vector2Int.right;
            default:
                return Vector2Int.one * int.MaxValue;
        }
    }

    public static Direction GetDirection(Vector2Int offset) {
        if (offset == Vector2Int.up)
            return Direction.UP;
        if (offset == Vector2Int.down) 
            return Direction.DOWN;
        if (offset == Vector2Int.left)
            return Direction.LEFT;
        if (offset == Vector2Int.right)
            return Direction.RIGHT;
        return Direction.INVALID;
    }
}