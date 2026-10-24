using System;
using UnityEngine;

public class Ticker : MonoBehaviour {
    public Ants ants;
    public float tickLength;
    float time = 0;

    void Update() {
        time += Time.deltaTime;
        if (time >= tickLength) {
            time -= tickLength;
            Tick();
        }
    }

    void Tick() {
        foreach (Ant ant in ants.GetAll()) {
            ant.Tick();
        }
    }
}
