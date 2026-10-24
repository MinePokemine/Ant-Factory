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
    int GetArr(Vector2Int arrayPos) {
        return grid[arrayPos.x, arrayPos.y];
    }
    void SetArr(Vector2Int arrayPos, int val) {
        grid[arrayPos.x, arrayPos.y] = val;
    }
    int GetGrid(Vector2Int gridPos) {
        return GetArr(ArrayPos(gridPos));
    }
    void SetGrid(Vector2Int gridPos, int val) {
        SetArr(ArrayPos(gridPos), val);
    }

    // Public
    public Ant Get(Vector2Int pos) {
        if (GetGrid(pos) == -1) return null;
        return ants[GetGrid(pos)];
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

        SetGrid(end, GetGrid(start));

        return (over, false); // Maybe override (no fatal override), no error
    }

}
