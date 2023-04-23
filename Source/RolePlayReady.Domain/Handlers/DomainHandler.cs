namespace RolePlayReady.Handlers;

public class DomainHandler {
    private readonly IDomainRepository _repository;
    private readonly string _owner;

    public DomainHandler(IDomainRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default)
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<NullableResult<Persisted<Domain>>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<Persisted<Domain>>> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result + input;
    }

    public async Task<Result<Persisted<Domain>>> UpdateAsync(Guid id, Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.UpdateAsync(_owner, id, input, cancellation)
            : result + input;
    }

    public Result<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}