using System;

public abstract class SimulationFailure : Exception {
    public Ant cause {get; protected set;}

    public abstract string GetDisplayMessage();
}
