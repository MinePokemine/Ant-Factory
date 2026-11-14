using UnityEngine;
using UnityEngine.InputSystem;

public class Breaker : MonoBehaviour {
    public SolvingManager manager;

    public GridSystem grid;
    public Camera main;

    void Start() {
        manager.actions.Remove.performed += Collapse;
        if (main == null) {
            main = Camera.main;
        }
    }

    void Collapse(InputAction.CallbackContext cont) {
        Vector3 world = main.ScreenToWorldPoint(manager.mousePos);
        Vector2Int pos = grid.Abstract(world);
        manager.manager.tunnels.Remove(manager.manager.tunnels.Touching(pos));
    }
}
