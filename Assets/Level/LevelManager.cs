using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour {
    public GridSystem grid;
    public AntSystem ants;
    public Tunnels tunnels;

    public SolvingManager solving;
    public SimulationManager simulation;

    public State state = State.SOLVING;

    void OnEnable() {
        solving.enabled = true;
        simulation.enabled = false;

        solving.actions.Simulate.performed += Simulate;
        simulation.SimulationFailure += SimFail;
    }

    void Simulate(InputAction.CallbackContext context) {
        state = State.SIMULATING;
        solving.enabled = false;
        simulation.enabled = true;
    }

    void SimFail(SimulationFailure failure) {
        Debug.Log("Simulation Failed: " + failure.GetDisplayMessage());
        simulation.enabled = false;
        solving.enabled = true;
    }


    public enum State { SOLVING, SIMULATING }
}
