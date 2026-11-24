using Unity.VisualScripting;
using UnityEngine;

public class SolvingManager : MonoBehaviour {
    public LevelManager manager;

    public TunnelPlacer placer;
    public TunnelBreaker breaker;

    public bool tunneling {
        get => placementMode;
        set {
            placementMode = value;
            if (value) {
                placer.gameObject.SetActive(true);
                breaker.gameObject.SetActive(true);
            } else {
                placer.gameObject.SetActive(false);
                breaker.gameObject.SetActive(false);
            }
        }
    }
    bool placementMode;

    public Key.SolvingActions actions => manager.key.Solving;

    public Vector2 mousePos => actions.CurserPosition.ReadValue<Vector2>();
    public bool digging => actions.Place.IsInProgress();

    void OnEnable() {
        actions.Enable();
        tunneling = true;
    }
}
