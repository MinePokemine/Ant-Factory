using UnityEngine;
using UnityEngine.InputSystem;

public class Breaker : MonoBehaviour {
    public Tunnels tunnels;
    public InputHandler input;
    public GridSystem grid;
    public Camera main;

    void Start() {
        input.CollapsePerformed(Collapse);
        if (main == null) {
            main = Camera.main;
        }
    }

    void Collapse(InputAction.CallbackContext cont) {
        Vector3 world = main.ScreenToWorldPoint(input.mousePos);
        Vector2Int pos = grid.Abstract(world);
        tunnels.Remove(tunnels.Touching(pos));
    }
}
