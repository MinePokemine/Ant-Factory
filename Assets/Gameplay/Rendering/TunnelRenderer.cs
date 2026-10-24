using System.Collections.Generic;
using UnityEngine;

public class TunnelRenderer : MonoBehaviour {
    public Tunnels tunnels;
    public GridSystem grid;
    List<LineRenderer> rs = new();
    public float UpdateTime;
    public float Width;
    public Gradient colorGradient;
    float t;

    void Start() {
        
    }

    void Update() {
        t += Time.deltaTime;
        if (t >= UpdateTime) {
            t = t - UpdateTime;
            RenderTick();
        }
    }

    void RenderTick() {
        foreach (LineRenderer r in rs) {
            Destroy(r.gameObject);
        }
        rs.Clear();
        foreach (Tunnel t in tunnels.GetArray()) {
            GameObject g = new GameObject();
            g.transform.parent = transform;
            LineRenderer r = g.AddComponent<LineRenderer>();
            rs.Add(r);

            r.SetPosition(0, grid.Get3D(t.pos1));
            r.SetPosition(1, grid.Get3D(t.pos2));

            r.startWidth = Width;
            r.endWidth = Width;

            r.colorGradient = colorGradient;
        }
    }
}
