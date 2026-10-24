using UnityEngine;

public abstract class Ant : MonoBehaviour {
    public Vector2Int position;
    public Ants ants;
    public Tunnels tunnels;
    public GridSystem grid;

    void Start() {
        position = grid.Abstract(transform.position);
        ants.Add(this);
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
        return (false, true);
    }

    protected abstract void UniqueTick();

    public void Tick() {
        UniqueTick();
        transform.position = grid.Get3D(position);
    }
}
