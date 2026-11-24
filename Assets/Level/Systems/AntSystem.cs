using System;
using System.Collections.Generic;
using UnityEngine;

public class AntSystem : MonoBehaviour {
    public LevelManager manager;

    public List<Ant> ants {get; private set;} = new();
    public int[,] grid {get; private set;}

    void OnEnable() {
        grid = new int[manager.grid.corner.x * 2, manager.grid.corner.y * 2];
        for (int x = 0; x < manager.grid.corner.x * 2; x++) {
            for (int y = 0; y < manager.grid.corner.y * 2; y++) {
                grid[x,y] = -1;
            }
        }
    }

    // Helpers
    Vector2Int ArrayPos(Vector2Int gridPos) {
        return gridPos + manager.grid.corner;
    }
    Vector2Int GridPos(Vector2Int arrayPos) {
        return arrayPos - manager.grid.corner;
    }
    (int, bool) GetArr(Vector2Int arrayPos) {
        if (InBounds(arrayPos)) {
            try {
                return (grid[arrayPos.x, arrayPos.y], false);
            } catch (Exception) {
                return (-1, true);
            }
        } else {
            Debug.Log("Array Position out of bounds: " + arrayPos);
            return (-1, true);
        }
    }
    bool SetArr(Vector2Int arrayPos, int val) {
        if (InBounds(arrayPos)) {
            grid[arrayPos.x, arrayPos.y] = val;
            return false;
        } else {
            Debug.Log("Array Position " + arrayPos + "not in bounds");
            return true;
        }
    }
    (int, bool) GetGrid(Vector2Int gridPos) {
        return GetArr(ArrayPos(gridPos));
    }
    bool SetGrid(Vector2Int gridPos, int val) {
        return SetArr(ArrayPos(gridPos), val);
    }


    // Public
    public (Ant, bool) GetAnt(Vector2Int pos) { // Grid Position -> Ant, Error
        (int cell, bool error) = GetGrid(pos);
        if (error || cell == -1) return (null, true);
        return (ants[cell], false);
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
        (Ant endA, bool error) = GetAnt(end);
        bool over = false;
        if (!error) {
            over = true;
            if (!allowOverride) {
                Debug.Log("Fatal Override");
                return (over, true);
            }
        }
        (Ant startA, bool error1) = GetAnt(start);
        if (error1) {
            Debug.Log("No ant at start");
            return (over, true);
        }

        (int cell, bool error2) = GetGrid(start);
        if (error2) {
            Debug.Log("Error getting grid");
            return (over, true);
        }
        ants[cell].position = end;

        error = SetGrid(end, cell);
        if (error) {
            Debug.Log("Error Setting End");
            return (over, true);
        }
        error = SetGrid(start, -1);
        if (error) {
            Debug.Log("Error clearing start");
            return (over, true);
        }

        return (over, false);
    }

    public bool InBounds(Vector2Int arrayPos) {
        return manager.grid.InBounds(GridPos(arrayPos));
    }

    public void Tick() {
        foreach (Ant ant in GetAll()) {
            try {
                ant.Tick();
            } catch (SimulationFailure e) {
                manager.simulation.SimulationFailure(e);
                return;
            } catch (Exception e) {
                manager.simulation.SimulationFailure(new ExceptionFailure(ant, e));
            }
        }
    }
}
