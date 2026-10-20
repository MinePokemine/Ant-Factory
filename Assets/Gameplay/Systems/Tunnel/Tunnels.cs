using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tunnels : MonoBehaviour {
    private HashSet<Tunnel> tunnels = new();

    public bool Dug(Tunnel tunnel) {
        return tunnels.Contains(tunnel);
    }

    public Tunnel[] Touching(Vector2Int pos) {
        return tunnels.Where(x => (x.pos1 == pos || x.pos2 == pos)).ToArray();
    }

    public void Add(Tunnel tunnel) {
        tunnels.Add(tunnel);
    }

    public Tunnel[] GetArray() {
        return tunnels.ToArray();
    }
}
