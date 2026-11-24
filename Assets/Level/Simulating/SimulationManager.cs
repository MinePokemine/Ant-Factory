using System;
using UnityEngine;

public class SimulationManager : MonoBehaviour {
    public LevelManager manager;

    public float tickLength;
    float time = 0;

    public Action<SimulationFailure> SimulationFailure;
    public Action<int> SimulationSuccess;

    void Update() {
        time += Time.deltaTime;
        if (time >= tickLength) {
            time -= tickLength;
            Tick();
        }
    }

    void Tick() {
        manager.ants.Tick();
    }

    public void Reset() {
        foreach (Ant ant in manager.ants.ants) {
            ant.Reset();
        }
    }
}
