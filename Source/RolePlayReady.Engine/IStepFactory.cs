namespace RolePlayReady.Engine;

public interface IStepFactory {
    IStep Create(Type stepType);
}
