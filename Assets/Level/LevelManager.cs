using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour {
    public GridSystem grid;
    public AntSystem ants;
    public Tunnels tunnels;

    public SolvingManager solving;
    public SimulationManager simulation;

    public Key key;

    public State state = State.SOLVING;

    void OnEnable() {
        key = new Key();
        key.Enable();
        
        solving.gameObject.SetActive(true);

        solving.actions.Simulate.performed += Simulate;
        simulation.SimulationFailure += SimFail;
    }

    void Simulate(InputAction.CallbackContext context) {
        if (state != State.SIMULATING) {
            state = State.SIMULATING;
            solving.gameObject.SetActive(false);
            simulation.gameObject.SetActive(true);
        }
    }

    void SimFail(SimulationFailure failure) {
        if (state == State.SIMULATING) {
            simulation.Reset();
            Debug.Log("Simulation Failed: " + failure.GetDisplayMessage());
            state = State.SOLVING;
            simulation.gameObject.SetActive(false);
            solving.gameObject.SetActive(true);
        }
    }


    public enum State { SOLVING, SIMULATING }
}
