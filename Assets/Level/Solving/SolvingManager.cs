using UnityEngine;

public class SolvingManager : MonoBehaviour {
    public LevelManager manager;

    public TunnelPlacer placer;

    public bool tunneling;
    public GameObject antPlacerPrefab;

    Key key;
    public Key.SolvingActions actions => key.Solving;

    public Vector2 mousePos => actions.CurserPosition.ReadValue<Vector2>();
    public bool digging => actions.Place.IsInProgress();

    void OnEnable() {
        key.Enable();
        actions.Enable();
    }
}
