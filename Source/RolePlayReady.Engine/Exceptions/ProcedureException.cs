namespace RolePlayReady.Engine.Exceptions;

public class ProcedureException : Exception {
    public ProcedureException(string message)
        : base(message) {
    }

    public ProcedureException(string message, Exception inner)
        : base(message, inner) {
    }
}