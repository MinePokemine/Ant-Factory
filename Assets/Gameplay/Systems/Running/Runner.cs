using UnityEngine;

public class Runner : MonoBehaviour {
    public Ants ants;
    public float tickLen;

    public void Run() {
        Ticker t = gameObject.AddComponent<Ticker>();
        t.ants = ants;
        t.tickLength = tickLen;
    }
}
