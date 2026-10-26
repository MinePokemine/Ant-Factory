using UnityEngine;

public abstract class Ant : MonoBehaviour {
    public Vector2Int position;
    public Ants ants;
    public Tunnels tunnels;
    public GridSystem grid;

    void Start() {
        position = grid.Abstract(transform.position);
        Debug.Log(position);
        bool error = ants.Add(this);
        if (error) {
            Debug.Log("Error adding ant to ants");
        }
    }

    protected (bool, bool) Move(Vector2Int end, bool allowOverride = false) {
        return ants.Move(position, end, allowOverride);
    }

    protected (bool, bool) MoveAlong(Tunnel tunnel, bool allowOverride = false) {
        if (tunnel.pos1 == position) {
            return ants.Move(position, tunnel.pos2, allowOverride);
        }
        if (tunnel.pos2 == position) {
            return ants.Move(position, tunnel.pos1, allowOverride);
        }
        Debug.Log("Ant at " + position + " not on tunnel " + tunnel);
        return (false, true);
    }

    protected abstract void UniqueTick(Tunnels tunnels);

    public void Tick(Tunnels tunnels) {
        UniqueTick(tunnels);
        transform.position = grid.Get3D(position);
    }
}
