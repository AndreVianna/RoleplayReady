using Attribute = RolePlayReady.Models.Attribute;

namespace RolePlayReady.DataAccess.Repositories;

public class RuleSetRepository : IRuleSetRepository {
    private readonly IDataFileRepository _files;

    public RuleSetRepository(IDataFileRepository files) {
        _files = files;
    }

    public async Task<IEnumerable<RuleSet>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<RuleSetDataModel>(cancellation)
            .ConfigureAwait(false);
        return files.Select(MapFrom).ToArray();
    }

    public async Task<RuleSet?> GetByIdAsync(string id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<RuleSetDataModel>(id, cancellation)
            .ConfigureAwait(false);
        return file is not null
            ? MapFrom(file)
            : null;
    }

    public Task UpsertAsync(RuleSet ruleSet, CancellationToken cancellation = default)
        => _files.UpsertAsync(ruleSet.Abbreviation, MapFrom(ruleSet), cancellation);

    public void Delete(string id)
        => _files.Delete(id);

    private static RuleSetDataModel MapFrom(IRuleSet input)
        => new() {
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            Attributes = input.Attributes.Select(MapFrom).ToArray(),
        };

    private static RuleSetDataModel.Attribute MapFrom(IAttribute input)
        => new() {
            Abbreviation = input.Abbreviation,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.Name,
        };

    private static RuleSet MapFrom(DataFile<RuleSetDataModel> input) {
        var result = new RuleSet(input.Id, input.Content.Name, input.Content.Name) {
            Timestamp = input.Timestamp,
            Tags = input.Content.Tags,
        };
        foreach (var attribute in input.Content.Attributes) {
            result.Attributes.Add(new Attribute(Type.GetType(attribute.DataType)!, result, attribute.Abbreviation, attribute.Name, attribute.Description));
        }

        return result;
    }

}
