using System;

public class ExceptionFailure : SimulationFailure {
    public Exception e;

    public ExceptionFailure(Ant cause, Exception e) {
        this.cause = cause;
        this.e = e;
    }

    public override string GetDisplayMessage() {
        return "An unexpected exception occured: " + e;
    }
}
