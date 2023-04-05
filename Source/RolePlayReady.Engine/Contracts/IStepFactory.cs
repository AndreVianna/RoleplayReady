namespace RolePlayReady.Engine.Contracts;

public interface IStepFactory {
    IStep Create(Type stepType);
}
