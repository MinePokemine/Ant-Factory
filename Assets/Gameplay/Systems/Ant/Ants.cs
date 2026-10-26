using System.Collections.Generic;
using UnityEngine;

public class Ants : MonoBehaviour {
    public GridSystem gridSys;
    public List<Ant> ants {get; private set;} = new();
    public int[,] grid {get; private set;}
    public Tunnels tunnels;

    void OnEnable() {
        grid = new int[gridSys.corner.x * 2, gridSys.corner.y * 2];
        for (int x = 0; x < gridSys.corner.x * 2; x++) {
            for (int y = 0; y < gridSys.corner.y * 2; y++) {
                grid[x,y] = -1;
            }
        }
    }

    // Helpers
    Vector2Int ArrayPos(Vector2Int gridPos) {
        return gridPos + gridSys.corner;
    }
    Vector2Int GridPos(Vector2Int arrayPos) {
        return arrayPos - gridSys.corner;
    }
    (int, bool) GetArr(Vector2Int arrayPos) {
        if (InBounds(arrayPos)) {
            Debug.Log(arrayPos);
            return (grid[arrayPos.x, arrayPos.y], false);
        } else {
            return (0, true);
        }
    }
    bool SetArr(Vector2Int arrayPos, int val) {
        if (InBounds(arrayPos)) {
            grid[arrayPos.x, arrayPos.y] = val;
            return false;
        } else {
            return true;
        }
    }
    (int, bool) GetGrid(Vector2Int gridPos) {
        if (gridSys.InBounds(gridPos)) {
            return GetArr(ArrayPos(gridPos));
        } else {
            return (0, true);
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
        if (ants.Contains(ant)) {
            return true; // Error
        }
        (int existing, bool error) = GetGrid(ant.position);
        if (error) {
            Debug.Log("Error Getting Grid at Ants Position");
            return true;
        }
        if (existing != -1) {
            Debug.Log("Already Ant Here: Ant #" + existing);
            return true;
        }
        error = SetGrid(ant.position, ants.Count);
        if (error) {
            Debug.Log("Error Setting Grid to Add Ant");
            return true;
        }
        ants.Add(ant);

        return false;
    }
    public (bool, bool) Move(Vector2Int start, Vector2Int end, bool allowOverride = false) { // (override / would override, stopped execution)
        Ant endA = Get(end);
        bool over = false;
        if (endA != null) {
            over = true;
            if (!allowOverride) {
                Debug.Log("Fatal Override");
                return (over, true);
            }
        }
        Ant startA = Get(start);
        if (startA == null) {
            Debug.Log("No ant at start");
            return (over, true);
        }

        (int cell, bool error) = GetGrid(start);
        if (error) {
            Debug.Log("Error getting grid");
            return (over, true);
        }

        error = SetGrid(end, cell);
        if (error) {
            Debug.Log("Error Setting Grid");
            return (over, true);
        }

        return (over, false);
    }

    public bool InBounds(Vector2Int arrayPos) {
        return gridSys.InBounds(GridPos(arrayPos));
    }
}
