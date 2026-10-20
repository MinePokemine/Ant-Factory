using UnityEngine;

public class TunnelPlacer : MonoBehaviour {
    public Tunnels tunnels;
    public GridSystem grid;
    public Camera mainCamera;
    public InputHandler input;

    Vector2Int curr = Vector2Int.one * int.MaxValue;

    void Update() {
        if (input.digging) { // Placements
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(input.mousePos);
            Vector2Int pos = grid.Abstract(worldPos);
            //Debug.Log("Digging at grid " + pos + " - world" + worldPos);

            if (DirectionEx.GetDirection(curr - pos) == Direction.INVALID) {
                if (curr != pos) Debug.Log("Non-equal invalid direction - " + (curr - pos) + " from " + curr + " and " + pos);
            }

            else if (grid.InBounds(pos) && grid.InBounds(curr)) {
                Debug.Log("New Tunnel");
                tunnels.Add(new Tunnel(curr, pos));
            }

            curr = pos;
        } else {
            curr = Vector2Int.one * int.MaxValue;
        }
    }
}