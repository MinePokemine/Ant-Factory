using System.Numerics;
using UnityEngine;

public enum Direction {
    UP, DOWN, LEFT, RIGHT, /*UL, UR, DL, DR,*/ INVALID
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

            /*case Direction.UL:
                return Vector2Int.up + Vector2Int.left;
            case Direction.UR:
                return Vector2Int.up + Vector2Int.right;
            case Direction.DL:
                return Vector2Int.down + Vector2Int.left;
            case Direction.DR:
                return Vector2Int.down + Vector2Int.right;*/
            
            default:
                return Vector2Int.one * int.MaxValue;
        }
    }
    public static Vector2Int Off(this Direction dir) => GetOffset(dir);

    public static Direction GetDirection(Vector2Int offset) {
        if (offset == Direction.UP.Off())
            return Direction.UP;
        if (offset == Direction.DOWN.Off()) 
            return Direction.DOWN;
        if (offset == Direction.LEFT.Off())
            return Direction.LEFT;
        if (offset == Direction.RIGHT.Off())
            return Direction.RIGHT;

        /*if (offset == Direction.UL.Off())
            return Direction.UL;
        if (offset == Direction.UL.Off())
            return Direction.UR;
        if (offset == Direction.UL.Off())
            return Direction.DL;
        if (offset == Direction.UL.Off())
            return Direction.DR;*/

        return Direction.INVALID;
    }
}