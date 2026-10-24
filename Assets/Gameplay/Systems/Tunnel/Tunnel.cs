using System;
using UnityEngine;

[Serializable]
public struct Tunnel : IEquatable<Tunnel> {
    public Vector2Int pos1;
    public Direction dir;
    public readonly Vector2Int pos2 => pos1 + dir.GetOffset();

    public Tunnel(Vector2Int pos1, Direction dir) {
        this.pos1 = pos1;
        this.dir = dir;
    }

    public Tunnel(Vector2Int pos1, Vector2Int pos2) {
        this.pos1 = pos1;
        this.dir = DirectionEx.GetDirection(pos2 - pos1);
        if (dir == Direction.INVALID) {
            throw new ArgumentException("Non-direction offset between coordinates " + pos1 + " and " + pos2 + ": " + (pos2 - pos1));
        }
    }

    public override bool Equals(object obj) {
        return obj is Tunnel other && Equals(other);
    }

    public bool Equals(Tunnel other) {
        return (pos1 == other.pos1 && pos2 == other.pos2)
            || (pos1 == other.pos2 && pos2 == other.pos1);
    }

    public override int GetHashCode() {
        int a = pos1.GetHashCode();
        int b = pos2.GetHashCode();
        return a ^ b; // Bitwise XOR
    }

    public override string ToString() {
        return $"{pos1}, {pos2} along {dir}";
    }
}