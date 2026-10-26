using System.Collections.Generic;
using UnityEngine;

public class Ants : MonoBehaviour {
    public GridSystem gridSys;
    List<Ant> ants = new();
    int[,] grid;
    public Tunnels tunnels;

    void OnEnable() {
        grid = new int[gridSys.corner.x * 2, gridSys.corner.y * 2];
    }

    // Helpers
    Vector2Int ArrayPos(Vector2Int gridPos) {
        return gridPos - gridSys.corner;
    }
    Vector2Int GridPos(Vector2Int arrayPos) {
        return arrayPos + gridSys.corner;
    }
    (int, bool) GetArr(Vector2Int arrayPos) {
        if (InBounds(arrayPos)) {
            return (grid[arrayPos.x, arrayPos.y], true);
        } else {
            return (0, false);
        }
    }
    bool SetArr(Vector2Int arrayPos, int val) {
        if (InBounds(arrayPos)) {
            grid[arrayPos.x, arrayPos.y] = val;
            return true;
        } else {
            return false;
        }
    }
    (int, bool) GetGrid(Vector2Int gridPos) {
        if (gridSys.InBounds(gridPos)) {
            return GetArr(ArrayPos(gridPos));
        } else {
            return (0, false);
        }
    }
    bool SetGrid(Vector2Int gridPos, int val) {
        if (gridSys.InBounds(gridPos)) {
            SetArr(ArrayPos(gridPos), val);
            return true;
        } else {
            return false;
        }
    }

    // Public
    public Ant Get(Vector2Int pos) {
        (int cell, bool success) = GetGrid(pos);
        if (!success || cell == -1) return null;
        return ants[cell];
    }
    public Ant[] GetAll() {
        return ants.ToArray();
    }
    public bool Add(Ant ant) {
        Vector2Int pos = ant.position;
        if (ants.Contains(ant)) {
            return false;
        }
        ants.Add(ant);
        grid[pos.x + gridSys.corner.x, pos.y + gridSys.corner.y] = ants.Count - 1;
        return true;
    }
    public (bool, bool) Move(Vector2Int start, Vector2Int end, bool allowOverride = false) { // (override / would override, other error)
        Ant endA = Get(end);
        bool over = false;
        if (endA != null) {
            over = true;
            if (!allowOverride) {
                return (true, false); // Override, no other error
            }
        }
        Ant startA = Get(start);
        if (startA == null) {
            return (over, true); // No override, other error (no ant at starting position)
        }

        (int cell, bool success) = GetGrid(start);
        if (!success) {
            return (over, true);
        }

        success = SetGrid(end, cell);
        if (!success) {
            return (over, true);
        }

        return (over, false); // Maybe override (no fatal override), no error
    }

    public bool InBounds(Vector2Int arrayPos) {
        return gridSys.InBounds(GridPos(arrayPos));
    }
}
