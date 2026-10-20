using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    private Key key;

    InputAction mousePosAction;
    InputAction digTunnelAction;

    void OnEnable() {
        key = new Key();

        mousePosAction = key.Solving.CurserPosition; mousePosAction.Enable();
        digTunnelAction = key.Solving.DigTunnel; digTunnelAction.Enable();
    }

    void OnDisable() {
        mousePosAction.Disable();
        digTunnelAction.Disable();
    }

    public Vector3 mousePos => MousePos(); // Screen Position
    Vector3 MousePos() {
        return mousePosAction.ReadValue<Vector2>();
    }

    public bool digging => Digging();
    bool Digging() {
        return digTunnelAction.IsInProgress();
    }
}
