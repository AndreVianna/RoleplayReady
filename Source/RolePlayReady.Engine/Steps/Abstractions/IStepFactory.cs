namespace RolePlayReady.Engine.Steps.Abstractions;

public interface IStepFactory {
    IStep Create(Type stepType);
}
