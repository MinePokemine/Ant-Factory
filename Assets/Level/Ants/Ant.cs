using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ant : MonoBehaviour {
    public Vector2Int position = Vector2Int.one * int.MaxValue;
    public Vector2Int startingPosition;
    public AntSystem manager;

    void Start() {
        startingPosition = manager.manager.grid.Abstract(transform.position);
        position = startingPosition;
        transform.position = manager.manager.grid.Get3D(position);

        bool error = manager.manager.ants.Add(this);
        if (error) {
            Debug.Log("Error adding ant to ants");
        }

        manager.manager.solving.actions.Simulate.performed += StartSim;
    }

    void StartSim(InputAction.CallbackContext context) {
        position = startingPosition;
    }

    protected (bool, bool) Move(Vector2Int end, bool allowOverride = false) {
        return manager.Move(position, end, allowOverride);
    }

    protected (bool, bool) MoveAlong(Tunnel tunnel, bool allowOverride = false) {
        if (tunnel.pos1 == position) {
            return manager.Move(position, tunnel.pos2, allowOverride);
        }
        if (tunnel.pos2 == position) {
            return manager.Move(position, tunnel.pos1, allowOverride);
        }
        Debug.Log("Ant at " + position + " not on tunnel " + tunnel);
        return (false, true);
    }

    protected abstract void UniqueTick();
    protected abstract void UniqueReset();

    public void Tick() {
        UniqueTick();
        transform.position = manager.manager.grid.Get3D(position);
    }

    public void Reset() {
        UniqueReset();
        Move(startingPosition, false);
        transform.position = manager.manager.grid.Get3D(position);
    }
}
