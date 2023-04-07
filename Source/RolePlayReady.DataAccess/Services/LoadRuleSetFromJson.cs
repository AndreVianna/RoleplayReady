using Attribute = RolePlayReady.Models.Attribute;

namespace RolePlayReady.DataAccess.Services;

public class LoadRuleSetFromJson : SimpleRunner<ReadJsonContext<RuleSet>, JsonDataOptions> {
    [SetsRequiredMembers]
    public LoadRuleSetFromJson(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) { }

    protected override async Task<ReadJsonContext<RuleSet>> OnRunAsync(ReadJsonContext<RuleSet> context, CancellationToken cancellation = default) {
        var fileName = context.FileName;

        // Load the json corresponding to the ruleName and deserialize it
        var filePath = $"{Options.DataRootFolder}{fileName}";
        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var ruleSetData = await JsonSerializer.DeserializeAsync<RuleSetDataModel>(stream, cancellationToken: cancellation)
                          ?? throw new InvalidDataException($"Failed to read '{nameof(RuleSet)}' data from file '{fileName}'.");

        context.Result = new RuleSet(ruleSetData.Abbreviation, ruleSetData.Name, ruleSetData.Description) {
            Tags = ruleSetData.Tags.ToList(),
        };
        foreach (var attributeData in ruleSetData.Attributes) {
            var attribute = new Attribute(Type.GetType(attributeData.DataType)!, context.Result, attributeData.Abbreviation, attributeData.Name, attributeData.Description);
            context.Result.Attributes.Add(attribute);
        }

        return context;
    }
}