namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveWorkflows {
    public IList<IProcess> Workflows { get;  }
}