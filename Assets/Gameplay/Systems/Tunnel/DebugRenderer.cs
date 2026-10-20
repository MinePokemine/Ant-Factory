using UnityEngine;

public class DebugTunnelRenderer : MonoBehaviour {
    public Tunnels tunnels;
    public GridSystem grid;

    void Update() {
        foreach (Tunnel t in tunnels.GetArray()) {
            Debug.DrawLine(grid.Get3D(t.pos1), grid.Get3D(t.pos2));
        }
    }
}
