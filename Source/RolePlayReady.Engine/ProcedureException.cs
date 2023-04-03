namespace RolePlayReady.Engine;

public class ProcedureException : Exception {
    public ProcedureException() {
    }

    public ProcedureException(string message)
        : base(message) {
    }

    public ProcedureException(string message, Exception inner)
        : base(message, inner) {
    }
}