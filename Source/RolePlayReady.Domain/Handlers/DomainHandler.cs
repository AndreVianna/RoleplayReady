namespace RolePlayReady.Handlers;

public class DomainHandler {
    private readonly IDomainRepository _repository;
    private readonly string _owner;

    public DomainHandler(IDomainRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(_owner, cancellation).ConfigureAwait(false);
        return new(list);
    }

    public async Task<NullableResult<Domain>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<Domain>> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result + input;
    }

    public async Task<Result<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.UpdateAsync(_owner, input, cancellation)
            : result + input;
    }

    public Result<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}