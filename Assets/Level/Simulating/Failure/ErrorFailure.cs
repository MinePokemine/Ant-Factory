using System;

public class ErrorFailure : SimulationFailure {
    public string message;

    public ErrorFailure(Ant cause, string message) {
        this.cause = cause;
        this.message = message;
    }

    public override string GetDisplayMessage() {
        return "An unexpected error occured: " + message;
    }
}
