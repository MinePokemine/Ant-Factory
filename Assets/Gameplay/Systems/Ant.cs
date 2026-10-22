using UnityEngine;

public abstract class Ant : MonoBehaviour {
    public Vector2Int position;
    public Ants ants;
    public GridSystem grid;

    protected (bool, bool) Move(Vector2Int end) {
        return ants.Move(position, end);
    }
}
