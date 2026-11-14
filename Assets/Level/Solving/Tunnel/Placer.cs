using UnityEngine;

public class TunnelPlacer : MonoBehaviour {
    public SolvingManager manager;
    public Camera main;

    Vector2Int curr = Vector2Int.one * int.MaxValue;

    void Start() {
        if (main == null) {
            main = Camera.main;
        }
    }

    void Update() {
        if (manager.digging) { // Placements
            Vector2 worldPos = main.ScreenToWorldPoint(manager.mousePos);
            Vector2Int pos = manager.manager.grid.Abstract(worldPos);
            //Debug.Log("Digging at grid " + pos + " - world" + worldPos);

            if (DirectionEx.GetDirection(curr - pos) == Direction.INVALID) {
                if (curr != pos) Debug.Log("Non-equal invalid direction - " + (curr - pos) + " from " + curr + " and " + pos);
            }

            else if (manager.manager.grid.InBounds(pos) && manager.manager.grid.InBounds(curr)) {
                Debug.Log("New Tunnel");
                manager.manager.tunnels.Add(new Tunnel(curr, pos));
            }

            curr = pos;
        } else {
            curr = Vector2Int.one * int.MaxValue;
        }
    }
}